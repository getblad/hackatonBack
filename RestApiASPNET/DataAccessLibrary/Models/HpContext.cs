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

    public virtual DbSet<EventMission> EventMissions { get; set; }

    public virtual DbSet<EventTeam> EventTeams { get; set; }

    public virtual DbSet<EventTeamMission> EventTeamMissions { get; set; }

    public virtual DbSet<EventTeamMissionStatus> EventTeamMissionStatuses { get; set; }

    public virtual DbSet<EventUser> EventUsers { get; set; }

    public virtual DbSet<EventUserEventTeamMission> EventUserEventTeamMissions { get; set; }

    

    public virtual DbSet<RowStatus> RowStatuses { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<MissionType> MissionTypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User?> Users { get; set; }


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
                .HasColumnType("datetime")
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

        modelBuilder.Entity<EventMission>(entity =>
        {
            entity.ToTable("EventMission");

            entity.Property(e => e.EventMissionId).HasColumnName("EventMission_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EventMissionExecutionTime)
                .HasPrecision(2)
                .HasColumnName("EventMission_execution_time");
            entity.Property(e => e.EventMissionLanguage)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EventMission_language");
            entity.Property(e => e.EventMissionPoint).HasColumnName("EventMission_point");
            entity.Property(e => e.EventMissionStepPointFine).HasColumnName("EventMission_step_point_fine");
            entity.Property(e => e.EventMissionStepTimeFine)
                .HasPrecision(2)
                .HasColumnName("EventMission_step_time_fine");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.MissionId).HasColumnName("Mission_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.EventMissionCreateUsers)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventMission_user");

            entity.HasOne(d => d.Event).WithMany(p => p.EventMissions)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvenMission_event_id");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.EventMissions)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventMission_row_status");

            entity.HasOne(d => d.Mission).WithMany(p => p.EventMissions)
                .HasForeignKey(d => d.MissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventMission_Mission_id");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EventMissionUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventMission_user1");
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

        modelBuilder.Entity<EventTeamMission>(entity =>
        {
            entity.ToTable("EventTeamMission");

            entity.Property(e => e.EventTeamMissionId)
                .ValueGeneratedNever()
                .HasColumnName("EventTeamMission_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.EventMissionId).HasColumnName("EventMission_id");
            entity.Property(e => e.EventTeamId).HasColumnName("EventTeam_id");
            entity.Property(e => e.EventTeamMissionCheckingUserId).HasColumnName("EventTeamMission_checking_user_id");
            entity.Property(e => e.EventTeamMissionEndTime)
                .HasColumnType("datetime")
                .HasColumnName("EventTeamMission_end_time");
            entity.Property(e => e.EventTeamMissionStartTime)
                .HasColumnType("datetime")
                .HasColumnName("EventTeamMission_start_time");
            entity.Property(e => e.EventTeamMissionStatusId).HasColumnName("EventTeamMission_status_id");
            entity.Property(e => e.EventTeamMissionUrlGitHub)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("EventTeamMission_urlGitHub");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.EventTeamMissionCreateUsers)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamMission_user");

            entity.HasOne(d => d.EventMission).WithMany(p => p.EventTeamMissions)
                .HasForeignKey(d => d.EventMissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamMission_EventMission");

            entity.HasOne(d => d.EventTeam).WithMany(p => p.EventTeamMissions)
                .HasForeignKey(d => d.EventTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamMission_EventTeam_id");

            entity.HasOne(d => d.EventTeamMissionCheckingUser).WithMany(p => p.EventTeamMissionEventTeamMissionCheckingUsers)
                .HasForeignKey(d => d.EventTeamMissionCheckingUserId)
                .HasConstraintName("FK_EventTeamMission_user2");

            entity.HasOne(d => d.EventTeamMissionStatus).WithMany(p => p.EventTeamMissions)
                .HasForeignKey(d => d.EventTeamMissionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamMission_EventTeamMission_status_id");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.EventTeamMissions)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamMission_row_status");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EventTeamMissionUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventTeamMission_user1");
        });

        modelBuilder.Entity<EventTeamMissionStatus>(entity =>
        {
            entity.ToTable("EventTeamMissionStatus");

            entity.Property(e => e.EventTeamMissionStatusId).HasColumnName("EventTeamMission_status_id");
            entity.Property(e => e.EventTeamMissionStatusName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EventTeamMission_status_name");
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

        modelBuilder.Entity<EventUserEventTeamMission>(entity =>
        {
            entity.HasKey(e => e.EventUserId);

            entity.ToTable("EventUser_EventTeamMission");

            entity.Property(e => e.EventUserId)
                .ValueGeneratedNever()
                .HasColumnName("EventUser_id");
            entity.Property(e => e.EventTeamMissionId).HasColumnName("EventTeamMission_id");

            entity.HasOne(d => d.EventTeamMission).WithMany(p => p.EventUserEventTeamMissions)
                .HasForeignKey(d => d.EventTeamMissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_EventTeamMission_id");

            entity.HasOne(d => d.EventUser).WithOne(p => p.EventUserEventTeamMission)
                .HasForeignKey<EventUserEventTeamMission>(d => d.EventUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventUser_event_user_id");
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

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.ToTable("Mission");

            entity.HasIndex(e => e.MissionName, "IX_Mission").IsUnique();

            entity.Property(e => e.MissionId)
                .ValueGeneratedNever()
                .HasColumnName("Mission_id");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
            entity.Property(e => e.RowStatusId).HasColumnName("row_status_id");
            entity.Property(e => e.MissionAuthor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Mission_author");
            entity.Property(e => e.MissionDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Mission_description");
            entity.Property(e => e.MissionExecutionTime)
                .HasPrecision(2)
                .HasColumnName("Mission_execution_time");
            entity.Property(e => e.MissionLanguage)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Mission_language");
            entity.Property(e => e.MissionMainTaskId).HasColumnName("Mission_main_task_id");
            entity.Property(e => e.MissionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Mission_name");
            entity.Property(e => e.MissionPoint).HasColumnName("Mission_point");
            entity.Property(e => e.MissionStepPointFine).HasColumnName("Mission_step_point_fine");
            entity.Property(e => e.MissionStepTimeFine)
                .HasPrecision(2)
                .HasColumnName("Mission_step_time_fine");
            entity.Property(e => e.MissionTypeId).HasColumnName("Mission_type_id");
            entity.Property(e => e.MissionUrlFileDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Mission_url_file_description");
            entity.Property(e => e.UpdateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

            entity.HasOne(d => d.CreateUser).WithMany(p => p.MissionCreateUsers)
                .HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mission_user");

            entity.HasOne(d => d.RowStatus).WithMany(p => p.Missions)
                .HasForeignKey(d => d.RowStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mission_row_status");

            entity.HasOne(d => d.MissionType).WithMany(p => p.Missions)
                .HasForeignKey(d => d.MissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mission_Mission_type_id");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.MissionUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mission_user1");
        });

        modelBuilder.Entity<MissionType>(entity =>
        {
            entity.ToTable("Mission_type");

            entity.Property(e => e.MissionTypeId).HasColumnName("Mission_type_id");
            entity.Property(e => e.MissionTypeName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Mission_type_name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("team");

            entity.HasIndex(e => e.TeamId, "IX_team").IsUnique();

            entity.Property(e => e.TeamId)
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
            // entity.Property(e => e.RoleId).HasColumnName("role_id");
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

            
            entity.HasOne(d => d.Team).WithMany(p => p.Users).HasForeignKey(d => d.TeamId);

            entity.HasOne(d => d.CreateUser).WithMany().HasForeignKey(d => d.CreateUserId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.UpdateUser).WithMany().HasForeignKey(d => d.UpdateUserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.RowStatus).WithMany(p => p.Users).HasForeignKey(d => d.RowStatusId);
        });
        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
   
}
