﻿CREATE TABLE Account
(
	ID_ACCOUNT INT NOT NULL PRIMARY KEY,	
	FIRST_NAME VARCHAR(15),
	LAST_NAME VARCHAR(15),
	USERNAME VARCHAR(15),
	ACCOUNT_PASSWORD VARCHAR(20),
	ACTIVE BIT
)

INSERT INTO Account (ID_ACCOUNT, FIRST_NAME, LAST_NAME, USERNAME, ACCOUNT_PASSWORD, ACTIVE) 
VALUES (1, 'Test', 'Son', 'Test', '1', 1)

INSERT INTO Account (ID_ACCOUNT, FIRST_NAME, LAST_NAME, USERNAME, ACCOUNT_PASSWORD, ACTIVE) 
VALUES (2, 'Aaron', 'Crvl', 'AaronCrvl', '2', 1)