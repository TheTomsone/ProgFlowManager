CREATE TABLE [dbo].[TUR_TEAM_USER_ROLE]
(
	[tur_team_id] INT NOT NULL,
	[tur_user_id] INT NOT NULL,
	[tur_role_id] INT NOT NULL,
	PRIMARY KEY ([tur_team_id],[tur_user_id],[tur_role_id]),
	FOREIGN KEY ([tur_team_id]) REFERENCES [dbo].[T_TEAM]([t_id]),
	FOREIGN KEY ([tur_user_id]) REFERENCES [dbo].[U_USER]([u_id]),
	FOREIGN KEY ([tur_role_id]) REFERENCES [dbo].[R_ROLE]([r_id]),
)
