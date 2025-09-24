namespace journey_service.Entities;

public class Spot
{
    public long Id { get; set; }
    public required long JourneyId { get; set; }
    public required string Name { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public string? SpotImageId { get; set; }

    // Navigation property
    public required Journey Journey { get; set; }

}