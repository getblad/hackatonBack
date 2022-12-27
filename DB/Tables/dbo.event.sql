CREATE TABLE [dbo].[event]
(
[event_id] [int] NOT NULL IDENTITY(1, 1),
[event_name] [varchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
[event_description] [varchar] (2000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[event_start_time] [datetime] NOT NULL,
[event_end_time] [datetime] NOT NULL,
[event_created_date] [timestamp] NOT NULL,
[event_status_id] [int] NOT NULL,
[event_max_count_of_team_members] [int] NULL,
[event_min_count_of_team_members] [int] NULL,
[event_max_count_of_event_members] [int] NULL,
[event_min_count_of_event_members] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_event_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_event_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[event] ADD CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED ([event_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[event] ADD CONSTRAINT [IX_event] UNIQUE NONCLUSTERED ([event_name]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[event] ADD CONSTRAINT [FK_event_EventStatus_EventStatus_id] FOREIGN KEY ([event_status_id]) REFERENCES [dbo].[EventStatus] ([event_status_id])
GO
