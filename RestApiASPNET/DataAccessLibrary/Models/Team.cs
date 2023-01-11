using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Models;

public partial class Team
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string? TeamAvatar { get; set; }

    public int TeamCapitanId { get; set; }

    [ForeignKey("CreateUser")]
    public int CreateUserId { get; set; }

    [ForeignKey("UpdateUser")]
    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    [DeleteBehavior(DeleteBehavior.NoAction)]
    
    public virtual User UpdateUser { get; set; } = null!;

    public virtual ICollection<EventTeam> EventTeams { get; } = new List<EventTeam>();

    
    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}

public class TeamDtoAdmin
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string? TeamAvatar { get; set; }

    public int TeamCapitanId { get; set; }

    public int CreateUserId { get; set; }
    public int UpdateUserId { get; set; }
    public DateTime? CreateTime { get; set; }

    public DateTime? UpdateTime { get; set; }
    
    
   
}
