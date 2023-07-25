drop database if exists SelXPressApi;
create database SelXPressApi;
use SelXPressApi;

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

CREATE TABLE Attributes (
                       id INT primary key not null auto_increment,
                       name varchar(200) not null unique,
                       type varchar(200) not null,
                       created_at date not null,
                       updated_at date not null
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
                        created_at date not null,
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

CREATE TABLE Marks (
                      id int primary key not null auto_increment,
                      rate float not null 
);

CREATE TABLE Comments (
                          id int primary key not null auto_increment,
                          message varchar(500) not null ,
                          created_at date not null,
                          userId int references Users(id),
                          productId int references Products(id),
                          markId int references Marks(id)
);
