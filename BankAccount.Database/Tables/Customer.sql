CREATE TABLE Customer (
    CustomerId      INT IDENTITY(1,1)   NOT NULL,
    FirstName       VARCHAR(100)        NOT NULL,
    LastName        VARCHAR(100)        NOT NULL,
    Email           VARCHAR(100)            NULL,
    PhoneNumber     VARCHAR(20)             NULL,
    DateOfBirth     DATE                NOT NULL,
    CreatedAt       DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt       DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT PK_Customer_CustomerId PRIMARY KEY CLUSTERED (CustomerId)
    
);
