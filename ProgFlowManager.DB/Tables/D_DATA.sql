CREATE TABLE [dbo].[D_DATA]
(
	[d_id]			INT				NOT NULL PRIMARY KEY IDENTITY,
	[d_name]		VARCHAR(50)		NOT NULL,
	[d_resume]		VARCHAR(MAX)	NULL,
	[d_created]		DATETIME2		NOT NULL DEFAULT GETDATE(),
	[d_updated]		DATETIME2		NULL,
	[d_image_data]	VARBINARY(MAX)	NULL,
	[d_image_mime]	NVARCHAR(50)	NULL,
)
