namespace DataAccessLibrary.Models;

public partial class EventUser
{
    public int EventUserId { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public int? EventTeamId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual EventTeam? EventTeam { get; set; }

    public virtual EventUserEventTeamMission? EventUserEventTeamMission { get; set; }

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
