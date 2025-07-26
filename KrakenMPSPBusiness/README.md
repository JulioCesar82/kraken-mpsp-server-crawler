# KrakenMPSPBusiness

Este projeto é a biblioteca de classes principal da solução Kraken MPSP. Ele contém todos os modelos de negócio, interfaces e enumerações que são compartilhados entre os outros projetos da solução (`KrakenMPSPConsole` e `KrakenMPSPServer`).

## Estrutura do Projeto

O projeto está organizado nas seguintes pastas:

### Models

A pasta `Models` contém as classes que representam as entidades do domínio da aplicação. Estas são as principais classes:

- **`PersonModel`**: Classe base para pessoas, contendo propriedades comuns a pessoas físicas e jurídicas.
- **`PhysicalPersonModel`**: Representa uma pessoa física, com propriedades como CPF, RG, etc. Herda de `PersonModel`.
- **`LegalPersonModel`**: Representa uma pessoa jurídica, com propriedades como CNPJ, Razão Social, etc. Herda de `PersonModel`.
- **`Crawler`**: Representa a entidade de um robô de busca (crawler), com informações sobre seu status e execução.
- **`CrawlerResult`**: Armazena o resultado da execução de um crawler para uma determinada pesquisa.
- **Modelos de Fontes de Dados**: Existem também modelos específicos para cada fonte de dados consultada pelos crawlers, como `ArispModel`, `CadespModel`, `SivecModel`, etc.

### Interfaces

A pasta `Interfaces` define os contratos que as classes de negócio devem implementar.

- **`ICrawler`**: Interface que define o contrato para todos os crawlers, garantindo que eles tenham um método `Execute`.
- **`IPerson`**: Interface que define as propriedades essenciais de uma pessoa.

### Enums

A pasta `Enums` contém as enumerações utilizadas no projeto.

- **`CrawlerStatus`**: Enumeração para o status de um crawler (ex: `Running`, `Completed`, `Error`).
- **`KindPerson`**: Enumeração para o tipo de pessoa (`Physical` ou `Legal`).

### Helpers

A pasta `Helpers` contém classes de ajuda com funcionalidades reutilizáveis.

- **`ManagerObjectHelper`**: Classe com métodos para manipulação de objetos.

## Como Usar

Este projeto é uma biblioteca de classes e não pode ser executado diretamente. Ele é referenciado pelos projetos `KrakenMPSPConsole` e `KrakenMPSPServer` para fornecer a lógica de negócio e os modelos de dados necessários.
