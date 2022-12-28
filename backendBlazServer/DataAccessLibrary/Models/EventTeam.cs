﻿namespace DataAccessLibrary.Models;

public partial class EventTeam
{
    public int EventTeamId { get; set; }

    public int EventId { get; set; }

    public int TeamId { get; set; }

    public int? EventTeamCapitanId { get; set; }

    public int CreateUserId { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RowStatusId { get; set; }

    public virtual User CreateUser { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<EventTeamTask> EventTeamTasks { get; } = new List<EventTeamTask>();

    public virtual ICollection<EventUser> EventUsers { get; } = new List<EventUser>();

    public virtual RowStatus RowStatus { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual User UpdateUser { get; set; } = null!;
}
