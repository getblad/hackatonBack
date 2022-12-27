CREATE TABLE [dbo].[task]
(
[task_id] [int] NOT NULL,
[task_type_id] [int] NOT NULL,
[task_name] [varchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
[task_description] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[task_language] [varchar] (200) COLLATE Cyrillic_General_CI_AS NOT NULL,
[task_author] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[task_url_file_description] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[task_execution_time] [time] (2) NOT NULL,
[task_point] [int] NOT NULL,
[task_step_time_fine] [time] (2) NULL,
[task_step_point_fine] [int] NULL,
[task_main_task_id] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_task_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_task_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[task] ADD CONSTRAINT [PK_task] PRIMARY KEY CLUSTERED ([task_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[task] ADD CONSTRAINT [IX_task] UNIQUE NONCLUSTERED ([task_name]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[task] ADD CONSTRAINT [FK_task_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[task] ADD CONSTRAINT [FK_task_task_type_id] FOREIGN KEY ([task_type_id]) REFERENCES [dbo].[task_type] ([task_type_id])
GO
ALTER TABLE [dbo].[task] ADD CONSTRAINT [FK_task_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[task] ADD CONSTRAINT [FK_task_user1] FOREIGN KEY ([update_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
