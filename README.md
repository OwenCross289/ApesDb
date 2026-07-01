# ApesDb

Minimal .NET 10 solution with a FastEndpoints API, Swagger, xUnit v3 integration tests, CSharpier, local infrastructure via Docker Compose, and Linux CI.

## Prerequisites

- .NET 10 SDK
- Docker Desktop or a compatible Docker engine with Docker Compose

## Setup

```bash
dotnet tool restore
dotnet restore ApesDb.slnx
docker compose up -d
```

## Run the API

```bash
dotnet run --project src/ApesDb.Api
```

Default local URLs from `launchSettings.json`:

- `http://localhost:5233`
- `https://localhost:7250`

Swagger UI:

- `https://localhost:7250/swagger`
- `http://localhost:5233/swagger`

## Run tests

```bash
dotnet test --solution ApesDb.slnx
```

## Format code

Check formatting:

```bash
dotnet csharpier check .
```

Format the repo:

```bash
dotnet csharpier format .
```

## Local services

Start Postgres and Redis:

```bash
docker compose up -d
```

Stop services:

```bash
docker compose down
```

Remove services and volumes:

```bash
docker compose down -v
```
