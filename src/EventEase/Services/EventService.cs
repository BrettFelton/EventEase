using EventEase.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EventEase.Services;

public class EventService
{
    private readonly HttpClient _httpClient;
    private const string JsonFilePath = "sample-events.json";

    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Get all events
    /// </summary>
    /// <returns></returns>
    public async Task<List<EventModel>> GetEventsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<EventModel>>(JsonFilePath) ?? [];
    }

    /// <summary>
    /// Get an event by ID
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<EventModel> GetyEventByIdAsync(int eventId)
    {
        List<EventModel> events = await GetEventsAsync();
        return events.FirstOrDefault(e => e.Id == eventId) ?? new();
    }

    /// <summary>
    /// Add attendee to an event
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="attendee"></param>
    /// <returns></returns>
    public async Task AddAttendeeAsync (int eventId, string attendee)
    {
        List<EventModel> events = await GetEventsAsync();
        EventModel selectedEvent = events.FirstOrDefault(e => e.Id == eventId) ?? new();

        if (selectedEvent != null)
        {
            selectedEvent.AddAttendee(attendee);
            await SaveEventsAsync(events);
        }
    }

    /// <summary>
    /// Save updated events
    /// </summary>
    /// <param name="events"></param>
    /// <returns></returns>
    private async Task SaveEventsAsync(List<EventModel> events)
    {
        JsonSerializerOptions options = new() { WriteIndented = true };
        string json = JsonSerializer.Serialize(events, options);
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", JsonFilePath);
        
        await File.WriteAllTextAsync(filePath, json);
    }
}