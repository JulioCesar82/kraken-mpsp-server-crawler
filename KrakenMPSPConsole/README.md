## KrakenMPSPConsole

Solução responsável por gerenciar o armazenamento das informações, sendo executado de tempos em tempos para solicitar buscas ao KrakenMPSPCrawler.

### Changelog

[Learn about the latest improvements](changelog)

------------

# 1. Project Configuration

**BEFORE YOU INSTALL:** please read the [requirements](../README.md#requirements)

```bash
Update-Package -reinstall

./KrakenMPSPConsole\KrakenMPSPConsole\ThirdParty\mongodb-win32-x86_64-2012plus-4.2.0\bin\mongod.exe
```

------------

# 2. Manage database

Create new migrations:
```bash
dotnet ef migrations add <your_name>

dotnet ef migrations add InitialCreate -c SqlLiteContext
```

Run migrations:
```bash
dotnet ef database update
```

------------

# 3. Generate build

The build artifacts will be stored in the `bin/` directory. Use the `-c` flag for specific environment build.

```bash
dotnet.exe publish -c Release -f netcoreapp2.2
```

------------

# 4. Documentation

TODO