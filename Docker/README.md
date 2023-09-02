# SelXPress

For docker :

Copy the .env-example, create a .env named file and modify the parameters in { }

Launch the containers: 

docker-compose up

Connect to the database in SQL Server container:

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<Password>"

Urls: 

front-office: http://localhost:4000
back-office: http://localhost:4001
api: https://localhost:7094
swagger: https://localhost:7094/swagger/
smtp: http://localhost:2500
smtp-webUI: http://localhost:8080
smtp-database: http://localhost:8085
