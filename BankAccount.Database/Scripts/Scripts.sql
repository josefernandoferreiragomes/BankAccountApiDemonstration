USE BankAccount

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

-- log
SELECT 
	 #ReportCustomerAccountTransaction.* 
	,DENSE_RANK () OVER (ORDER BY #ReportCustomerAccountTransaction.FirstName) AS DenseRankCustomerAccountTransaction
	,RANK () OVER (ORDER BY #ReportCustomerAccountTransaction.FirstName) AS RankCustomerAccountTransaction
	,ROW_NUMBER () OVER (ORDER BY #ReportCustomerAccountTransaction.FirstName) AS RowNumberCustomerAccountTransaction
FROM #ReportCustomerAccountTransaction

-- remove duplicates based on creteria
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

--log again
SELECT 
	 #ReportCustomerAccountTransaction.* 
FROM #ReportCustomerAccountTransaction

DROP TABLE #ReportCustomerAccountTransaction