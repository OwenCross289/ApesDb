# ApesDb

Minimal .NET 10 solution with a FastEndpoints API, worker service, Swagger, xUnit v3 integration tests, a root Nx/pnpm React workspace, Oxc frontend linting/formatting, CSharpier, local infrastructure via Docker Compose, and Linux CI.

## Prerequisites

- .NET 10 SDK
- Node.js 26
- pnpm 11
- Docker Desktop or a compatible Docker engine with Docker Compose

## Setup

```bash
dotnet tool restore
dotnet restore ApesDb.slnx
pnpm install
docker compose up -d
```

## Run the API

```bash
dotnet run --project src/backend/ApesDb.Api
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

## Run the frontend

```bash
pnpm serve
```

Build the frontend:

```bash
pnpm build
```

Lint and check formatting:

```bash
pnpm lint
pnpm format:check
```

Format frontend files:

```bash
pnpm format
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

Start the app, worker, Postgres, and Redis:

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
