drop table if exists Roles, Users, Categories, Tags, Products, Stocks, Orders, OrderProducts, Carts, Comments, Mark cascade ;

CREATE TABLE Roles (
                       id int primary key not null auto_increment,
                       name varchar(200) not null
);
CREATE TABLE Users (
                       id INT primary key not null auto_increment,
                       username varchar(150) not null unique ,
                       firstname varchar(100),
                       lastname varchar(100),
                       password varchar(200) not null ,
                       email varchar(200) not null unique ,
                       roleId int references Roles(id)
);

CREATE TABLE Categories (
                            id int primary key not null auto_increment,
                            name varchar(200) not null unique
);

CREATE TABLE Tags (
                      id int primary key not null auto_increment,
                      name varchar(200) not null unique ,
                      categoryId int references Categories(id)
);

CREATE TABLE Products (
                          id int primary key not null auto_increment,
                          name varchar(200) not null ,
                          description varchar(1000) not null,
                          price float,
                          categoryId int references Categories(id),
                          picture varchar(500),
                          created_at date not null,
                          sellPeopleId int references Users(id)
);

CREATE TABLE Stocks (
                        id int primary key not null auto_increment,
                        productId int references Products(id),
                        quantity int not null
);

CREATE TABLE Orders (
                        id int primary key not null auto_increment ,
                        userId int references Users(id),
                        totalPrice float not null
);

CREATE TABLE OrderProducts (
                               id int primary key not null auto_increment,
                               productId int references Products(id),
                               orderId int references Orders(id)
);

CREATE TABLE Carts (
                       id int primary key not null auto_increment,
                       userId int references Users(id),
                       productId int references Products(id)
);

CREATE TABLE Comments (
                          id int primary key not null auto_increment,
                          message varchar(500) not null ,
                          userId int references Users(id),
                          productId int references Products(id)
);

CREATE TABLE Mark (
                      id int primary key not null auto_increment,
                      rate float not null ,
                      commentId int references Comments(id)
);