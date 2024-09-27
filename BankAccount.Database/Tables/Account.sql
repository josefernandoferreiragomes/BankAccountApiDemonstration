CREATE TABLE Account (
    AccountId   INT IDENTITY (1,1)                 NOT NULL,
    CustomerId  INT                                NOT NULL,
    AccountType VARCHAR(50)                        NOT NULL,
    Balance     DECIMAL(15, 2) DEFAULT 0.00        NOT NULL,
    Currency    VARCHAR(10) DEFAULT 'USD'          NOT NULL,
    CreatedAt   DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedAt   DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CONSTRAINT PK_Account_AccountId PRIMARY KEY CLUSTERED (AccountId),
    CONSTRAINT FK_Account_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

