namespace CommandsService.EventProcessing;

public interface IEventProcessor
{
    void ProcessEvent(string message);
}

enum EventType
{
    PlatformPublished,
    Undetermined
}