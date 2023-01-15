using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Models;

public partial class User : IStatus
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
    
    
    // public string? GitHub { get; set; }
    
    public virtual RowStatus? RowStatus { get; set; }
    
    [Column("user_birthday")]
    [Required]
    public DateTime UserBirthday { get; set; }
    
    [Column("user_birthday_visibility")]
    
    public bool UserBirthdayVisibility { get; set; }

    [Column("user_github")]
    public string? UserGitHub { get; set; } = null!;
    
    [Column("user_twitter")]
    public string? UserTwitter { get; set; } = null!;
    
    [Column("user_instagram")]
    public string? UserInstagram { get; set; } = null!;

    [Column("user_facebook")]
    public string? UserFacebook { get; set; } = null!;

    [Column("user_auth0_id")] 
    public string UserAuth0Id { get; set; } = null!;
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

public class UserDtoPublic
{
    


    public string UserFirstName { get; set; } = null!;

    public string UserSecondName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;
    
    public string UserAvatar { get; set; } = null!;
    
    public int? TeamId { get; set; }
    
    
}

public class UserDtoAdmin
{

    public int UserId { get; set; }

    public string UserFirstName { get; set; } = null!;

    public string UserSecondName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;
    
    public string UserAvatar { get; set; } = null!;
    
    public int? TeamId { get; set; }
    
    public DateTime CreateTime { get; set; }
    
    public DateTime UpdateTime { get; set; }
    
    public int CreateUserId { get; set; }
    
    public int UpdateUserId { get; set; }
    
    public DateTime UserBirthday { get; set; }

    public bool UserBirthdayVisibility { get; set; }
    
    public string? UserGitHub { get; set; } = null!;
    
    public string? UserTwitter { get; set; } = null!;
    
    public string? UserInstagram { get; set; } = null!;
    
    public string? UserFacebook { get; set; } = null!;

    public string UserAuth0Id { get; set; } = null!;

}
