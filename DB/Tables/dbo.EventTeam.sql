CREATE TABLE [dbo].[EventTeam]
(
[EventTeam_id] [int] NOT NULL IDENTITY(1, 1),
[event_id] [int] NOT NULL,
[team_id] [int] NOT NULL,
[EventTeam_capitan_id] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL,
[update_time] [datetime] NOT NULL,
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTeam] ADD CONSTRAINT [PK_EventTeam] PRIMARY KEY CLUSTERED ([EventTeam_id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_EventTeam] ON [dbo].[EventTeam] ([event_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTeam] ADD CONSTRAINT [FK_EventTeam_event_id] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[EventTeam] ADD CONSTRAINT [FK_EventTeam_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[EventTeam] ADD CONSTRAINT [FK_EventTeam_team_id] FOREIGN KEY ([team_id]) REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[EventTeam] ADD CONSTRAINT [FK_EventTeam_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventTeam] ADD CONSTRAINT [FK_EventTeam_user1] FOREIGN KEY ([update_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
