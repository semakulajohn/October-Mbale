CREATE TABLE [dbo].[DeliveryBatch]
(
	[BatchId]    BIGINT NOT NULL,
	[DeliveryId] BIGINT NOT NULL,
	[BatchQuantity] FLOAT NOT NULL,
	[Price]         FLOAT NULL,
	[CreatedOn]	[datetime] NOT NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	

  CONSTRAINT [PK_dbo.DeliveryBatch] PRIMARY KEY CLUSTERED 
(
	[DeliveryId] ASC,
	[BatchId]  ASC
),
CONSTRAINT [FK_DeliveryBatch_BatchId] FOREIGN KEY([BatchId]) REFERENCES [dbo].[Batch](BatchId),
CONSTRAINT [FK_DeliveryBatch_DeliveryId] FOREIGN KEY([DeliveryId]) REFERENCES [dbo].[Delivery](DeliveryId),
)ON [PRIMARY]