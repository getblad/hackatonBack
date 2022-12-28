using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Models;

public partial class HpContext : DbContext
{
    public HpContext()
    {
    }

    public HpContext(DbContextOptions<HpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventStatus> EventStatuses { get; set; }

    public virtual DbSet<EventTask> EventTasks { get; set; }

    public virtual DbSet<EventTeam> EventTeams { get; set; }

    public virtual DbSet<EventTeamTask> EventTeamTasks { get; set; }

    public virtual DbSet<EventTeamTaskStatus> EventTeamTaskStatuses { get; set; }

    public virtual DbSet<EventUser> EventUsers { get; set; }

    public virtual DbSet<EventUserEventTeamTask> EventUserEventTeamTasks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RowStatus> RowStatuses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskType> TaskTypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("event");

            entity.HasIndex(e => e.EventName, "IX_event").IsUnique();

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.EventCreatedDate)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("event_created_date");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("event_description");
            entity.Property(e => e.EventEndTime)
                .HasColumnType("datetime")
                .HasColumnName("event_end_time");
            entity.Property(e => e.EventMaxCountOfEventMembers).HasColumnName("event_max_count_of_event_members");
            entity.Property(e => e.EventMaxCountOfTeamMembers).HasColumnName("event_max_count_of_team_members");
            entity.Property(e => e.EventMinCountOfEventMembers).HasColumnName("event_min_count_of_event_members");
            entity.Property(e => e.EventMinCountOfTeamMembers).HasColumnName("event_min_count_of_team_members");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("event_name");
            entity.Property(e => e.EventStartTime)
                .HasColumnType("datetime")
                .HasColumnName("event_start_time");
            entity.Property(e => e.EventStatusId).HasColumnName("event_status_id");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.EventStatus).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_event_EventStatus_EventStatus_id");
        });

        modelBuilder.Entity<EventStatus>(entity =>
        {
            entity.ToTable("EventStatus");

            entity.Property(e => e.EventStatusId).HasColumnName("event_status_id");
            entity.Property(e => e.EventStatusName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EventStatus_name");
        });

        modelBuilder.Entity<EventTask>(entity =>
        {
            entity.ToTable("EventTask");

            entity.Property(e => e.EventTaskId).HasColumnName("EventTask_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EventTaskExecutionTime)
                .HasPrecision(2)
                .HasColumnName("EventTask_execution_time");
            entity.Property(e => e.EventTaskLanguage)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EventTask_language");
            entity.Property(e => e.EventTaskPoint).HasColumnName("EventTask_point");
            entity.Property(e => e.EventTaskStepPointFine).HasColumnName("EventTask_step_point_fine");
            entity.Property(e => e.EventTaskStepTimeFine)
                .HasPrecision(2)
                .HasColumnName("EventTask_step_time_fine");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.EventTaskCreateUsers)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTask_user");

            entity.HasOne(d => d.Event).WithMany(p => p.EventTasks)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvenTask_event_id");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.EventTasks)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTask_row_status");

            entity.HasOne(d => d.Task).WithMany(p => p.EventTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTask_task_id");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EventTaskUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTask_user1");
        });

        modelBuilder.Entity<EventTeam>(entity =>
        {
            entity.ToTable("EventTeam");

            entity.HasIndex(e => e.EventId, "IX_EventTeam");

            entity.Property(e => e.EventTeamId).HasColumnName("EventTeam_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EventTeamCapitanId).HasColumnName("EventTeam_capitan_id");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.EventTeamCreateUsers)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeam_user");

            entity.HasOne(d => d.Event).WithMany(p => p.EventTeams)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeam_event_id");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.EventTeams)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeam_row_status");

            entity.HasOne(d => d.Team).WithMany(p => p.EventTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeam_team_id");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EventTeamUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeam_user1");
        });

        modelBuilder.Entity<EventTeamTask>(entity =>
        {
            entity.ToTable("EventTeamTask");

            entity.Property(e => e.EventTeamTaskId)
                .ValueGeneratedNever()
                .HasColumnName("EventTeamTask_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.EventTaskId).HasColumnName("EventTask_id");
            entity.Property(e => e.EventTeamId).HasColumnName("EventTeam_id");
            entity.Property(e => e.EventTeamTaskCheckingUserId).HasColumnName("EventTeamTask_checking_user_id");
            entity.Property(e => e.EventTeamTaskEndTime)
                .HasColumnType("datetime")
                .HasColumnName("EventTeamTask_end_time");
            entity.Property(e => e.EventTeamTaskStartTime)
                .HasColumnType("datetime")
                .HasColumnName("EventTeamTask_start_time");
            entity.Property(e => e.EventTeamTaskStatusId).HasColumnName("EventTeamTask_status_id");
            entity.Property(e => e.EventTeamTaskUrlGitHub)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EventTeamTask_urlGitHub");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.EventTeamTaskCreateUsers)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamTask_user");

            entity.HasOne(d => d.EventTask).WithMany(p => p.EventTeamTasks)
                .HasForeignKey(d => d.EventTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamTask_EventTask");

            entity.HasOne(d => d.EventTeam).WithMany(p => p.EventTeamTasks)
                .HasForeignKey(d => d.EventTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamTask_EventTeam_id");

            entity.HasOne(d => d.EventTeamTaskCheckingUser).WithMany(p => p.EventTeamTaskEventTeamTaskCheckingUsers)
                .HasForeignKey(d => d.EventTeamTaskCheckingUserId)
                .HasConstraintName("FK_EventTeamTask_user2");

            entity.HasOne(d => d.EventTeamTaskStatus).WithMany(p => p.EventTeamTasks)
                .HasForeignKey(d => d.EventTeamTaskStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamTask_EventTeamTask_status_id");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.EventTeamTasks)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamTask_row_status");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EventTeamTaskUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamTask_user1");
        });

        modelBuilder.Entity<EventTeamTaskStatus>(entity =>
        {
            entity.ToTable("EventTeamTaskStatus");

            entity.Property(e => e.EventTeamTaskStatusId).HasColumnName("EventTeamTask_status_id");
            entity.Property(e => e.EventTeamTaskStatusName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EventTeamTask_status_name");
        });

        modelBuilder.Entity<EventUser>(entity =>
        {
            entity.ToTable("EventUser");

            entity.Property(e => e.EventUserId).HasColumnName("EventUser_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EventTeamId).HasColumnName("EventTeam_id");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.EventUserCreateUsers)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_user");

            entity.HasOne(d => d.Event).WithMany(p => p.EventUsers)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_Event_event_id");

            entity.HasOne(d => d.EventTeam).WithMany(p => p.EventUsers)
                .HasForeignKey(d => d.EventTeamId)
                .HasConstraintName("FK_EventUser_EventTeam_id");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.EventUsers)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_row_status");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EventUserUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_user1");

            entity.HasOne(d => d.User).WithMany(p => p.EventUserUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_user_id");
        });

        modelBuilder.Entity<EventUserEventTeamTask>(entity =>
        {
            entity.HasKey(e => e.EventUserId);

            entity.ToTable("EventUser_EventTeamTask");

            entity.Property(e => e.EventUserId)
                .ValueGeneratedNever()
                .HasColumnName("EventUser_id");
            entity.Property(e => e.EventTeamTaskId).HasColumnName("EventTeamTask_id");

            entity.HasOne(d => d.EventTeamTask).WithMany(p => p.EventUserEventTeamTasks)
                .HasForeignKey(d => d.EventTeamTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_EventTeamTask_id");

            entity.HasOne(d => d.EventUser).WithOne(p => p.EventUserEventTeamTask)
                .HasForeignKey<EventUserEventTeamTask>(d => d.EventUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_event_user_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<RowStatus>(entity =>
        {
            entity.ToTable("row_status");

            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.RowStatusName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("row_status_name");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("task");

            entity.HasIndex(e => e.TaskName, "IX_task").IsUnique();

            entity.Property(e => e.TaskId)
                .ValueGeneratedNever()
                .HasColumnName("task_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.TaskAuthor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("task_author");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("task_description");
            entity.Property(e => e.TaskExecutionTime)
                .HasPrecision(2)
                .HasColumnName("task_execution_time");
            entity.Property(e => e.TaskLanguage)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("task_language");
            entity.Property(e => e.TaskMainTaskId).HasColumnName("task_main_task_id");
            entity.Property(e => e.TaskName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("task_name");
            entity.Property(e => e.TaskPoint).HasColumnName("task_point");
            entity.Property(e => e.TaskStepPointFine).HasColumnName("task_step_point_fine");
            entity.Property(e => e.TaskStepTimeFine)
                .HasPrecision(2)
                .HasColumnName("task_step_time_fine");
            entity.Property(e => e.TaskTypeId).HasColumnName("task_type_id");
            entity.Property(e => e.TaskUrlFileDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("task_url_file_description");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.TaskCreateUsers)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_task_user");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_task_row_status");

            entity.HasOne(d => d.TaskType).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_task_task_type_id");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.TaskUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_task_user1");
        });

        modelBuilder.Entity<TaskType>(entity =>
        {
            entity.ToTable("task_type");

            entity.Property(e => e.TaskTypeId).HasColumnName("task_type_id");
            entity.Property(e => e.TaskTypeName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("task_type_name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("team");

            entity.HasIndex(e => e.TeamId, "IX_team").IsUnique();

            entity.Property(e => e.TeamId)
                .ValueGeneratedNever()
                .HasColumnName("team_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.TeamAvatar)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("team_avatar");
            entity.Property(e => e.TeamCapitanId).HasColumnName("team_capitan_id");
            entity.Property(e => e.TeamName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("team_name");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.Teams)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_team_user");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.Teams)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_team_row_status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.HasIndex(e => e.UserEmail, "IX_user_1").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");
            entity.Property(e => e.UserAvatar)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("To store url to avatar image")
                .HasColumnName("user_avatar");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.UserFirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_first_name");
            entity.Property(e => e.UserSecondName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_second_name");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role");

            entity.HasOne(d => d.Team).WithMany(p => p.Users).HasForeignKey(d => d.TeamId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
