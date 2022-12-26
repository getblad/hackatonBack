CREATE TABLE [dbo].[EventUser_EventTeamTask]
(
[EventUser_id] [int] NOT NULL,
[EventTeamTask_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventUser_EventTeamTask] ADD CONSTRAINT [FK_EventUser_event_user_id] FOREIGN KEY ([EventUser_id]) REFERENCES [dbo].[EventUser] ([EventUser_id])
GO
ALTER TABLE [dbo].[EventUser_EventTeamTask] ADD CONSTRAINT [FK_EventUser_EventTeamTask_id] FOREIGN KEY ([EventTeamTask_id]) REFERENCES [dbo].[EventTeamTask] ([EventTeamTask_id])
GO
