# Docker

## Launch Docker:

Copy the ``.env-example``, create a ``.env`` named file and modify the parameters in ``{}``

## Launch the container: 

``cd ./docker`` 
``docker-compose up``

## Connect to the database in SQL Server container:

``/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<Password>"``

## Urls: 

| Container | URL |
|:---------------:|:---------------|
| Front | http://localhost:4000 |
| Back | http://localhost:4001 |
| API | https://localhost:7094 |
| Swagger | https://localhost:7094/swagger/ |
| SMTP | http://localhost:2500 |
| SMTP-WebUI | http://localhost:8080 |
| SMTP-Database | http://localhost:8085 |

## For testing in local the smtp server

``cd ./docker``
``python testing_smtp.py``


