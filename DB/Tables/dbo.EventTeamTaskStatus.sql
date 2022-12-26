CREATE TABLE [dbo].[EventTeamTaskStatus]
(
[EventTeamTask_status_id] [int] NOT NULL IDENTITY(1, 1),
[EventTeamTask_status_name] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTeamTaskStatus] ADD CONSTRAINT [PK_EventTeamTaskStatus] PRIMARY KEY CLUSTERED ([EventTeamTask_status_id]) ON [PRIMARY]
GO
