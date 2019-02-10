Create Database DesafioPartnerGroupBD
Go

Use DesafioPartnerGroupBD
Go

Create table Marcas
(
     ID int primary key identity,
     Nome nvarchar(50) UNIQUE NOT NULL,
)
Go

Create table Patrimonios
(
     ID int primary key identity,
	 MarcaID int FOREIGN KEY REFERENCES Marcas(ID) NOT NULL,
     Nome nvarchar(50) NOT NULL,
     Descricao nvarchar(50) NULL,
     Tombo int NULL
)
Go

Insert into Marcas values ('Marca 1')
Insert into Marcas values ('Marca 2')
Insert into Marcas values ('Marca 3')

Insert into Patrimonios values (1, 'Patrimônio 1', 'Descrição 1', 1)
Insert into Patrimonios values (2, 'Patrimônio 2', 'Descrição 2', 2)
Insert into Patrimonios values (3, 'Patrimônio 3', NULL, NULL)
