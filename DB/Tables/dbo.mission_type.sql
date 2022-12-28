CREATE TABLE [dbo].[mission_type]
(
[mission_type_id] [int] NOT NULL IDENTITY(1, 1),
[mission_type_name] [varchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[mission_type] ADD CONSTRAINT [PK_task_type] PRIMARY KEY CLUSTERED ([mission_type_id]) ON [PRIMARY]
GO
