CREATE TABLE [dbo].[task_type]
(
[task_type_id] [int] NOT NULL IDENTITY(1, 1),
[task_type_name] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[task_type] ADD CONSTRAINT [PK_task_type] PRIMARY KEY CLUSTERED ([task_type_id]) ON [PRIMARY]
GO
