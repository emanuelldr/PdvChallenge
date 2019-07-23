# ZX Ventures Backend Challenge #

.Net Core API para dado uma localiza��o (lng, lat), busque o PDV mais pr�ximo e que atenda � mesma, conforme sua �rea de cobertura.

## Tecnologia 

- .Net Core 
- Postgre/Postgis 
- EF Core
- Docker 
- Swagger

## Requisitos

- Docker
- Docker-Compose

## Documenta��o

- As rotas, bem como os modelos/objetos foram documentados utilizando Swagger.
-  A documenta��o ser� disponibilizada em: http://localhost:5000/index.html


## Executar o Projeto 

- Para executar o projeto, basta rodar os 2 comandos � seguir na pasta raiz do projeto:
		
		- 1� - "docker-compose up --build -d"  - Para subir os containers de banco e web api
		- 2� - "docker-compose exec db_postgres psql -U PdvUser -d PdvDb -1 -f /data/postgres/PdvDbEntryPoint.sql"  - Cria��o das tabelas no banco



## Rotas 

- GET: http://localhost:5000/api/Pdvs - Lista todos os pdvs.
- GET: http://localhost:5000/api/Pdvs/id - Obt�m um pdv por id.
- GET: http://localhost:5000/api/Pdvs/{lng}/{lat} - Obt�m o PDV que seja o mais pr�ximo a atender as coordenadas.
- POST: http://localhost:5000/api/Pdvs - Insere um novo PDV (Todos os campos s�o obrigat�rios, inclusive o id)


##  Testes

- O projeto PdvChallenge.API.Tests cont�m testes realizados em tempo de desenvolvimento