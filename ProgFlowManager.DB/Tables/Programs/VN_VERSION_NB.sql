CREATE TABLE [dbo].[VN_VERSION_NB]
(
	[vn_id]			INT			NOT NULL PRIMARY KEY,
	[vn_major]		INT			NOT NULL DEFAULT 0,
	[vn_minor]		INT			NOT NULL DEFAULT 0,
	[vn_patch]		INT			NOT NULL DEFAULT 0,
	[vn_goal]		DATETIME2	NULL,
	[vn_release]	DATETIME2	NULL,
	[vn_stage_id]	INT			NOT NULL,
	[vn_software_id] INT		NOT NULL,
	UNIQUE ([vn_major],[vn_minor],[vn_patch],[vn_software_id]),
	FOREIGN KEY ([vn_id])			REFERENCES [dbo].[D_DATA]([d_id]),
	FOREIGN KEY ([vn_stage_id])		REFERENCES [dbo].[S_STAGE](s_id),
	FOREIGN KEY ([vn_software_id])	REFERENCES [dbo].[S_SOFTWARE]([s_id]),
)
