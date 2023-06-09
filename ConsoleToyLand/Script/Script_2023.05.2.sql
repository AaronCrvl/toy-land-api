﻿CREATE TABLE ClientOrder
(
	ID_CLIENT_ORDER INT NOT NULL PRIMARY KEY,		
	ID_ACCOUNT INT NOT NULL FOREIGN KEY References Account(ID_ACCOUNT),		
	ID_PRODUCT INT NOT NULL FOREIGN KEY References Product(ID_PRODUCT),	
	FINISHED BIT NOT NULL DEFAULT(0)
)

INSERT INTO [ClientOrder] (ID_CLIENT_ORDER, ID_ACCOUNT, ID_PRODUCT, FINISHED)
VALUES (1, 1, 1, 0)
INSERT INTO [ClientOrder] (ID_CLIENT_ORDER, ID_ACCOUNT, ID_PRODUCT, FINISHED)
VALUES (2, 1, 2, 0)
INSERT INTO [ClientOrder] (ID_CLIENT_ORDER, ID_ACCOUNT, ID_PRODUCT, FINISHED)
VALUES (3, 1, 3, 0)

CREATE TABLE ProductOrder
(
	ID_PRODUCT_ORDER INT NOT NULL PRIMARY KEY,	
	ID_CLIENT_ORDER INT NOT NULL FOREIGN KEY References ClientOrder(ID_CLIENT_ORDER),		
	ID_STATUS_ORDER INT NOT NULL,		
	PRODUCT_NAME VARCHAR(30),
	EMAIL VARCHAR(50),
	CLIENT_LOCATION VARCHAR(50) NOT NULL,
	HASH_CODE VARCHAR(50)	
)

INSERT INTO ProductOrder (ID_PRODUCT_ORDER, ID_CLIENT_ORDER, ID_STATUS_ORDER, PRODUCT_NAME, EMAIL, CLIENT_LOCATION, HASH_CODE)
VALUES (1, 1, 2, 'LEGO Classic', 'teste@gmail.com', 'St, Brazil', '12775767634')

INSERT INTO ProductOrder (ID_PRODUCT_ORDER, ID_CLIENT_ORDER, ID_STATUS_ORDER, PRODUCT_NAME, EMAIL, CLIENT_LOCATION, HASH_CODE)
VALUES (1, 2, 1, 'LEGO City', 'teste@gmail.com', 'St, Brazil', '545454')

INSERT INTO ProductOrder (ID_PRODUCT_ORDER, ID_CLIENT_ORDER, ID_STATUS_ORDER, PRODUCT_NAME, EMAIL, CLIENT_LOCATION, HASH_CODE)
VALUES (1, 3, 3, 'LEGO Jurassic Park', 'teste@gmail.com', 'St, Brazil', '135454689')