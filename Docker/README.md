# SelXPress

Connect to the database in SQL Server container:

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<Password>"

Create a certaficate for the .net API:

dotnet dev-certs https -ep .\docker\https\aspnetapp.pfx -p <CREDENTIAL_PLACEHOLDER>
dotnet dev-certs https --trust