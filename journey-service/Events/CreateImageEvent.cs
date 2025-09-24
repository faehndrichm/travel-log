namespace journey_service.Events;
public record CreateImageEvent(long JourneyId, long SpotId): BaseEvent;