CREATE TABLE [dbo].[EventTask]
(
[EventTask_id] [int] NOT NULL IDENTITY(1, 1),
[event_id] [int] NOT NULL,
[task_id] [int] NOT NULL,
[EventTask_language] [varchar] (200) COLLATE Cyrillic_General_CI_AS NULL,
[EventTask_execution_time] [time] (2) NULL,
[EventTask_point] [int] NULL,
[EventTask_step_time_fine] [time] (2) NULL,
[EventTask_step_point_fine] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_EventTask_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_EventTask_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTask] ADD CONSTRAINT [PK_EventTask] PRIMARY KEY CLUSTERED ([EventTask_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTask] ADD CONSTRAINT [FK_EvenTask_event_id] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[EventTask] ADD CONSTRAINT [FK_EventTask_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[EventTask] ADD CONSTRAINT [FK_EventTask_task_id] FOREIGN KEY ([task_id]) REFERENCES [dbo].[task] ([task_id])
GO
ALTER TABLE [dbo].[EventTask] ADD CONSTRAINT [FK_EventTask_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventTask] ADD CONSTRAINT [FK_EventTask_user1] FOREIGN KEY ([update_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
