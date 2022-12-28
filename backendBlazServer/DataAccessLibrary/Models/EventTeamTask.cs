namespace DataAccessLibrary.Models;

public partial class EventTeamTask
{
    public int EventTeamTaskId { get; set; }

    public int EventTeamId { get; set; }

    public int EventTaskId { get; set; }

    public DateTime EventTeamTaskStartTime { get; set; }

    public DateTime? EventTeamTaskEndTime { get; set; }

    public int EventTeamTaskStatusId { get; set; }

    public string? EventTeamTaskUrlGitHub { get; set; }

    public int? EventTeamTaskCheckingUserId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual EventTask EventTask { get; set; } = null!;

    public virtual EventTeam EventTeam { get; set; } = null!;

    public virtual User? EventTeamTaskCheckingUser { get; set; }

    public virtual EventTeamTaskStatus EventTeamTaskStatus { get; set; } = null!;

    public virtual ICollection<EventUserEventTeamTask> EventUserEventTeamTasks { get; } = new List<EventUserEventTeamTask>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;
}
