# Agent Instructions

## C# Style

- Do not use primary constructors for dependency-injected classes.
- Prefer explicit constructors with `private readonly` fields for services, endpoints, handlers, workers, clients, and similar classes.
- Positional records are acceptable for DTOs and value-like response/model shapes.

## Frontend UI

- When a needed shadcn component is not already available in `@apesdb/ui`, add it to the shared UI package and export it before using it in an app.
- NEVER HANDROLL COMPONENTS UNLESS EXPLICITLY TOLD TO ALWAYS USE THE SHADCN COMMAND TO ADD IT.
