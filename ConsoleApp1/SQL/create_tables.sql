USE [igfindb]
GO

/****** Object:  Table [dbo].[simpletransaction]    Script Date: 4/9/2019 10:10:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[simpletransaction](
	[id] [int] NOT NULL,
	[charge_amount] [decimal](18, 6) NOT NULL,
	[fee_amount] [decimal](18, 6) NOT NULL,
	[event_status] [tinyint] NOT NULL,
 CONSTRAINT [PK__id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
