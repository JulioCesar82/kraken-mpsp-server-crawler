# KrakenMPSPServer

Este projeto é uma API RESTful desenvolvida com ASP.NET Core. Sua função é expor os dados coletados pelos crawlers do `KrakenMPSPConsole` através de endpoints HTTP. Ele serve como a camada de acesso aos dados para qualquer cliente que precise consumir as informações centralizadas.

## Estrutura do Projeto

O projeto segue a arquitetura padrão de uma API ASP.NET Core:

### `Controllers/`

Contém os controladores da API, que são responsáveis por receber as requisições HTTP e retornar as respostas.

- **`PhysicalPersonController.cs`**: Expõe endpoints para consultar dados de pessoas físicas.
- **`LegalPersonController.cs`**: Expõe endpoints para consultar dados de pessoas jurídicas.

### `Repository/`

Contém as classes de repositório, que são responsáveis pela lógica de acesso ao banco de dados.

- **`PhysicalPersonRepository.cs`**: Implementa a lógica para buscar dados de pessoas físicas no banco de dados.
- **`LegalPersonRepository.cs`**: Implementa a lógica para buscar dados de pessoas jurídicas no banco de dados.

### `Services/`

Contém serviços de negócio que podem ser utilizados pelos controladores.

- **`CacheService.cs`**: Implementa um serviço de cache (usando Redis) para melhorar a performance das consultas.

### `Context/`

Contém as classes de contexto de banco de dados.

- **`MongoDbContext.cs`**: Gerencia a conexão e as operações com o banco de dados MongoDB.
- **`MySqlContext.cs`**: (Potencialmente para outro uso) Gerencia a conexão com um banco de dados MySQL.

### `Startup.cs`

Arquivo de configuração da aplicação, onde são registrados os serviços, a injeção de dependência, o pipeline de middleware e outras configurações da API, como a do Swagger.

## Como Executar

Para executar a API, você pode usar o comando `dotnet run` na pasta do projeto.

```bash
cd KrakenMPSPServer/KrakenMPSPServer
dotnet run
```

Após a execução, a API estará disponível no endereço configurado (por exemplo, `http://localhost:5000`).

### Documentação da API (Swagger)

A API está documentada com o Swagger. Após iniciar o servidor, você pode acessar a documentação interativa através do endpoint `/swagger` (ex: `http://localhost:5000/swagger`). Lá você poderá ver todos os endpoints disponíveis, seus parâmetros e schemas de resposta, além de poder testá-los diretamente pelo navegador.
