<h1 align="center"> ApiSample </h1>

<p align="center">
<img loading="lazy" src="http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge"/>
</p>


# Objetivo

Este é um projeto de exemplo que poderá servir como referência para consultas futuras. O objetivo principal é demonstrar como acessar um fornecedor de feed, baixar cada feed publicado e enfileirá-los utilizando o RabbitMQ para gerenciar as filas de mensagens.

O projeto inclui diversos componentes:

Coletor de Feeds: Um worker service que acessa o fornecedor de feed, baixa os conteúdos publicados e os enfileira no RabbitMQ. Este componente é responsável por garantir que todos os feeds sejam capturados e enviados para processamento de maneira eficiente e confiável.

Processador de Feeds: Outro worker service que consome os feeds da fila no RabbitMQ, processa cada item e finaliza o processo com a persistência dos dados em um banco de dados. Este componente assegura que todos os feeds coletados sejam processados corretamente e armazenados para uso posterior.

API de Feeds: Uma API desenvolvida em .NET que expõe os dados dos feeds processados. Esta API permite que outros sistemas acessem os dados dos feeds de maneira estruturada e segura.

Frontend em React: Um projeto frontend desenvolvido em React que consome a API de Feeds e exibe os conteúdos coletados de maneira amigável e intuitiva. Este componente fornece uma interface visual para os usuários interagirem com os dados dos feeds.

O fluxo completo do projeto é: o coletor de feeds baixa e enfileira os feeds, o processador de feeds consome e processa esses itens, a API de Feeds disponibiliza os dados e o frontend em React exibe as informações para os usuários. Este projeto é um exemplo abrangente que integra várias tecnologias e práticas recomendadas para o desenvolvimento de software moderno, desde o backend até o frontend.

# Sobre o projeto
A ideia deste projeto é ter um worker que varre uma lista de endereços de RSS, baixa seu conteúdo e o enfileira para ser processado por outro componente.

# Tecnologias usadas

* .Net
* Entityframework
* Worker Service
* Docker
* RabbitMQ
* Keycloak

# Comandos dotnet

Vamos iniciar criando o projeto. Execute o comando:

`dotnet new sln -o ApiSample -n ApiSample` Esse comando cria a sulution com o diretório definido por `-o` e o nome do projeto definido por `-n`

Agora vamos criar mais um projeto que irá conter nossas apis.

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

# Worker Service

Esta solução é composta por alguns projetos com responsabilidades diferentes, entre eles:

RssReaderContainer: Responsável por acessar, baixar e enfileirar os feeds.
RssQueueConsumer: Responsável por retirar os feeds da fila e processá-los.
A motivação para usar um Worker Process é a possibilidade de ser um componente que pode ficar executando em background. Leia mais sobre <A href="https://learn.microsoft.com/pt-br/dotnet/core/extensions/workers">Worker Service aqui</a>.

Nessa solution os Workers foram criados para ser executados em containers mas também poderiam ser instalados diretamente no computador e gerenciados como serviços.


## No Windows
### Instalar um Worker Service como Serviço do Windows

Publicar o Projeto:
No terminal, vá para a pasta do seu projeto e execute o comando para publicar:
`dotnet publish --configuration Release`

Criar o Serviço:
Abra o PowerShell como administrador e execute o seguinte comando para criar um serviço do Windows:

`New-Service -Name "MyWorkerService" -Binary "C:\Caminho\Para\Seu\Projeto\bin\Release\netcoreapp3.1\win-x64\publish\SeuWorkerService.exe"`

Iniciar o Serviço:
Ainda no PowerShell, inicie o serviço:

`Start-Service -Name "MyWorkerService"`

### Desinstalar um Worker Service como Serviço do Windows

Parar o Serviço:
No PowerShell como administrador, pare o serviço:

`Stop-Service -Name "MyWorkerService"` 

Remover o Serviço:
No PowerShell, remova o serviço:

`Remove-Service -Name "MyWorkerService"`

## Linux
### Instalar um Worker Service como Serviço no Linux

Publicar o Projeto:
No terminal, vá para a pasta do seu projeto e execute o comando para publicar:

`dotnet publish --configuration Release`

Criar um Arquivo de Serviço:
Crie um arquivo de serviço systemd. Abra um editor de texto e crie um arquivo chamado `myworkerservice service`:

`sudo nano /etc/systemd/system/myworkerservice.service`

Adicione o seguinte conteúdo ao arquivo:

[Unit]
Description=My Worker Service

[Service]
ExecStart=/usr/bin/dotnet /caminho/para/seu/projeto/bin/Release/netcoreapp3.1/publish/SeuWorkerService.dll
Restart=always
RestartSec=10
SyslogIdentifier=myworkerservice
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target

Habilitar o Serviço:
No terminal, habilite e inicie o serviço:

`sudo systemctl enable myworkerservice`
`sudo systemctl start myworkerservice`

## Desinstalar um Worker Service como Serviço no Linux

Parar o Serviço:
No terminal, pare o serviço:

`sudo systemctl stop myworkerservice` 

Desabilitar o Serviço:
Desabilite o serviço:

`sudo systemctl disable myworkerservice`

Remover o Arquivo de Serviço:
Remova o arquivo de serviço:

`sudo rm /etc/systemd/system/myworkerservice.service`

Recarregar os Daemons:
Recarregue os daemons do systemd para aplicar as mudanças:

`sudo systemctl daemon-reload` 

# Criando container com RabbitMQ

docker run -d --name my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
Vale a pena lembrar que o login é guest e senha guest
E, nesse caso, o endereço de acesso será: http://localhost:15672



