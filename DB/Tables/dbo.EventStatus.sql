CREATE TABLE [dbo].[EventStatus]
(
[event_status_id] [int] NOT NULL IDENTITY(1, 1),
[EventStatus_name] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventStatus] ADD CONSTRAINT [PK_EventStatus] PRIMARY KEY CLUSTERED ([event_status_id]) ON [PRIMARY]
GO
