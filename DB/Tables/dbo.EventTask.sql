CREATE TABLE [dbo].[EventTask]
(
[event_id] [int] NOT NULL,
[task_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventTask] ADD CONSTRAINT [FK_EvenTask_event_id] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[EventTask] ADD CONSTRAINT [FK_EventTask_task_id] FOREIGN KEY ([task_id]) REFERENCES [dbo].[task] ([task_id])
GO
