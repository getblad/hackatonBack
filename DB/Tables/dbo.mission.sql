CREATE TABLE [dbo].[mission]
(
[mission_id] [int] NOT NULL,
[mission_type_id] [int] NOT NULL,
[mission_name] [varchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
[mission_description] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[mission_language] [varchar] (200) COLLATE Cyrillic_General_CI_AS NOT NULL,
[mission_author] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[mission_url_file_description] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[mission_execution_time] [time] (2) NOT NULL,
[mission_point] [int] NOT NULL,
[mission_step_time_fine] [time] (2) NULL,
[mission_step_point_fine] [int] NULL,
[mission_main_task_id] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_task_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_task_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[mission] ADD CONSTRAINT [PK_task] PRIMARY KEY CLUSTERED ([mission_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[mission] ADD CONSTRAINT [IX_task] UNIQUE NONCLUSTERED ([mission_name]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[mission] ADD CONSTRAINT [FK_mission_mission_type_id] FOREIGN KEY ([mission_type_id]) REFERENCES [dbo].[mission_type] ([mission_type_id])
GO
ALTER TABLE [dbo].[mission] ADD CONSTRAINT [FK_mission_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[mission] ADD CONSTRAINT [FK_mission_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[mission] ADD CONSTRAINT [FK_mission_user1] FOREIGN KEY ([update_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
