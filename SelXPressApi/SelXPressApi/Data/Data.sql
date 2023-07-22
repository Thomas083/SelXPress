INSERT INTO Roles (Name)
VALUES ('Vendeur'),('SuperVendeur'),('Acheteur'),('SuperAcheteur'),('GOAT'),('SuperAdministrateur');

INSERT INTO Users (Username, Password, Email, RoleId)
VALUES ('LeBirz','MotDePasse','ugo.bertrand@epitech.eu',6),
       ('Elsharion','MotDePasse','david.vacossin@epitech.eu',4),
       ('Aliak','MotDePasse','thomas.debray@epitech.eu',1),
       ('Maxence-Leroy','MotDePasse','maxence.leroy@epitech.eu',5),
       ('Mockingame','MotDePasse','julien.lamalle@epitech.eu',2);

INSERT INTO Attributes (Name, Type, CreatedAt, UpdatedAt)
VALUES
    ('Attribute1','Type1',GETDATE(),GETDATE()),
    ('Attribute2','Type2',GETDATE(),GETDATE()),
    ('Attribute3','Type3',GETDATE(),GETDATE()),
    ('Attribute4','Type4',GETDATE(),GETDATE()),
    ('Attribute5','Type5',GETDATE(),GETDATE());

INSERT INTO Stocks (Quantity)
VALUES
    (20),
    (40),
    (60),
    (80),
    (100),
    (120);

INSERT INTO Categories (Name)
VALUES
    ('Categorie1'),
    ('Categorie2'),
    ('Categorie3'),
    ('Categorie4'),
    ('Categorie5'),
    ('Categorie6');

INSERT INTO Tags (Name,CategoryId)
VALUES
    ('Tag1Category1',1),
    ('Tag2Category1',1),
    ('Tag3Category2',2),
    ('Tag4Category2',2),
    ('Tag5Category3',3),
    ('Tag6Category3',3),
    ('Tag7Category4',4),
    ('Tag8Category4',4),
    ('Tag9Category5',5),
    ('Tag10Category5',5),
    ('Tag11Category6',6),
    ('Tag12Category6',6);

INSERT INTO Marks (rate)
VALUES
    (0),
    (1),
    (2),
    ( 3),
    (4),
    (5),
    (0),
    (1),
    (2),
    (3),
    (4),
    (5),
    (0),
    (1),
    (2),
    (3),
    (4),
    (5),
    (4),
    (3);

INSERT INTO Products (Name,Description,Price,Picture,CreatedAt,CategoryId,StockId)
VALUES
    ('SuperProduit1','Description du super produit',23,'picture',GETDATE(),1,1),
    ('Produit pas genial1','Description du produit pas genial',6,'picture',GETDATE(),2,2),
    ('Produit pas genial2','Description du produit pas genial',8,'picture',GETDATE(),3,3),
    ('SuperProduit2','Description du super produit',125,'picture',GETDATE(),4,4),
    ('ProduitIncroyable1','Description du produit incroyable',1056,'picture',GETDATE(),5,5),
    ('ProduitIncroyable2','Description du produit incroyable',1478,'picture',GETDATE(),6,6);

INSERT INTO Carts (Id,UserId,ProductId)
VALUES
    (1,1,1),
    (2,1,2),
    (3,1,3),
    (4,3,4),
    (5,5,1),
    (6,2,2),
    (7,5,6);

INSERT INTO ProductAttributes (Id,ProductId,AttributeId)
VALUES
    (1,1,1),
    (2,1,2),
    (3,2,3),
    (4,2,4),
    (5,3,5),
    (6,3,4),
    (7,4,1),
    (8,4,2),
    (9,5,3),
    (10,5,4),
    (11,6,5),
    (12,6,4);

INSERT INTO Comments (Message,CreatedAt,UserId,ProductId,MarkId)
VALUES
    ('Nul publicit� mensong�re',GETDATE(),1,1,1),
    ('Je suis met la note de 1 parce que je suis tr�s de�u',GETDATE(),2,1,2),
    ('Produit pas si incroyable que �a',GETDATE(),3,1,3),
    ('Je trouve ce produit pas si mal que �a, il m�rite une note moyenne',GETDATE(),4,1,4),
    ('Le produit est bon je met la note de 4',GETDATE(),5,1,5),
    ('Produit incroyable je le recommande beaucoup',GETDATE(),1,2,6),
    ('Nul publicit� mensong�re',GETDATE(),2,2,7),
    ('Je suis met la note de 1 parce que je suis tr�s de�u',GETDATE(),3,2,8),
    ('Produit pas si incroyable que �a',GETDATE(),4,2,9),
    ('Je trouve ce produit pas si mal que �a, il m�rite une note moyenne',GETDATE(),5,2,10),
    ('Le produit est bon je met la note de 4',GETDATE(),1,3,11),
    ('Produit incroyable je le recommande beaucoup',GETDATE(),2,3,12),
    ('Nul publicit� mensong�re',GETDATE(),3,3,13),
    ('Je suis met la note de 1 parce que je suis tr�s de�u',GETDATE(),4,3,14),
    ('Produit pas si incroyable que �a',GETDATE(),5,3,15),
    ('Je trouve ce produit pas si mal que �a, il m�rite une note moyenne',GETDATE(),1,4,16),
    ('Le produit est bon je met la note de 4',GETDATE(),2,4,17),
    ('Produit incroyable je le recommande beaucoup',GETDATE(),3,4,18),
    ('Nul publicit� mensong�re',GETDATE(),4,4,19),
    ('Je suis met la note de 1 parce que je suis tr�s de�u',GETDATE(),5,4,20);

INSERT INTO Orders (TotalPrice,CreatedAt,UserId)
VALUES
    (37,GETDATE(),1),
    (1478,GETDATE(),2);

INSERT INTO OrderProducts (Id,ProductId,OrderId)
VALUES
    (1,1,1),
    (2,2,1),
    (3,3,1),
    (4,6,2);