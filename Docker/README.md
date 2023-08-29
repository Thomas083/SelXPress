# SelXPress

Launch the containers: 

docker-compose up

Connect to the database in SQL Server container:

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<Password>"

Create a certaficate for the .net API:

dotnet dev-certs https -ep .\docker\https\aspnetapp.pfx -p <CREDENTIAL_PLACEHOLDER>
dotnet dev-certs https --trust

Urls: 

front-office: http://localhost:4000
back-office: http://localhost:4001
api: https://localhost:7094
swagger: https://localhost:7094/swagger/
smtp: http://localhost:2500
smtp-webUI: http://localhost:8080
smtp-database: http://localhost:8085
