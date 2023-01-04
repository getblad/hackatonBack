using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Models;

public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    
    public string UserFirstName { get; set; } = null!;

    public string UserSecondName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    

    /// <summary>
    /// To store url to avatar image
    /// </summary>
    public string UserAvatar { get; set; } = null!;

    public int? TeamId { get; set; }

    [ForeignKey("CreateUser")]
    

    public int CreateUserId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]

    
    public User? CreateUser { get; set; }

    [ForeignKey("UpdateUser")]

    public int UpdateUserId { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]

    public User? UpdateUser { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
    
    public int RowStatusId { get; set; }
    
    public virtual RowStatus? RowStatus { get; set; }

    public virtual ICollection<EventMission> EventMissionCreateUsers { get; } = new List<EventMission>();

    public virtual ICollection<EventMission> EventMissionUpdateUsers { get; } = new List<EventMission>();

    public virtual ICollection<EventTeam> EventTeamCreateUsers { get; } = new List<EventTeam>();

    public virtual ICollection<EventTeamMission> EventTeamMissionCreateUsers { get; } = new List<EventTeamMission>();

    public virtual ICollection<EventTeamMission> EventTeamMissionEventTeamMissionCheckingUsers { get; } = new List<EventTeamMission>();

    public virtual ICollection<EventTeamMission> EventTeamMissionUpdateUsers { get; } = new List<EventTeamMission>();

    public virtual ICollection<EventTeam> EventTeamUpdateUsers { get; } = new List<EventTeam>();

    public virtual ICollection<EventUser> EventUserCreateUsers { get; } = new List<EventUser>();

    public virtual ICollection<EventUser> EventUserUpdateUsers { get; } = new List<EventUser>();

    public virtual ICollection<EventUser> EventUserUsers { get; } = new List<EventUser>();

    

    public virtual ICollection<Mission> MissionCreateUsers { get; } = new List<Mission>();

    public virtual ICollection<Mission> MissionUpdateUsers { get; } = new List<Mission>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}

public class UserPublic
{
    


    public string UserFirstName { get; set; } = null!;

    public string UserSecondName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;
    
    public string UserAvatar { get; set; } = null!;
    
    public int? TeamId { get; set; }
    
    
}

public class UserAdmin
{

    public int? UserId { get; set; }

    public string UserFirstName { get; set; } = null!;

    public string UserSecondName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;
    
    public string UserAvatar { get; set; } = null!;
    
    public int? TeamId { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public DateTime UpdateTime { get; set; }
    
    public int UpdateUser { get; set; }
    
}
