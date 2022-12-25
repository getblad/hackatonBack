CREATE TABLE [dbo].[EventUser]
(
[event_id] [int] NOT NULL,
[user_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [FK_EventUser_event_id] FOREIGN KEY ([event_id]) REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[EventUser] ADD CONSTRAINT [FK_EventUser_user_id] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
GO
