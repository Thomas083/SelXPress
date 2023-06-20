drop table if exists Roles, Users, Categories, Tags, Products, Stocks, Orders, OrderProducts, Carts cascade ;

CREATE TABLE Roles (
                       id int primary key not null,
                       name varchar(200) not null
);
CREATE TABLE Users (
                       id INT primary key not null,
                       username varchar(150) not null ,
                       firstname varchar(100),
                       lastname varchar(100),
                       password varchar(200) not null ,
                       email varchar(200) not null ,
                       roleId int references Roles(id)
);

CREATE TABLE Categories (
                            id int primary key not null,
                            name varchar(200) not null
);

CREATE TABLE Tags (
                      id int primary key not null,
                      name varchar(200) not null ,
                      categoryId int references Categories(id)
);

CREATE TABLE Products (
                          id int primary key not null,
                          name varchar(200) not null ,
                          description varchar(1000) not null,
                          price float,
                          categoryId int references Categories(id),
                          picture varchar(500),
                          created_at date not null,
                          sellPeopleId int references Users(id)
);

CREATE TABLE Stocks (
                        id int primary key not null ,
                        productId int references Products(id),
                        quantity int not null
);

CREATE TABLE Orders (
                        id int primary key not null ,
                        userId int references Users(id),
                        totalPrice float not null
);

CREATE TABLE OrderProducts (
                               id int primary key not null ,
                               productId int references Products(id),
                               orderId int references Orders(id)
);

CREATE TABLE Carts (
                       id int primary key not null ,
                       userId int references Users(id),
                       productId int references Products(id)
);