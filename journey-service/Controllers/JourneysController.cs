using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using journey_service.Entities;
using journey_service.Services;
using journey_service.Dto;
using Microsoft.AspNetCore.Authorization;

namespace journey_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JourneysController : ControllerBase
    {
        private readonly KafkaProducerService _kafkaProducer;
        private readonly S3ImageStorageService _imageService;
        private readonly JourneyContext _context;

        public JourneysController(JourneyContext context, KafkaProducerService kafkaProducer, S3ImageStorageService imageService)
        {
            _context = context;
            _kafkaProducer = kafkaProducer;
            _imageService = imageService;
        }

        // GET: api/Journeys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Journey>>> GetJourneys()
        {
            return await _context.Journeys.ToListAsync();
        }

        // GET: api/Journeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Journey>> GetJourney(long id)
        {
            var journey = await _context.Journeys.FindAsync(id);

            if (journey == null)
            {
                return NotFound();
            }

            return journey;
        }

        // PUT: api/Journeys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJourney(long id, Journey journey)
        {
            if (id != journey.Id)
            {
                return BadRequest();
            }

            _context.Entry(journey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JourneyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Journeys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Journey>> PostJourney(Journey journey)
        {
            _context.Journeys.Add(journey);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJourney), new { id = journey.Id }, journey);
        }

        // DELETE: api/Journeys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJourney(long id)
        {
            var journey = await _context.Journeys.FindAsync(id);
            if (journey == null)
            {
                return NotFound();
            }

            _context.Journeys.Remove(journey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JourneyExists(long id)
        {
            return _context.Journeys.Any(e => e.Id == id);
        }

        // GET: api/Journeys/5/Spots
        [HttpGet("{journeyId}/Spots")]
        public async Task<ActionResult<IEnumerable<Spot>>> GetSpots(long journeyId)
        {
            var journey = await _context.Journeys
                .Include(j => j.Spots)
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            if (journey == null) return NotFound();

            return Ok(journey.Spots);
        }

        // POST: api/Journeys/5/Spots
        [HttpPost("{journeyId}/Spots")]
        public async Task<ActionResult<Spot>> AddSpot(long journeyId, SpotDto spotDto)
        {
            var journey = await _context.Journeys
                .Include(j => j.Spots)
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            if (journey == null) return NotFound();

            var spot = new Spot
            {
                JourneyId = journeyId,
                Name = spotDto.Name,
                Latitude = spotDto.Latitude,
                Longitude = spotDto.Longitude,
                Journey = journey
            };

            if (spotDto.Image != null)
            {
                spot.SpotImageId = await _imageService.UploadImageAsync(journeyId.ToString(), spotDto.Image);
            }

            journey.Spots.Add(spot);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSpots), new { journeyId }, spot);
        }
    }
}
