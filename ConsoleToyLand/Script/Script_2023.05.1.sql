﻿CREATE TABLE Account
(
	ID_ACCOUNT INT NOT NULL PRIMARY KEY,	
	ACCOUNT_NAME VARCHAR(15),
	ACCOUNT_PASSWORD VARCHAR(20),
	ACTIVE BIT
)

INSERT INTO Account (ID_ACCOUNT, ACCOUNT_NAME, ACCOUNT_PASSWORD, ACTIVE) 
VALUES (1, 'Test', '1', 1)

INSERT INTO Account (ID_ACCOUNT, ACCOUNT_NAME, ACCOUNT_PASSWORD, ACTIVE) 
VALUES (2, 'Aaron', '2', 1)