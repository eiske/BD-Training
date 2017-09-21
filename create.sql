create table Linha (
	seqLinha numeric,
	codLinha numeric,
	descLinha varchar(50),
	primary key (codLinha)
);

create table Producao (
	seqProducao numeric NOT NULL,
	produto varchar(50) NOT NULL,
	codLinha int NOT NULL,
	peso float NOT NULL,
	numeroSerie NUMERIC NOT NULL,
	constraint pk_CE primary key (produto, numeroSerie),
	foreign key (codLinha) references Linha (codLinha)
);

INSERT INTO Linha(codLinha, descLinha) VALUES (1, 'Moto');
INSERT INTO Linha(codLinha, descLinha) VALUES (2, 'Carro');
INSERT INTO Linha(codLinha, descLinha) VALUES (3, 'Bola');
INSERT INTO Linha(codLinha, descLinha) VALUES (4, 'Barco');
INSERT INTO Linha(codLinha, descLinha) VALUES (5, 'PC');

ALTER TABLE Producao ADD FOREIGN KEY (codLinha) REFERENCES Linha (codLinha); 

DROP TABLE Linha;
DROP TABLE Producao;