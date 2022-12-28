namespace DataAccessLibrary.Models;

public partial class EventStatus
{
    public int EventStatusId { get; set; }

    public string EventStatusName { get; set; } = null!;

    public virtual ICollection<Event> Events { get; } = new List<Event>();
}
