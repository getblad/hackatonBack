namespace DataAccessLibrary.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserFirstName { get; set; } = null!;

    public string UserSecondName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public int RoleId { get; set; }

    /// <summary>
    /// To store url to avatar image
    /// </summary>
    public string UserAvatar { get; set; } = null!;

    public int? TeamId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual ICollection<EventTask> EventTaskCreateUsers { get; } = new List<EventTask>();

    public virtual ICollection<EventTask> EventTaskUpdateUsers { get; } = new List<EventTask>();

    public virtual ICollection<EventTeam> EventTeamCreateUsers { get; } = new List<EventTeam>();

    public virtual ICollection<EventTeamTask> EventTeamTaskCreateUsers { get; } = new List<EventTeamTask>();

    public virtual ICollection<EventTeamTask> EventTeamTaskEventTeamTaskCheckingUsers { get; } = new List<EventTeamTask>();

    public virtual ICollection<EventTeamTask> EventTeamTaskUpdateUsers { get; } = new List<EventTeamTask>();

    public virtual ICollection<EventTeam> EventTeamUpdateUsers { get; } = new List<EventTeam>();

    public virtual ICollection<EventUser> EventUserCreateUsers { get; } = new List<EventUser>();

    public virtual ICollection<EventUser> EventUserUpdateUsers { get; } = new List<EventUser>();

    public virtual ICollection<EventUser> EventUserUsers { get; } = new List<EventUser>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Task> TaskCreateUsers { get; } = new List<Task>();

    public virtual ICollection<Task> TaskUpdateUsers { get; } = new List<Task>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}
