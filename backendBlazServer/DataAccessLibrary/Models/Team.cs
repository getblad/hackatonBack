namespace DataAccessLibrary.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string? TeamAvatar { get; set; }

    public int TeamCapitanId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual ICollection<EventTeam> EventTeams { get; } = new List<EventTeam>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
