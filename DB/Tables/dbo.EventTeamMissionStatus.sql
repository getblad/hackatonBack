CREATE TABLE [dbo].[EventTeamMissionStatus]
(
[EventTeamMission_status_id] [int] NOT NULL IDENTITY(1, 1),
[EventTeamMission_status_name] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTeamMissionStatus] ADD CONSTRAINT [PK_EventTeamTaskStatus] PRIMARY KEY CLUSTERED ([EventTeamMission_status_id]) ON [PRIMARY]
GO
