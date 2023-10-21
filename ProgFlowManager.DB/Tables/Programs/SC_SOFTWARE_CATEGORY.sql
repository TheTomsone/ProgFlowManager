CREATE TABLE [dbo].[SC_SOFTWARE_CATEGORY]
(
	[sc_software_id]		INT NOT NULL,
	[sc_category_id]	INT NOT NULL,
	PRIMARY KEY ([sc_software_id],[sc_category_id]),
	FOREIGN KEY ([sc_software_id]) REFERENCES [dbo].[S_SOFTWARE]([s_id]),
	FOREIGN KEY ([sc_category_id]) REFERENCES [dbo].[C_CATEGORY]([c_id]),
)
