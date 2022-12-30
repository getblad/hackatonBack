namespace DataAccessLibrary.Models;

public partial class RowStatus
{
    public int RowStatusId { get; set; }

    public string RowStatusName { get; set; } = null!;

    public virtual ICollection<EventMission> EventMissions { get; } = new List<EventMission>();

    public virtual ICollection<EventTeamMission> EventTeamMissions { get; } = new List<EventTeamMission>();

    public virtual ICollection<EventTeam> EventTeams { get; } = new List<EventTeam>();

    public virtual ICollection<EventUser> EventUsers { get; } = new List<EventUser>();

    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();

    public virtual ICollection<Team> Teams { get; } = new List<Team>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
