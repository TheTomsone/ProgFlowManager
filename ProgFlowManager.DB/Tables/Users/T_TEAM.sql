CREATE TABLE [dbo].[T_TEAM]
(
	[t_id] INT NOT NULL PRIMARY KEY,
	[t_team_category_id] INT NOT NULL,
	FOREIGN KEY ([t_id]) REFERENCES [dbo].[D_DATA]([d_id]),
	FOREIGN KEY ([t_team_category_id]) REFERENCES [dbo].[TC_TEAM_CATEGORY](tc_id),
)
