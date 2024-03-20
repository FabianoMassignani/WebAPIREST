# Avaliação de candidato (.NET)

## Persistência de Dados 

- PostgreSQL

## Migration

 - Add-Migration InitialCreate
 - Update-Database

## Dependências

- Microsoft.AspNetCore.Authentication.JwtBearer
- AutoMapper
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Npgsql
- Npgsql.EntityFrameworkCore.PostgreSQL
- Swashbuckle.AspNetCore
- FluentValidation
- FluentValidation.AspNetCore
- Microsoft.Extensions.Configuration.EnvironmentVariables

## Docker

- docker build -t nome_da_imagem .
- docker run -d -p 8080:8080 --name nome_do_container nome_da_imagem

