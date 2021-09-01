-- DDL

CREATE DATABASE T_Rental;
GO

USE T_Rental;
GO

CREATE TABLE EMPRESA (
	idEmpresa SMALLINT PRIMARY KEY IDENTITY(1,1),
	nomeEmpresa VARCHAR(20) NOT NULL
);
GO

CREATE TABLE MARCA (
	idMarca TINYINT PRIMARY KEY IDENTITY(1,1),
	nomeMarca VARCHAR(20) NOT NULL
);
GO

CREATE TABLE CLIENTE (
	idCliente SMALLINT PRIMARY KEY IDENTITY(1,1),
	nomeCliente VARCHAR (20) NOT NULL,
	sobrenomeCliente VARCHAR (20) NOT NULL,
	CNH CHAR (12) NOT NULL UNIQUE
);
GO

CREATE TABLE MODELO (
	idModelo SMALLINT PRIMARY KEY IDENTITY(1,1),
	idMarca TINYINT FOREIGN KEY REFERENCES MARCA(idMarca),
	nomeModelo VARCHAR (15) NOT NULL
);
GO

CREATE TABLE VEICULO (
	idVeiculo SMALLINT PRIMARY KEY IDENTITY(1,1),
	idEmpresa SMALLINT FOREIGN KEY REFERENCES EMPRESA(idEmpresa),
	idModelo SMALLINT FOREIGN KEY REFERENCES MODELO(idModelo),
	Placa CHAR(7) NOT NULL
);
GO

CREATE TABLE ALUGUEL (
	idAluguel INT PRIMARY KEY IDENTITY(1,1),
	idVeiculo SMALLINT FOREIGN KEY REFERENCES VEICULO(idVeiculo),
	idCliente SMALLINT FOREIGN KEY REFERENCES CLIENTE(idCliente),
	dataEmpresetimo DATE NOT NULL,
	dataDevolucao DATE NOT NULL
);
GO
