CREATE TABLE [dbo].[S_SOFTWARE]
(
	[s_id]		INT			NOT NULL PRIMARY KEY,
	[s_eta]		DATETIME2	NULL,
	[s_started] DATETIME2	NOT NULL,
	[s_user_id] INT			NULL,
	[s_team_id] INT			NULL,
	UNIQUE ([s_id],[s_user_id]),
	FOREIGN KEY ([s_id]) REFERENCES [dbo].[D_DATA]([d_id]),
	FOREIGN KEY ([s_user_id]) REFERENCES [dbo].[U_USER]([u_id]),
	FOREIGN KEY ([s_team_id]) REFERENCES [dbo].[T_TEAM]([t_id]),
)
