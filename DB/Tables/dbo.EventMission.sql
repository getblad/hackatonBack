CREATE TABLE [dbo].[EventMission]
(
[EventMission_id] [int] NOT NULL IDENTITY(1, 1),
[event_id] [int] NOT NULL,
[mission_id] [int] NOT NULL,
[EventMission_language] [varchar] (200) COLLATE Cyrillic_General_CI_AS NULL,
[EventMission_execution_time] [time] (2) NULL,
[EventMission_point] [int] NULL,
[EventMission_step_time_fine] [time] (2) NULL,
[EventMission_step_point_fine] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_EventTask_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_EventTask_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventMission] ADD CONSTRAINT [PK_EventTask] PRIMARY KEY CLUSTERED ([EventMission_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventMission] ADD CONSTRAINT [FK_EvenMission_event_id] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[EventMission] ADD CONSTRAINT [FK_EventMission_mission_id] FOREIGN KEY ([mission_id]) REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[EventMission] ADD CONSTRAINT [FK_EventMission_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[EventMission] ADD CONSTRAINT [FK_EventMission_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventMission] ADD CONSTRAINT [FK_EventMission_user1] FOREIGN KEY ([update_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
