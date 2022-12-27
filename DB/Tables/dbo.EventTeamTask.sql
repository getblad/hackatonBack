CREATE TABLE [dbo].[EventTeamTask]
(
[EventTeamTask_id] [int] NOT NULL,
[EventTeam_id] [int] NOT NULL,
[EventTask_id] [int] NOT NULL,
[EventTeamTask_start_time] [datetime] NOT NULL,
[EventTeamTask_end_time] [datetime] NULL,
[EventTeamTask_status_id] [int] NOT NULL,
[EventTeamTask_urlGitHub] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NULL,
[EventTeamTask_checking_user_id] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_EventTeamTask_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_EventTeamTask_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTeamTask] ADD CONSTRAINT [PK_EventTeamTask] PRIMARY KEY CLUSTERED ([EventTeamTask_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTeamTask] ADD CONSTRAINT [FK_EventTeamTask_EventTask] FOREIGN KEY ([EventTask_id]) REFERENCES [dbo].[EventTask] ([EventTask_id])
GO
ALTER TABLE [dbo].[EventTeamTask] ADD CONSTRAINT [FK_EventTeamTask_EventTeam_id] FOREIGN KEY ([EventTeam_id]) REFERENCES [dbo].[EventTeam] ([EventTeam_id])
GO
ALTER TABLE [dbo].[EventTeamTask] ADD CONSTRAINT [FK_EventTeamTask_EventTeamTask_status_id] FOREIGN KEY ([EventTeamTask_status_id]) REFERENCES [dbo].[EventTeamTaskStatus] ([EventTeamTask_status_id])
GO
ALTER TABLE [dbo].[EventTeamTask] ADD CONSTRAINT [FK_EventTeamTask_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[EventTeamTask] ADD CONSTRAINT [FK_EventTeamTask_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventTeamTask] ADD CONSTRAINT [FK_EventTeamTask_user1] FOREIGN KEY ([update_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventTeamTask] ADD CONSTRAINT [FK_EventTeamTask_user2] FOREIGN KEY ([EventTeamTask_checking_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
