<h1 align="center"> ApiSample </h1>


# Objetivo
Este projeto tem como finalidade criar exemplos para documentar comandos e techos de código para que possam servir para consulta futuramente.  

# Sobre o projeto
A ideia para esse projeto é termos um worker que vai varrer uma lista de endereços de RSS, baixar o seu conteúdo e enfileirar para que o mesmo possa ser processado por outro componente. 

# Tecnologias usadas

* .Net
* Entityframework
* Docker
* RabbitMQ
* Keycloak

# Comandos dotnet

Vamos iniciar criando o projeto. Para isso vamos executar o comando

`dotnet new sln -o ApiSample -n ApiSample` Esse comando cria a sulution com o diretório definido por `-o` e o nome do projeto definido por `-n`

Agora vamos criar mais um projeto que vai conter nossas apis.

`dotnet new webapi -controllers -o ApiSample.Application -n ApiSample.Applicattion`
 Vamos inserir esse mesmo projeto à solution criada anteriormente como o comando

 `dotnet sln add ApiSample.Application/ApiSample.Applicattion.csproj`

 Vamos repetir esse passo mais duas vezes para criarmos dois projetos de Class Library.
 Um responsável pelo domínio do nosso projeto e o outro pela camada de infraestrutura.

`dotnet new classlib -o ApiSample.Domain -n ApiSample.Domain`

`dotnet sln add ApiSample.Domain/ApiSample.Domain.csproj`

`dotnet new classlib -o ApiSample.Infrastructure -n ApiSample.Infrastructure`

`dotnet sln add ApiSample.Infrastructure/ApiSample.Infrastructure.csproj`

# Entityframework

De dentro da pasta de infraestrutura vamos executar os comandos

`dotnet add package Microsoft.EntityFrameworkCore`

`dotnet add package Microsoft.EntityFrameworkCore.Tools`

`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`


# Criando container com RabbitMQ

docker run -d --name my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
Vale a pena lembrar que o login é guest e senha guest
E, nesse caso, o endereço de acesso será: http://localhost:15672
