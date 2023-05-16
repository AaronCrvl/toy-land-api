--drop table Product
CREATE TABLE Product
(
	ID_PRODUCT INT,
	PRODUCT_NAME VARCHAR(40),
	SHORT_DESCRIPTION VARCHAR(300),
	IMAGE_URL VARCHAR(200)
)


INSERT INTO PRODUCT (ID_PRODUCT, PRODUCT_NAME, SHORT_DESCRIPTION, IMAGE_URL) 
VALUES (1, 'LEGO Classic', 'The classics never disapont, the classic series bring the best of LEGO toys.', 'https://m.media-amazon.com/images/I/91RaDysK8zL.jpg')

INSERT INTO PRODUCT (ID_PRODUCT, PRODUCT_NAME, SHORT_DESCRIPTION, IMAGE_URL) 
VALUES (2, 'LEGO City', 'The City series bring everything you see on the outside and more, fell free to create and build your own city.', 'https://www.lego.com/cdn/cs/set/assets/blte840fb67809da48b/60384_alt1.png')

INSERT INTO PRODUCT (ID_PRODUCT, PRODUCT_NAME, SHORT_DESCRIPTION, IMAGE_URL) 
VALUES (3, 'LEGO Jurassic Park', 'Pre-historic creatures, savanna vibes and much, much more! The Jurassic Park edition is here.', 'https://www.lego.com/cdn/cs/set/assets/blta4e7362465e23d18/76957_alt1.png')

INSERT INTO PRODUCT (ID_PRODUCT, PRODUCT_NAME, SHORT_DESCRIPTION, IMAGE_URL) 
VALUES (4, 'LEGO Creator', 'Build your own creations with this product, your imagionation is the limit. This version your the jurassic.', 'https://images.immediate.co.uk/production/volatile/sites/23/2022/09/81Qv-reRpoL.ACSL1500-08865c5.jpg')

INSERT INTO PRODUCT (ID_PRODUCT, PRODUCT_NAME, SHORT_DESCRIPTION, IMAGE_URL) 
VALUES (5, 'LEGO Creator', 'Build your own creations with this product, your imagionation is the limit. This version your the jurassic.', 'https://i.pinimg.com/originals/5f/5d/67/5f5d67d6ac873adbc72f00a5daac0403.jpg')