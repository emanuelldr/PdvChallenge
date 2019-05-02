# ZX Ventures Backend Challenge #

.Net Core API para dado uma localização (lng, lat), busque o PDV mais próximo e que atenda à mesma, conforme sua área de cobertura.

## Tecnologia 

- .Net Core 
- Postgre/Postgis 
- Docker 
- Docker-Compose 
- Swagger

## Requisitos

- Docker
- Docker-Compose

## Documentação

- As rotas, bem como os modelos/objetos foram documentados utilizando Swagger.
-  A documentação será disponibilizada em: http://localhost:5000/index.html


## Executar o Projeto 

- Para executar o projeto, basta rodar os 2 comandos à seguir:
		
		- 1º - "docker-compose up --build -d"  - Para subir os containers de banco e web api
		- 2º - "docker-compose exec db_postgres psql -U PdvUser -d PdvDb -1 -f /data/postgres/PdvDbEntryPoint.sql"  - Criação das tabelas no banco



## Rotas 

- GET: http://localhost:5000/api/Pdvs - Lista todos os pdvs.
- GET: http://localhost:5000/api/Pdvs/id - Obtém um pdv por id.
- GET: http://localhost:5000/api/Pdvs/{lng}/{lat} - Obtém o PDV que seja o mais próximo a atender as coordenadas.
- POST: http://localhost:5000/api/Pdvs - Insere um novo PDV (Todos os campos são obrigatórios, inclusive o id)


##  Testes

- O projeto PdvChallenge.API.Tests contém testes realizados em tempo de desenvolvimento