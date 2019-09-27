## KrakenMPSPServer

Solução responsável por gerenciar o armazenamento das informações, disponibilizando esses dados via comunicação REST documentado com Swagger para integrações futuras.

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

ENTITY FRAMEWORK 6
```bash
Enable-Migrations -ContextTypeName MySqlContext
Add-Migration InitialCreate
Update-Database
```

ENTITY FRAMEWORK CORE
```bash
dotnet ef migrations add InitialCreate -c MySqlContext
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