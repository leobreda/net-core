MockSQL
-------
Biblioteca para utilização de mock no lugar de banco de dados SQL.

Útil para cenários de testes onde a massa de dados não está pronta,
ou até mesmo para Teste Unitários.

A injeção do Mock é realizada diretamente na camada de dados (CRUD).
Sendo assim, substitui a codificação de objetos via MOQ (https://github.com/moq).

O projeto é composto de:

MockSQL.csproj
--------------
Console para testar o consumo de SQL e Mock.


Net.Leobreda.Database
---------------------
Camada de dados CRUD (Create, Read, Update e Delete), para comunicação com SQL e Mock.
Possui dependência com o projeto Net.Leobreda.Mock


Net.Leobreda.Mock
-----------------
Biblioteca de Mock para interpretar arquivos CSV, transformando em objetos do tipo:
1. ExecuteNonQuery, interpretado no arquivo ExecuteNonQuery.json
2. ExecuteReader, interpretado no arquivo ExecuteReader.json
3. ExecuteScalar, interpretado no arquivo ExecuteScalar.json

Cada arquivo json faz referência a um novo arquivo CSV, responsáveis pelo retorno dos dados.


UnitTest
--------
Teste unitário para verificar a acurácia dos métodos.


appsettings.json
----------------
Arquivo de parametrização da solução.

Net.Leobreda.Mock.Verbose
- enabled:	Determina se o envio/retorno dos dados são escrito num arquivo texto.
- folder:		Diretório onde são armazenados os LOGs verbose
- size:		Tamanho máximo de cada arquivo de LOG

Net.Leobreda.Mock.Sql
- enabled:	Habilita/Desabilita o uso de arquivos Mock
- folder:		Diretório onde contém os arquivos ExecuteNonQuery.json, ExecuteReader.json, ExecuteScalar.json e arquivos Mock na extensão .CSV

ConnectionStrings
- Mysql:		ConnectionString de comunicação com o Banco de dados MySQL/MariaDB


Banco de dados
--------------
Para testar a comunicação com o banco de dados, é imprescindível a criação de uma tabela no MySQL/MariaDB.

A connection string está parametrizada no arquivo appsettings.json

Estrutura:

CREATE TABLE PEOPLE (

	ID INT AUTO_INCREMENT,

	FULLNAME VARCHAR (90),

	DATEBIRTH DATE,

	PRIMARY KEY(ID)

); 


INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('1','Neil Armstrong','1930-08-05');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('2','Buzz Aldrin','1930-01-20');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('3','Charles Conrrad','1930-06-02');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('4','Alan Bean','1932-03-15');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('5','Alan Shepard','1932-11-18');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('6','Edgar Mitchell','1930-09-17');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('7','David Scott','1932-06-06');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('8','James Irwin','1930-03-17');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('9','John Young','1930-09-24');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('10','Charles Duke','1935-10-03');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('11','Eugene Cernan','1930-03-14');

INSERT INTO PEOPLE (ID, FULLNAME, DATEBIRTH) VALUES('12','Harrison Schmitt','1935-07-03');

SELECT * FROM PEOPLE;