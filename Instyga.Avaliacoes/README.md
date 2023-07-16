# Trabalho de Software Engineer
Simples CRUD de cadastro de Avaliacoes e Avaliações.
Exemplo:
Avaliacao: Desenvolvedor .NET
Avaliações: Criação de Web APIs com .NET CORE, Migrations com Entity Framework, Criação de WebApps com Blazor etc..

## Como rodar:
```
dotnet restore
# dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run
```

Então acesse: `http://localhost:5001` for Swagger UI.

Para rodar o rabbitmq usar comandos do docker:

docker pull rabbitmq:management
docker run --detach --name rabbitmq-blog-management --publish 5672:5672 --publish 15672:15672 rabbitmq:management