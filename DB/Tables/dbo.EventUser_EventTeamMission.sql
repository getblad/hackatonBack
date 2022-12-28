CREATE TABLE [dbo].[EventUser_EventTeamMission]
(
[EventUser_EventTeamMission] [int] NOT NULL IDENTITY(1, 1),
[EventUser_id] [int] NOT NULL,
[EventTeamMission_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventUser_EventTeamMission] ADD CONSTRAINT [PK_EventUser_EventTeamMission] PRIMARY KEY CLUSTERED ([EventUser_EventTeamMission]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventUser_EventTeamMission] ADD CONSTRAINT [FK_EventUser_event_user_id] FOREIGN KEY ([EventUser_id]) REFERENCES [dbo].[EventUser] ([EventUser_id])
GO
ALTER TABLE [dbo].[EventUser_EventTeamMission] ADD CONSTRAINT [FK_EventUser_EventTeamMission_id] FOREIGN KEY ([EventTeamMission_id]) REFERENCES [dbo].[EventTeamMission] ([EventTeamMission_id])
GO
