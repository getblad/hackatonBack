namespace DataAccessLibrary.Models;

public partial class EventTeamTaskStatus
{
    public int EventTeamTaskStatusId { get; set; }

    public string EventTeamTaskStatusName { get; set; } = null!;

    public virtual ICollection<EventTeamTask> EventTeamTasks { get; } = new List<EventTeamTask>();
}
