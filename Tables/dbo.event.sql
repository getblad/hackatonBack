CREATE TABLE [dbo].[event]
(
[event_id] [int] NOT NULL IDENTITY(1, 1),
[event_name] [varchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
[event_description] [varchar] (2000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[event_start_time] [datetime] NOT NULL,
[event_end_time] [datetime] NOT NULL,
[event_created_date] [timestamp] NOT NULL,
[event_status] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[event] ADD CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED ([event_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[event] ADD CONSTRAINT [IX_event] UNIQUE NONCLUSTERED ([event_name]) ON [PRIMARY]
GO
