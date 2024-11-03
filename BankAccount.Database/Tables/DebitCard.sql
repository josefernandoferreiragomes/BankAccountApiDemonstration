CREATE TABLE DebitCard (
    DebitCardId   INT IDENTITY (1,1)                  NOT NULL,
    AccountId     INT                                 NOT NULL,
    DebitCardType VARCHAR(50)                         NOT NULL,
    MaximumAmount DECIMAL(15, 2)                      NOT NULL,
    CreatedAt     DATETIME DEFAULT CURRENT_TIMESTAMP  NOT NULL,
    CONSTRAINT PK_DebitCard_DebitCardId PRIMARY KEY CLUSTERED (DebitCardId),
    CONSTRAINT FK_DebitCard_Account FOREIGN KEY (AccountId) REFERENCES Account(AccountId)
);
