CREATE TABLE [dbo].[EventUser]
(
[EventUser_id] [int] NOT NULL IDENTITY(1, 1),
[event_id] [int] NOT NULL,
[user_id] [int] NOT NULL,
[EventTeam_id] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_EventUser_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_EventUser_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [PK_EventUser] PRIMARY KEY CLUSTERED ([EventUser_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [FK_EventUser_Event_event_id] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [FK_EventUser_EventTeam_id] FOREIGN KEY ([EventTeam_id]) REFERENCES [dbo].[EventTeam] ([EventTeam_id])
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [FK_EventUser_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [FK_EventUser_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [FK_EventUser_user_id] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [FK_EventUser_user1] FOREIGN KEY ([update_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
