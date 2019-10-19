docker pull mcr.microsoft.com/mssql/server:2017-latest

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Abc12345678" ` -p 1433:1433 --name db ` -d mcr.microsoft.com/mssql/server:2017-latest

docker exec -it db /opt/mssql-tools/bin/sqlcmd ` -S localhost -U SA -P "Abc12345678" ` -Q "ALTER LOGIN SA WITH PASSWORD='Abc12345678'"    

docker exec -it db "bash"

# od tego momentu, wymagane jest pisanie recznie komend

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Abc12345678'

CREATE DATABASE FindATutor
GO
exit

cd ..
cd .\Backend\
cd .\Find-A-Tutor.Infrastructure\
dotnet ef database update