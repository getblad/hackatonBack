using DataAccessLibrary.Repositories;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models;

public partial class Mission:IStatus
{
    public int MissionId { get; set; }

    public int MissionTypeId { get; set; }

    public string MissionName { get; set; } = null!;

    public string MissionDescription { get; set; } = null!;

    public string MissionLanguage { get; set; } = null!;

    public string MissionAuthor { get; set; } = null!;

    public string? MissionUrlFileDescription { get; set; }

    public TimeSpan MissionExecutionTime { get; set; }

    public int MissionPoint { get; set; }

    public TimeSpan? MissionStepTimeFine { get; set; }

    public int? MissionStepPointFine { get; set; }

    public int? MissionMainTaskId { get; set; }

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

public class MissionDtoAdmin
{
    public int MissionId { get; set; }

    public int MissionTypeId { get; set; }

	[Required(ErrorMessage = "Fill in the task name field\r\n")]
	[StringLength(100, MinimumLength = 4, ErrorMessage = "Task name must be between 4 and 100 characters")]
	public string MissionName { get; set; } = null!;

    [Required(ErrorMessage = "Fill in the description field\r\n")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Descriprion must be between 10 and 1000 characters")]
    public string MissionDescription { get; set; } = null!;

    [Required(ErrorMessage = "Fill in the language field\r\n")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Field language must be between 1 and 200 characters")]
    public string MissionLanguage { get; set; } = null!;

    [Required(ErrorMessage = "Fill in the author field\r\n")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Field author must be between 3 and 50 characters")]
    public string MissionAuthor { get; set; } = null!;


    [Required(ErrorMessage = "Fill in the language field\r\n")]
    [StringLength(1000, ErrorMessage = "Field url must be between 0 and 1000 characters")]
    public string? MissionUrlFileDescription { get; set; }

    public TimeSpan MissionExecutionTime { get; set; }

    public int MissionPoint { get; set; }

    public TimeSpan? MissionStepTimeFine { get; set; }

    public int? MissionStepPointFine { get; set; }

    public int? MissionMainTaskId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }
    
    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
    
    
}