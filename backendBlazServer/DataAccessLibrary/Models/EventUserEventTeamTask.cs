namespace DataAccessLibrary.Models;

public partial class EventUserEventTeamTask
{
    public int EventUserId { get; set; }

    public int EventTeamTaskId { get; set; }

    public virtual EventTeamTask EventTeamTask { get; set; } = null!;

    public virtual EventUser EventUser { get; set; } = null!;
}