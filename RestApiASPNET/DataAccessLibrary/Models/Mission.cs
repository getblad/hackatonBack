namespace DataAccessLibrary.Models;

public partial class Mission
{
    public int MissionId { get; set; }

    public int MissionTypeId { get; set; }

    public string MissionName { get; set; } = null!;

    public string MissionDescription { get; set; } = null!;

    public string MissionLanguage { get; set; } = null!;

    public string MissionAuthor { get; set; } = null!;

    public string MissionUrlFileDescription { get; set; } = null!;

    public TimeSpan MissionExecutionTime { get; set; }

    public int MissionPoint { get; set; }

    public TimeSpan? MissionStepTimeFine { get; set; }

    public int? MissionStepPointFine { get; set; }

    public int? MissionMainMissionId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual ICollection<EventMission> EventMissions { get; } = new List<EventMission>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual MissionType MissionType { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;
}
