namespace EventEase.Services;

public class SessionState
{
    public string UserName { get; set; } = string.Empty;
    public string UserEmail {  get; set; } = string.Empty;
    public Dictionary<int, List<string>> EventAttendees { get; private set; } = [];

    public void RegisterForEvent(int eventId, string attendee)
    {
        if (!EventAttendees.ContainsKey(eventId))
        {
            EventAttendees[eventId] = [];
        }

        if (!EventAttendees[eventId].Contains(attendee))
        {
            EventAttendees[eventId].Add(attendee);
        }
    }

    public List<string> GetAttendeesForEvent(int eventId)
    {
        return EventAttendees.ContainsKey(eventId) ? EventAttendees[eventId] : [];
    }
}