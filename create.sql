-----------------
-- Fábrica 1.0 --
-----------------
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

-----------------
-- Fábrica 2.0 --
-----------------

CREATE TABLE linha (
  seq_linha INTEGER NOT NULL,
  cod_linha VARCHAR2(10)  NOT NULL primary key,
  des_linha VARCHAR2(50) NULL
);

CREATE TABLE produto (
  seq_produto INTEGER       NOT NULL,
  cod_produto VARCHAR2(10)  NOT NULL primary key,
  cor VARCHAR2(50) NULL,
  cod_familia INTEGER NOT null
);

CREATE TABLE producao (
  seq_producao number(10)    NOT NULL,
  cod_linha    VARCHAR2(10)  NOT NULL,
  cod_produto  VARCHAR2(10) NOT NULL,
  num_serie    number(20,10) NOT NULL,
  peso     number(20,10) NOT NULL,
  dat_data DATE,
  CONSTRAINT pk_producao primary key(cod_produto, num_serie),
  CONSTRAINT fk_producao_linha FOREIGN KEY(cod_linha) references linha(cod_linha)
);

CREATE TABLE inspecao (
  seq_inspecao INTEGER NOT NULL,
  cod_produto  VARCHAR2(10) NOT NULL,
  num_serie    number(20,10) NOT NULL,
  aprovado CHAR(1) DEFAULT 'n' NOT NULL,
  num_resultado NUMBER(20,10) NOT NULL,
  dat_data DATE,
  CONSTRAINT fk_inspecao_producao FOREIGN KEY (cod_produto,num_serie) REFERENCES producao(cod_produto,num_serie) 
);

INSERT INTO Linha(cod_linha, des_linha) VALUES (1, 'Linha1');
INSERT INTO Linha(cod_linha, des_linha) VALUES (2, 'Linha2');
INSERT INTO Linha(cod_linha, des_linha) VALUES (3, 'Linha3');
INSERT INTO Linha(cod_linha, des_linha) VALUES (4, 'Linha4');
INSERT INTO Linha(cod_linha, des_linha) VALUES (5, 'Linha5');


INSERT INTO produto(cod_produto, cor, cod_familia) VALUES ('Geladeira', 'branca', 1);
INSERT INTO produto(cod_produto, cor, cod_familia) VALUES ('Fogao', 'preto', 2);
INSERT INTO produto(cod_produto, cor, cod_familia) VALUES ('Ar-condicionar', 'branca', 3);
INSERT INTO produto(cod_produto, cor, cod_familia) VALUES ('Aspirador', 'vermelho', 6);
INSERT INTO produto(cod_produto, cor, cod_familia) VALUES ('Forno', 'marrom', 8);

