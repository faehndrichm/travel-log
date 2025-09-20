using Microsoft.AspNetCore.Mvc;

namespace image_service.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly ILogger<ImageController> _logger;
    private readonly string _imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "stored-images");

    public ImageController(ILogger<ImageController> logger)
    {
        _logger = logger;

        if (!Directory.Exists(_imageFolder))
            Directory.CreateDirectory(_imageFolder);
    }

    [HttpPost]
    public async Task<IActionResult> UploadAsync(IFormFile image)
    {
        if (image == null || image.Length == 0)
            return BadRequest("No image uploaded.");

        string id = Guid.NewGuid().ToString();

        string filePath = GetImagePath(id);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return Ok(new { Id = id, Message = "Image uploaded." });
    }


    [HttpGet("{id}")]
    public IActionResult DownloadAsync(string id)
    {
        string filePath = GetImagePath(id);

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "image/jpeg", $"{id}.jpg");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAsync(string id)
    {
        string filePath = GetImagePath(id);

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        System.IO.File.Delete(filePath);
        return NoContent();
    }

    private string GetImagePath(string id) => Path.Combine(_imageFolder, $"{id}.jpg");
}
