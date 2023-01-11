using System.Text.Json.Serialization;

namespace DataAccessLibrary.Models;

public partial class RowStatus
{
    public int RowStatusId { get; set; }

    public string RowStatusName { get; set; } = null!;
    // [JsonIgnore]
    public virtual ICollection<EventMission> EventMissions { get; } = new List<EventMission>();
    [JsonIgnore]
    public virtual ICollection<EventTeamMission> EventTeamMissions { get; } = new List<EventTeamMission>();
    [JsonIgnore]
    public virtual ICollection<EventTeam> EventTeams { get; } = new List<EventTeam>();
    [JsonIgnore]
    public virtual ICollection<EventUser> EventUsers { get; } = new List<EventUser>();
    [JsonIgnore]
    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();
    
    public virtual ICollection<Team> Teams { get; } = new List<Team>();
    [JsonIgnore]
    public virtual ICollection<User> Users { get; } = new List<User>();
}
