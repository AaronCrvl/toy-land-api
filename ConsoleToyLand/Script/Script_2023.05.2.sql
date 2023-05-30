﻿CREATE TABLE Client_Order
(
	ID_CLIENT_ORDER INT NOT NULL PRIMARY KEY,	
	ID_ACCOUNT INT NOT NULL FOREIGN KEY References Account(ID_ACCOUNT),		
	ID_PRODUCT INT NOT NULL FOREIGN KEY References Product(ID_PRODUCT),	
	FINISHED BIT
)