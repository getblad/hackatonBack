CREATE TABLE [dbo].[row_status]
(
[row_status_id] [int] NOT NULL IDENTITY(0, 1),
[row_status_name] [varchar] (30) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[row_status] ADD CONSTRAINT [PK_row_status] PRIMARY KEY CLUSTERED ([row_status_id]) ON [PRIMARY]
GO
