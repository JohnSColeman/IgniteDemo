USE [igfindb]
GO

/****** Object:  StoredProcedure [dbo].[delete_simple_transaction]    Script Date: 4/9/2019 2:29:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[delete_simple_transaction]
	(@id int)
AS
BEGIN
	DELETE FROM [dbo].[simpletransaction] WHERE id = @id
END
GO

CREATE PROCEDURE [dbo].[select_simple_transaction]
	(@id int)
AS
BEGIN
	SELECT id, charge_amount, fee_amount, event_status FROM [dbo].[simpletransaction] WHERE id = @id
END
GO

CREATE PROCEDURE [dbo].[updateorinsert_simple_transaction]
	(@id int
	, @charge_amount decimal(18,6)
	, @fee_amount decimal(18,6)
	, @event_status tinyint)
AS
BEGIN TRANSACTION
	UPDATE [dbo].[simpletransaction] SET charge_amount = @charge_amount, fee_amount = @fee_amount, event_status = @event_status
	 WHERE id = @id
IF @@ROWCOUNT = 0
	INSERT INTO [dbo].[simpletransaction] (id, charge_amount, fee_amount, event_status)
	 VALUES(@id, @charge_amount, @fee_amount, @event_status)
COMMIT TRANSACTION
GO