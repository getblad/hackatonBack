CREATE TABLE [dbo].[team]
(
[team_id] [int] NOT NULL,
[team_name] [varchar] (30) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[team] ADD CONSTRAINT [PK_team] PRIMARY KEY CLUSTERED ([team_id]) ON [PRIMARY]
GO
