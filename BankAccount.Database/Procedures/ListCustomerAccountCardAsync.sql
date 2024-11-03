CREATE PROCEDURE [dbo].[ListCustomerAccountCardAsync]
	@customerId int
AS
	
	SELECT 
		 Customer.FirstName 
		,Customer.LastName
		,Account.AccountType
		,Account.Balance
		,AccountTransaction.Amount
		,AccountTransaction.TransactionDate
		,DebitCard.DebitCardType
		,DebitCard.MaximumAmount
	INTO #ReportCustomerAccountTransaction
	FROM Customer
	LEFT JOIN Account ON (Account.CustomerId = Customer.CustomerId)
	LEFT JOIN AccountTransaction ON (AccountTransaction.AccountId = Account.AccountId)
	LEFT JOIN DebitCard ON (DebitCard.AccountId = Account.AccountId)

	-- REMOVE DUPLICATES BASED ON CRITERIA
	DELETE FROM T
	FROM(
	SELECT 
		 #ReportCustomerAccountTransaction.* 	
		,DupRank = ROW_NUMBER () OVER (
			PARTITION BY #ReportCustomerAccountTransaction.Balance
			ORDER BY (SELECT NULL)
		)
	FROM #ReportCustomerAccountTransaction
	)
	AS T
	WHERE DupRank > 1

	-- RETURN RESULTS
	SELECT 
		 #ReportCustomerAccountTransaction.* 
	FROM #ReportCustomerAccountTransaction

	DROP TABLE #ReportCustomerAccountTransaction

RETURN 0
