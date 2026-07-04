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
pnpm build
docker compose up -d
```

## Authentication

ApesDb uses Auth0 for cookie-based session authentication. Google SSO is the only login method and is restricted to a preset allowlist of emails.

### Auth0 setup

1. Create a **Regular Web Application** in your Auth0 tenant.
2. Under **Authentication > Social**, enable the **Google** connection for the application.
3. Configure the following application URLs:
   - Allowed Callback URLs:
     - `https://localhost:7250/api/auth/callback`
     - `https://apesdb.owencross.com/api/auth/callback`
   - Allowed Logout URLs:
     - `https://localhost:7250/`
     - `https://apesdb.owencross.com/`
   - Allowed Web Origins:
     - `https://localhost:7250`
     - `https://apesdb.owencross.com`
4. Create a **Post Login** action that reads an `ALLOWED_EMAILS` secret and denies login for unknown emails:

   ```javascript
   exports.onExecutePostLogin = async (event, api) => {
     const allowedEmails = event.secrets["ALLOWED_EMAILS"] || "";
     const allowedList = allowedEmails
       .split(",")
       .map((email) => email.trim().toLowerCase())
       .filter(Boolean);

     const userEmail = (event.user.email || "").toLowerCase();

     if (!allowedList.includes(userEmail)) {
       api.access.deny("Access denied.");
     }
   };
   ```

### Local Auth0 configuration

Auth0 settings are not committed. Set them via user secrets before running the API:

```bash
dotnet user-secrets set "Auth0:Domain" "<your-auth0-domain>" --project src/backend/ApesDb.Api
dotnet user-secrets set "Auth0:ClientId" "<your-auth0-client-id>" --project src/backend/ApesDb.Api
dotnet user-secrets set "Auth0:ClientSecret" "<your-auth0-client-secret>" --project src/backend/ApesDb.Api
```

### Local IGDB configuration

IGDB client credentials are also not committed. Set them via user secrets:

```bash
dotnet user-secrets set "Igdb:ClientId" "<your-igdb-client-id>" --project src/backend/ApesDb.Api
dotnet user-secrets set "Igdb:ClientSecret" "<your-igdb-client-secret>" --project src/backend/ApesDb.Api
```

## Run the API

The API serves the built frontend in both Development and Production so that styling and asset loading are identical. Build the frontend first, then start the API:

```bash
pnpm build
dotnet run --project src/backend/ApesDb.Api
```

Default local URL from `launchSettings.json`:

- `https://localhost:7250`

Swagger UI:

- `https://localhost:7250/swagger`

## Run tests

```bash
dotnet test --solution ApesDb.slnx
```

## Run the frontend dev server

```bash
pnpm serve
```

This starts the Vite dev server on `http://localhost:5173` for frontend-only development with HMR. Auth and cookie-based API calls will not work from this URL because the API expects requests from `https://localhost:7250`. For normal full-stack development, use `pnpm build && dotnet run --project src/backend/ApesDb.Api` and open `https://localhost:7250`.

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

Local Redis requires the password `apesdb`. The API's committed local defaults match this through `Cache:ConnectionString=localhost:6379` and `Cache:Password=apesdb`.

Stop services:

```bash
docker compose down
```

Remove services and volumes:

```bash
docker compose down -v
```

## Database migrations

Migrations are managed with [Flyway](https://documentation.red-gate.com/flyway) through Docker Compose.

Migration scripts live in `db/migrations/`. To add a new migration, create a Flyway versioned SQL file with the next version number, for example:

```bash
db/migrations/V2__Add_profile_fields.sql
```

Flyway tracks applied scripts in the `migrations.flyway_schema_history` table and only runs each migration once. `FLYWAY_BASELINE_ON_MIGRATE` is enabled in Compose so existing databases that were previously migrated by DbUp are adopted at version 1.

Flyway uses `migrations` as its default schema and also manages `public`. App tables should be schema-qualified as `public` in migration scripts.

The local and deployment compose files run the `flyway` service against Postgres before the app and worker start. To run migrations manually:

```bash
docker compose run --rm flyway
```

The integration tests use an in-memory database and do not run Compose-managed migrations.

## Deployment environment variables

The production compose file expects the following environment variables:

- `AUTH0_DOMAIN`
- `AUTH0_CLIENT_ID`
- `AUTH0_CLIENT_SECRET`
- `IGDB_CLIENT_ID`
- `IGDB_CLIENT_SECRET`
- `REDIS_PASSWORD`

The API reads database and cache settings from `Database:ConnectionString`, `Cache:ConnectionString`, and `Cache:Password`. In Docker Compose, use the equivalent `Database__ConnectionString`, `Cache__ConnectionString`, and `Cache__Password` environment variable names.
