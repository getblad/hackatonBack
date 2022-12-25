CREATE TABLE [dbo].[user]
(
[user_id] [int] NOT NULL IDENTITY(1, 1),
[user_first_name] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_second_name] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_email] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_date_registration] [timestamp] NOT NULL,
[user_status] [int] NOT NULL,
[user_role] [int] NOT NULL,
[user_avatar] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([user_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [IX_user_1] UNIQUE NONCLUSTERED ([user_email]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [FK_user_role] FOREIGN KEY ([user_role]) REFERENCES [dbo].[role] ([role_id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'To store url to avatar image', 'SCHEMA', N'dbo', 'TABLE', N'user', 'COLUMN', N'user_avatar'
GO