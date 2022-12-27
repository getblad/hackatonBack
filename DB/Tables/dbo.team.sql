CREATE TABLE [dbo].[team]
(
[team_id] [int] NOT NULL,
[team_name] [varchar] (30) COLLATE Cyrillic_General_CI_AS NOT NULL,
[team_avatar] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NULL,
[team_capitan_id] [int] NOT NULL,
[create_user_id] [int] NOT NULL,
[update_user_id] [int] NOT NULL,
[create_time] [datetime] NOT NULL CONSTRAINT [DF_team_create_time] DEFAULT (getdate()),
[update_time] [datetime] NOT NULL CONSTRAINT [DF_team_update_time] DEFAULT (getdate()),
[row_status_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[team] ADD CONSTRAINT [PK_team] PRIMARY KEY CLUSTERED ([team_id]) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_team] ON [dbo].[team] ([team_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[team] ADD CONSTRAINT [FK_team_row_status] FOREIGN KEY ([row_status_id]) REFERENCES [dbo].[row_status] ([row_status_id])
GO
ALTER TABLE [dbo].[team] ADD CONSTRAINT [FK_team_user] FOREIGN KEY ([create_user_id]) REFERENCES [dbo].[user] ([user_id])
GO
