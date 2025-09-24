using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using journey_service.Entities;
using journey_service.Services;
using journey_service.Events;

namespace journey_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneysController : ControllerBase
    {
        private readonly KafkaProducerService _kafkaProducer;
        private readonly JourneyContext _context;

        public JourneysController(JourneyContext context, KafkaProducerService kafkaProducer)
        {
            _context = context;
            _kafkaProducer = kafkaProducer;
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
        public async Task<ActionResult<Spot>> AddSpot(long journeyId, Spot spot)
        {
            var journey = await _context.Journeys
                .Include(j => j.Spots)
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            if (journey == null) return NotFound();

            journey.Spots.Add(spot);
            await _context.SaveChangesAsync();

            await _kafkaProducer.SendMessageAsync("image-service", journeyId.ToString(), new CreateImageEvent(journeyId,spot.Id));

            return CreatedAtAction(nameof(GetSpots), new { journeyId }, spot);
        }
    }
}
