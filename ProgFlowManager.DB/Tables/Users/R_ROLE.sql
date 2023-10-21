CREATE TABLE [dbo].[R_ROLE]
(
	[r_id] INT NOT NULL PRIMARY KEY,
	[r_authorized_id] INT NOT NULL,
	FOREIGN KEY ([r_id]) REFERENCES [dbo].[TC_TEAM_CATEGORY]([tc_id]),
	FOREIGN KEY ([r_authorized_id]) REFERENCES [dbo].[A_AUTHORIZED](a_id),
)
