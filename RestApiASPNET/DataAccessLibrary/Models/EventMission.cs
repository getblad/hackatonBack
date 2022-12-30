namespace DataAccessLibrary.Models;

public partial class EventMission
{
    public int EventMissionId { get; set; }

    public int EventId { get; set; }

    public int MissionId { get; set; }

    public string? EventMissionLanguage { get; set; }

    public TimeSpan? EventMissionExecutionTime { get; set; }

    public int? EventMissionPoint { get; set; }

    public TimeSpan? EventMissionStepTimeFine { get; set; }

    public int? EventMissionStepPointFine { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<EventTeamMission> EventTeamMissions { get; } = new List<EventTeamMission>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual Mission Mission { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;
}
