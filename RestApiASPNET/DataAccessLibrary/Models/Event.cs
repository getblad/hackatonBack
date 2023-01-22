using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLibrary.Repositories;

namespace DataAccessLibrary.Models;

public partial class Event:IStatus
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public string? EventUrlAvatar { get; set; }

    public DateTime EventStartTime { get; set; }

    public DateTime EventEndTime { get; set; }

    [Column("event_created_date")]
    public DateTime EventCreatedDate { get; set; } 

    public int EventStatusId { get; set; }

    public int? EventMaxCountOfTeamMembers { get; set; }

    public int? EventMinCountOfTeamMembers { get; set; }

    public int? EventMaxCountOfEventMembers { get; set; }

    public int? EventMinCountOfEventMembers { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual EventStatus EventStatus { get; set; } = null!;

    public virtual ICollection<EventMission> EventMissions { get; set; } = new List<EventMission>();

    public virtual ICollection<EventTeam> EventTeams { get; } = new List<EventTeam>();

    public virtual ICollection<EventUser> EventUsers { get; } = new List<EventUser>();
}
public class EventDtoAdmin
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public string? EventUrlAvatar { get; set; }

    public DateTime EventStartTime { get; set; }
   
    public DateTime EventEndTime { get; set; }

    public  DateTime EventCreatedDate { get; set; } 

    public int EventStatusId { get; set; }

    public int? EventMaxCountOfTeamMembers { get; set; }

    public int? EventMinCountOfTeamMembers { get; set; }

    public int? EventMaxCountOfEventMembers { get; set; }

    public int? EventMinCountOfEventMembers { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public  ICollection<MissionDtoAdmin>? Missions { get; set; } = new List<MissionDtoAdmin>();

  
}