CREATE TABLE [dbo].[task]
(
[task_id] [int] NOT NULL,
[task_name] [varchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
[task_description] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL,
[task_author] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[task_url_file_description] [varchar] (1000) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[task] ADD CONSTRAINT [PK_task] PRIMARY KEY CLUSTERED ([task_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[task] ADD CONSTRAINT [IX_task] UNIQUE NONCLUSTERED ([task_name]) ON [PRIMARY]
GO
