## KrakenMPSPConsole

Solução responsável por gerenciar o armazenamento das informações, sendo executado de tempos em tempos para solicitar buscas ao KrakenMPSPCrawler.

### Changelog

[Learn about the latest improvements](changelog)

------------

# 1. Project Configuration

**BEFORE YOU INSTALL:** please read the [requirements](../README.md#prerequisites)

```bash
Update-Package -reinstall
net start MongoDB
```

------------

# 2. Manage database

Create new migrations:
```bash
dotnet ef migrations add <your_name>
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