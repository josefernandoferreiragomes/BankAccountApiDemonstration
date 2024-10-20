USE BankAccount
GO

-- Sample data for Customer
IF NOT EXISTS(
    SELECT 1 FROM Customer
)
BEGIN
    INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, DateOfBirth)
    VALUES
        ('John', 'Doe', 'john.doe@example.com', '1234567890', '1985-05-15'),
        ('Jane', 'Smith', 'jane.smith@example.com', '0987654321', '1990-08-22'),
        ('Alice', 'Johnson', 'alice.j@example.com', '1122334455', '1992-11-12');
END
GO

-- Sample data for Account
IF NOT EXISTS(
    SELECT 1 FROM Account
)
BEGIN
    INSERT INTO Account (CustomerId, AccountType, Balance, Currency)
    VALUES
        (1, 'Checking', 1500.00, 'USD'),
        (1, 'Savings', 2500.50, 'USD'),
        (2, 'Checking', 1200.75, 'USD'),
        (3, 'Savings', 500.00, 'USD');
END

-- Sample data for AccountTransaction
IF NOT EXISTS(
    SELECT 1 FROM AccountTransaction
)
BEGIN
    INSERT INTO AccountTransaction (AccountId, TransactionType, Amount, Description)
    VALUES
        (1, 'Deposit', 500.00, 'Direct deposit from employer'),
        (1, 'Withdrawal', 200.00, 'ATM withdrawal'),
        (2, 'Deposit', 1000.50, 'Transfer from another account'),
        (2, 'Withdrawal', 300.00, 'Payment for utility bill'),
        (3, 'Deposit', 500.00, 'Check deposit'),
        (4, 'Withdrawal', 50.00, 'Online purchase');
END

-- Sample data for DebitCard
IF NOT EXISTS (
    SELECT 1 FROM DebitCard
)
BEGIN
    INSERT INTO DebitCard (AccountId, DebitCardType, MaximumAmount, CreatedAt)
    VALUES
        (1, 'Standard', 1000.00, GETDATE()),
        (1, 'Premium', 2500.00, GETDATE()),
        (2, 'Standard', 1500.00, GETDATE()),
        (3, 'Standard', 500.00, GETDATE());
END
