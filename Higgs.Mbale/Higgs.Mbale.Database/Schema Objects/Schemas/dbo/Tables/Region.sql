CREATE TABLE [dbo].[Region]
(
	[RegionId] BIGINT IDENTITY(1,1) NOT NULL,	
	[Name]  [nvarchar](128) NOT NULL,
	[CreatedOn]	[datetime] NOT NULL,

    CONSTRAINT [PK_dbo.Region] PRIMARY KEY CLUSTERED 
(
	[RegionId] ASC
),

)ON [PRIMARY]

