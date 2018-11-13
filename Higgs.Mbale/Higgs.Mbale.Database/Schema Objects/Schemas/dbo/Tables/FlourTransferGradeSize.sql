CREATE TABLE [dbo].[FlourTransferGradeSize]
(
	[FlourTransferId] BIGINT NOT NULL,
	[GradeId] BIGINT NOT NULL,
	[SizeId] BIGINT NOT NULL,
	[StoreId] BIGINT NOT NULL,
	[Quantity] FLOAT NOT NULL,
	[Rate]		FLOAT NOT NULL,
	[Amount]    FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.FlourTransferGradeSize] PRIMARY KEY CLUSTERED 
(
	[FlourTransferId] ASC,
	[GradeId] ASC,
	[SizeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FlourTransferGradeSize]  ADD  CONSTRAINT [FK_dbo.FlourTransferGradeSize_dbo.FlourTransfer_FlourTransferId] FOREIGN KEY([FlourTransferId])
REFERENCES [dbo].[FlourTransfer] ([FlourTransferId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[FlourTransferGradeSize] CHECK CONSTRAINT [FK_dbo.FlourTransferGradeSize_dbo.FlourTransfer_FlourTransferId]
GO

ALTER TABLE [dbo].[FlourTransferGradeSize]  ADD  CONSTRAINT [FK_dbo.FlourTransferGradeSize_dbo.Size_SizeId] FOREIGN KEY([SizeId])
REFERENCES  [dbo].[Size] ([SizeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[FlourTransferGradeSize] CHECK CONSTRAINT [FK_dbo.FlourTransferGradeSize_dbo.Size_SizeId]
GO


ALTER TABLE [dbo].[FlourTransferGradeSize]  ADD  CONSTRAINT [FK_dbo.FlourTransferGradeSize_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[FlourTransferGradeSize] CHECK CONSTRAINT [FK_dbo.FlourTransferGradeSize_dbo.Grade_GradeId]
GO


ALTER TABLE [dbo].[FlourTransferGradeSize]  ADD  CONSTRAINT [FK_dbo.FlourTransferGradeSize_dbo.Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES  [dbo].[Store] ([StoreId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[FlourTransferGradeSize] CHECK CONSTRAINT [FK_dbo.FlourTransferGradeSize_dbo.Store_StoreId]
GO

