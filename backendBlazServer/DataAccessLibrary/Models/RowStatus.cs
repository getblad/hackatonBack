namespace DataAccessLibrary.Models;

public partial class RowStatus
{
    public int RowStatusId { get; set; }

    public string RowStatusName { get; set; } = null!;

    public virtual ICollection<EventTask> EventTasks { get; } = new List<EventTask>();

    public virtual ICollection<EventTeamTask> EventTeamTasks { get; } = new List<EventTeamTask>();

    public virtual ICollection<EventTeam> EventTeams { get; } = new List<EventTeam>();

    public virtual ICollection<EventUser> EventUsers { get; } = new List<EventUser>();

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}
