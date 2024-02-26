# srsProject

SRS sample task

# Prerequisites:

.NET SDK: https://dotnet.microsoft.com/en-us/download  
MS SQL Server Express (with LocalDB option enabled: Select LocalDB on the Feature Selection/Shared Features page during installation): https://www.microsoft.com/en-us/sql-server/sql-server-downloads

# After cloning repo

Run in CLI at the project root in this order only before the first run:
"dotnet tool install --global dotnet-ef"  
"dotnet ef database update"  
"dotnet build"  
Run the application:  
"dotnet run"

Open app in a web browser at https://localhost:7185

# Stored procedure can be found here:

projectroot/Migrations/20240224151408_AddStoredProcedure.cs
