namespace journey_service.Dto;
public class SpotDto
{
    public required string Name { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public IFormFile? Image { get; set; }
}