CREATE TABLE [dbo].[EventTeamMission]
(
[EventTeamMission_id] [int] NOT NULL,
[EventTeam_id] [int] NOT NULL,
[EventMission_id] [int] NOT NULL,
[EventTeamMission_start_time] [datetime] NOT NULL,
[EventTeamMission_end_time] [datetime] NULL,
[EventTeamMission_status_id] [int] NOT NULL,
[EventTeamMission_urlGitHub] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NULL,
[EventTeamMission_checking_user_id] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_EventTeamTask_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_EventTeamTask_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTeamMission] ADD CONSTRAINT [PK_EventTeamTask] PRIMARY KEY CLUSTERED ([EventTeamMission_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTeamMission] ADD CONSTRAINT [FK_EventTeamMission_EventMission] FOREIGN KEY ([EventMission_id]) REFERENCES [dbo].[EventMission] ([EventMission_id])
GO
ALTER TABLE [dbo].[EventTeamMission] ADD CONSTRAINT [FK_EventTeamMission_EventTeam_id] FOREIGN KEY ([EventTeam_id]) REFERENCES [dbo].[EventTeam] ([EventTeam_id])
GO
ALTER TABLE [dbo].[EventTeamMission] ADD CONSTRAINT [FK_EventTeamMission_EventTeamMission_status_id] FOREIGN KEY ([EventTeamMission_status_id]) REFERENCES [dbo].[EventTeamMissionStatus] ([EventTeamMission_status_id])
GO
ALTER TABLE [dbo].[EventTeamMission] ADD CONSTRAINT [FK_EventTeamMission_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[EventTeamMission] ADD CONSTRAINT [FK_EventTeamMission_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventTeamMission] ADD CONSTRAINT [FK_EventTeamMission_user1] FOREIGN KEY ([update_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventTeamMission] ADD CONSTRAINT [FK_EventTeamMission_user2] FOREIGN KEY ([EventTeamMission_checking_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
