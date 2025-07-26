# KrakenMPSPConsole

Este projeto é uma aplicação de console .NET que atua como o orquestrador dos crawlers (robôs de busca). Sua principal responsabilidade é iniciar e gerenciar a execução dos crawlers para coletar informações de diversas fontes de dados sobre pessoas físicas e jurídicas.

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

### `Program.cs`

O ponto de entrada da aplicação. Ele é responsável por ler os argumentos da linha de comando, instanciar os coordenadores e iniciar o processo de busca.

### Coordenadores

- **`PhysicalPersonCoordinator.cs`**: Responsável por orquestrar a busca de informações sobre pessoas físicas. Ele gerencia a execução dos crawlers específicos para PFs.
- **`LegalPersonCoordinator.cs`**: Responsável por orquestrar a busca de informações sobre pessoas jurídicas, gerenciando os crawlers específicos para PJs.

### `Crawlers/`

Esta pasta contém a implementação de cada um dos robôs de busca. Cada crawler é uma classe que herda da interface `ICrawler` (do projeto `KrakenMPSPBusiness`) e é especializada em extrair dados de uma fonte específica. Exemplos:

- `ArispCrawler.cs`
- `CadespCrawler.cs`
- `SivecCrawler.cs`
- `JucespCrawler.cs`

### `Services/`

Contém serviços de suporte para a aplicação, como o `WebDriverService.cs`, que é responsável por gerenciar as instâncias do Selenium WebDriver para os crawlers que necessitam de navegação em um browser.

### `Helpers/`

Contém classes de ajuda com funcionalidades reutilizáveis, como o `HttpHelper.cs` para realizar requisições HTTP.

### `ThirdParty/`

Esta pasta contém drivers de terceiros necessários para o funcionamento do Selenium, como o `chromedriver.exe` e o `geckodriver.exe`.

## Como Executar

Para executar a aplicação de console, você precisará compilar o projeto e executá-lo através da linha de comando, passando os argumentos necessários para a busca.

**Exemplo (ainda a ser detalhado):**

```bash
# Exemplo de como a execução poderia ser
KrakenMPSPConsole.exe --tipo=pf --documento=123.456.789-00
```

A aplicação irá então instanciar o coordenador apropriado (`PhysicalPersonCoordinator` neste caso) e iniciar a execução dos crawlers configurados para buscar informações sobre o CPF fornecido. Os resultados são então armazenados para serem consumidos pela API (`KrakenMPSPServer`).
