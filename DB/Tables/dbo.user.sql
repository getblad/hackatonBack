CREATE TABLE [dbo].[user]
(
[user_id] [int] NOT NULL IDENTITY(1, 1),
[user_first_name] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_second_name] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_email] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[role_id] [int] NOT NULL,
[user_avatar] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[team_id] [int] NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_user_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_user_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([user_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [IX_user_1] UNIQUE NONCLUSTERED ([user_email]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [FK_user_role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [FK_user_team_team_id] FOREIGN KEY ([team_id]) REFERENCES [dbo].[team] ([team_id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'To store url to avatar image', 'SCHEMA', N'dbo', 'TABLE', N'user', 'COLUMN', N'user_avatar'
GO
