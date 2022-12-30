namespace DataAccessLibrary.Models;

public partial class EventTeamMission
{
    public int EventTeamMissionId { get; set; }

    public int EventTeamId { get; set; }

    public int EventMissionId { get; set; }

    public DateTime EventTeamMissionStartTime { get; set; }

    public DateTime? EventTeamMissionEndTime { get; set; }

    public int EventTeamMissionStatusId { get; set; }

    public string? EventTeamMissionUrlGitHub { get; set; }

    public int? EventTeamMissionCheckingUserId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual EventMission EventMission { get; set; } = null!;

    public virtual EventTeam EventTeam { get; set; } = null!;

    public virtual User? EventTeamMissionCheckingUser { get; set; }

    public virtual EventTeamMissionStatus EventTeamMissionStatus { get; set; } = null!;

    public virtual ICollection<EventUserEventTeamMission> EventUserEventTeamMissions { get; } = new List<EventUserEventTeamMission>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;
}
