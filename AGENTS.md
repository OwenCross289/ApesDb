# Agent Instructions

## C# Style

- Do not use primary constructors for dependency-injected classes.
- Prefer explicit constructors with `private readonly` fields for services, endpoints, handlers, workers, clients, and similar classes.
- Positional records are acceptable for DTOs and value-like response/model shapes.
