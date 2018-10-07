﻿CREATE TABLE [dbo].[BuveraGradeSize]
(
	[BuveraId] BIGINT NOT NULL,
	[GradeId] BIGINT NOT NULL,
	[SizeId] BIGINT NOT NULL,
	[StoreId] BIGINT NOT NULL,
	[Quantity] FLOAT NOT NULL,
	[Rate]		FLOAT NOT NULL,
	[Amount]    FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.BuveraGradeSize] PRIMARY KEY CLUSTERED 
(
	[BuveraId] ASC,
	[GradeId] ASC,
	[SizeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BuveraGradeSize]  ADD  CONSTRAINT [FK_dbo.BuveraGradeSize_dbo.Buvera_BuveraId] FOREIGN KEY([BuveraId])
REFERENCES [dbo].[Buvera] ([BuveraId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BuveraGradeSize] CHECK CONSTRAINT [FK_dbo.BuveraGradeSize_dbo.Buvera_BuveraId]
GO

ALTER TABLE [dbo].[BuveraGradeSize]  ADD  CONSTRAINT [FK_dbo.BuveraGradeSize_dbo.Size_SizeId] FOREIGN KEY([SizeId])
REFERENCES  [dbo].[Size] ([SizeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BuveraGradeSize] CHECK CONSTRAINT [FK_dbo.BuveraGradeSize_dbo.Size_SizeId]
GO


ALTER TABLE [dbo].[BuveraGradeSize]  ADD  CONSTRAINT [FK_dbo.BuveraGradeSize_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BuveraGradeSize] CHECK CONSTRAINT [FK_dbo.BuveraGradeSize_dbo.Grade_GradeId]
GO


ALTER TABLE [dbo].[BuveraGradeSize]  ADD  CONSTRAINT [FK_dbo.BuveraGradeSize_dbo.Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES  [dbo].[Store] ([StoreId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BuveraGradeSize] CHECK CONSTRAINT [FK_dbo.BuveraGradeSize_dbo.Store_StoreId]
GO

