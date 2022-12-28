namespace DataAccessLibrary.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int TaskTypeId { get; set; }

    public string TaskName { get; set; } = null!;

    public string TaskDescription { get; set; } = null!;

    public string TaskLanguage { get; set; } = null!;

    public string TaskAuthor { get; set; } = null!;

    public string TaskUrlFileDescription { get; set; } = null!;

    public TimeSpan TaskExecutionTime { get; set; }

    public int TaskPoint { get; set; }

    public TimeSpan? TaskStepTimeFine { get; set; }

    public int? TaskStepPointFine { get; set; }

    public int? TaskMainTaskId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual ICollection<EventTask> EventTasks { get; } = new List<EventTask>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual TaskType TaskType { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;
}
