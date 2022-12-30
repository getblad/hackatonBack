namespace DataAccessLibrary.Models;

public partial class EventTeamMissionStatus
{
    public int EventTeamMissionStatusId { get; set; }

    public string EventTeamMissionStatusName { get; set; } = null!;

    public virtual ICollection<EventTeamMission> EventTeamMissions { get; } = new List<EventTeamMission>();
}
