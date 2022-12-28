namespace DataAccessLibrary.Models;

public partial class TaskType
{
    public int TaskTypeId { get; set; }

    public string TaskTypeName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
