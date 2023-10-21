CREATE TABLE [dbo].[U_USER]
(
	[u_id]				INT				NOT NULL PRIMARY KEY,
	[u_firstname]		VARCHAR(100)	NOT NULL,
	[u_lastname]		VARCHAR(100)	NOT NULL,
	[u_email]			VARCHAR(100)	NOT NULL UNIQUE,
	[u_password_hash]	VARCHAR(60)		NOT NULL,
	FOREIGN KEY ([u_id]) REFERENCES [dbo].[D_DATA]([d_id]),
	UNIQUE ([u_firstname],[u_lastname],[u_email]),
)
