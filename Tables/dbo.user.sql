CREATE TABLE [dbo].[user]
(
[user_id] [int] NOT NULL IDENTITY(1, 1),
[user_login] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_first_name] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_second_name] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_email] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[user_date_registration] [datetime] NOT NULL,
[user_status] [int] NOT NULL,
[user_role] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([user_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [IX_user] UNIQUE NONCLUSTERED ([user_login]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [FK_user_role] FOREIGN KEY ([user_role]) REFERENCES [dbo].[role] ([role_id])
GO
