CREATE TABLE [dbo].[SL_SOFTWARE_LANGUAGE]
(
	[sl_software_id]	INT NOT NULL,
	[sl_language_id]	INT NOT NULL,
	PRIMARY KEY ([sl_software_id],[sl_language_id]),
	FOREIGN KEY ([sl_software_id])	REFERENCES [dbo].[S_SOFTWARE]([s_id]),
	FOREIGN KEY ([sl_language_id])	REFERENCES [dbo].[L_LANGUAGE]([l_id]),
)
