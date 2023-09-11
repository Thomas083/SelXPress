<img src="https://user.oc-static.com/upload/2021/11/10/1636542639252_Moby-logo.png" alt="Docker" width="80" height="60"> **Docker**

## Table of Contents
  - [Launch Docker:](#launch-docker)
        - [.env Content](#env-content)
  - [Launch the container:](#launch-the-container)
  - [SQL Server container:](#sql-server-container)
  - [URLs:](#urls)
  - [SMTP server](#smtp-server)
  
## Launch Docker:

Copy the ``.env-example``, create a ``.env`` named file and modify the parameters in ``{}``

##### .env Content

| Name | Value |
|:---------------|:---------------|
| **ASPNETCORE_Kestrel__Certificates__Default__Password** | Password for creating the HTTPS certificate |
| **ASPNETCORE_Kestrel__Certificates__Default__Path** | URL Path for the HTTPS certificate |
| **ASPNETCORE_URLS** | URL Configuration for the API (HTTP only or HTTP/HTTPS) |
| **ASPNETCORE_ENVIRONMENT** | {Development} for testing, {Production} for official launch |
| **MSSQL_SA_PASSWORD** | SA Password for the database |

## Launch the containers: 

Open a terminal at the root project and execute this :

``cd ./docker`` 
``docker-compose up``

If you want to launch a single service :

``cd ./docker`` 
``docker-compose up api``

## SQL Server container:

To access in local to the database, open the container terminal and execute this :

``/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<Password>"``

## URLs: 

| Container | URL |
|:---------------:|:---------------|
| Front | http://localhost:4000 |
| Back | http://localhost:4001 |
| API | https://localhost:7094 |
| Swagger | https://localhost:7094/swagger/ |
| SMTP | http://localhost:2500 |
| SMTP-WebUI | http://localhost:8080 |
| SMTP-Database | http://localhost:8085 |

## SMTP server

For testing in local the smtp server, open a terminal at the root project and execute this :

``cd ./docker``
``python testing_smtp.py``


