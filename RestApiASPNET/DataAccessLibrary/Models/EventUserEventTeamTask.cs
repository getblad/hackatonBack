namespace DataAccessLibrary.Models;

public partial class EventUserEventTeamMission
{
    public int EventUserId { get; set; }

    public int EventTeamMissionId { get; set; }

    public virtual EventTeamMission EventTeamMission { get; set; } = null!;

    public virtual EventUser EventUser { get; set; } = null!;
}