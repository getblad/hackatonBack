namespace DataAccessLibrary.Models;

public partial class EventTask
{
    public int EventTaskId { get; set; }

    public int EventId { get; set; }

    public int TaskId { get; set; }

    public string? EventTaskLanguage { get; set; }

    public TimeSpan? EventTaskExecutionTime { get; set; }

    public int? EventTaskPoint { get; set; }

    public TimeSpan? EventTaskStepTimeFine { get; set; }

    public int? EventTaskStepPointFine { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<EventTeamTask> EventTeamTasks { get; } = new List<EventTeamTask>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;
}
