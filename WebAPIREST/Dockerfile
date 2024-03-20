FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY *.csproj .

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out ./

ENV DBServer=localhost

ENV DBPort=5432

ENV DBName=postgresDB

ENV DBUsername=postgres

ENV DBPassword=root

RUN env

ENTRYPOINT ["dotnet", "WebAPIREST.dll"]
