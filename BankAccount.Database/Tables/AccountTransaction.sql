CREATE TABLE AccountTransaction (
    TransactionId   INT IDENTITY (1,1)                  NOT NULL,
    AccountId       INT                                 NOT NULL,
    TransactionType VARCHAR(50)                         NOT NULL,
    Amount          DECIMAL(15, 2)                      NOT NULL,
    TransactionDate DATETIME DEFAULT CURRENT_TIMESTAMP  NOT NULL,
    [Description]   VARCHAR(255)                        NOT NULL,
    CreatedAt       DATETIME DEFAULT CURRENT_TIMESTAMP  NOT NULL,
    CONSTRAINT PK_AccountTransaction_TransactionId PRIMARY KEY CLUSTERED (TransactionId),
    CONSTRAINT FK_AccountTransaction_Account FOREIGN KEY (AccountId) REFERENCES Account(AccountId)
);
