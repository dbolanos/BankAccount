USE [BankAccountsAPI]
GO
/****** Object:  StoredProcedure [dbo].[TransferenceBetweenAccounts]    Script Date: 5/26/2024 7:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TransferenceBetweenAccounts]
    @OriginAccountId INT,
    @DestinationAccountId INT,
    @Amount DECIMAL(18, 2),
    @OriginAccountBalance DECIMAL(18, 2) OUTPUT 
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Origin Account
        UPDATE Accounts
        SET Balance = Balance - @Amount
        WHERE Id = @OriginAccountId;

        -- Destination Account
        UPDATE Accounts
        SET Balance = Balance + @Amount
        WHERE Id = @DestinationAccountId;

        -- Get Balance Origen Account
        SELECT @OriginAccountBalance = Balance
        FROM Accounts
        WHERE Id = @OriginAccountId;

       
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Revert if exist error
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

GO
