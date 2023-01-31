using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLibrary.Repositories;

namespace DataAccessLibrary.Models;

public partial class EventTeam : IStatus
{
    public int EventTeamId { get; set; }

    public int EventId { get; set; }

    public int? TeamId { get; set; }

    [Column("eventTeam_name")] public string EventTeamName { get; set; } = null!;

    [Column("eventTeam_avatar")] public string EventTeamAvatar { get; set; } = null!;

    public int? EventTeamCapitanId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<EventTeamMission> EventTeamMissions { get; } = new List<EventTeamMission>();

    public virtual ICollection<EventUser> EventUsers { get; } = new List<EventUser>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;
    
    [Column("EventTeam_twitterPoint")]
    [DefaultValue(false)]
    public bool TeamTwitterPoint { get; set; } 
    [Column("EventTeam_point")]
    [DefaultValue(0)]
    public int EventTeamPoint { get; set; }
}
public class EventTeamDto
{
    public int EventTeamId { get; set; }
    public int EventId { get; set; }
    public int? TeamId { get; set; }
    public string EventTeamName { get; set; } = null!;
    public string EventTeamAvatar { get; set; } = null!;
    public int? EventTeamCapitanId { get; set; }
    public bool TeamTwitterPoint { get; set; } 
    public int EventTeamPoint { get; set; }
    public List<UserDtoAdmin?> Users { get; set; } = null!;
}