create database DBEmployee
go
use DBEmployee
go
create table Employee(
	ID int IDENTITY(1,1) PRIMARY KEY,
	FirstName varchar(20),
	LastName varchar(20),
	Email varchar(30),
	FSU varchar(10),
	Position varchar(20)
);