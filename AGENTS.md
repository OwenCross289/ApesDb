# Agent Instructions

## C# Style

- Do not use primary constructors for dependency-injected classes.
- Do not use ternary conditional expressions in C#; use explicit control flow instead.
- Prefer explicit constructors with `private readonly` fields for services, endpoints, handlers, workers, clients, and similar classes.
- Positional records are acceptable for DTOs and value-like response/model shapes.

## Frontend UI

- When a needed shadcn component is not already available in `@apesdb/ui`, add it to the shared UI package and export it before using it in an app.
- NEVER HANDROLL COMPONENTS UNLESS EXPLICITLY TOLD TO ALWAYS USE THE SHADCN COMMAND TO ADD IT.

## Integration Testing

The tests in `tests/backend/ApesDb.Api.Tests` are black-box integration tests. New and modified tests must interact with the application only through its public HTTP API by using `ApiTestClient`.

### Black-Box Boundary

- Exercise endpoints through `ApiTestClient`; authenticate with the test identities or use an anonymous client as required by the scenario.
- Do not access `factory.Services`, `ApplicationDbContext`, repositories, handlers, or other implementation details from a test case.
- Do not query the database to verify an outcome. Observe state and side effects through public API endpoints instead.
- Shared test infrastructure may start containers, apply migrations, seed baseline data, and reset the database. These infrastructure responsibilities do not permit test cases to cross the black-box boundary.
- A generated identifier may be read from an HTTP response only when it is needed to construct a subsequent public API request.

### Verify Is the Assertion Mechanism

- Use Verify snapshots for all assertions in integration tests. Do not use xUnit `Assert` calls or another assertion library.
- `NotificationsStreamTests` is the only exception: it may use xUnit `Assert` calls for incremental SSE framing, timeout, isolation, and cancellation behavior. Keep its final externally observable stream results covered by Verify snapshots, and do not extend this exception to other integration tests.
- Capture HTTP status, headers, and response content with `HttpResponseSnapshot`, using a response contract type when appropriate.
- When a scenario makes multiple requests, combine every relevant observable response into one clearly named object and verify that object once.
- Use `UseParameters` when parameterized cases require distinct, readable snapshot files.
- Keep snapshots focused on externally observable behavior. Do not include database entities or other internal state.

### GET and Read-Only Tests

- Use the assembly-provided `SharedGetApiFactory` for GET and other genuinely read-only scenarios.
- Treat its seeded application state as shared and immutable. A test using this factory must not call an endpoint that changes state.
- Snapshot the complete observable HTTP response needed to establish the endpoint contract, including unsuccessful authentication, authorization, validation, and not-found responses where relevant.

### Mutation Tests

- Use `MutableEndpointApiFactory` with `IClassFixture<MutableEndpointApiFactory>` and `IAsyncLifetime` for POST, PUT, PATCH, DELETE, or any other scenario that can change state.
- Call `_factory.ResetAsync(TestContext.Current.CancellationToken)` from `InitializeAsync` so every test starts from the same seeded baseline.
- Snapshot the mutation response and then call one or more GET endpoints to prove the resulting externally observable state. Verify the mutation and follow-up responses together.
- In `NotificationsStreamTests`, the externally observed SSE event may serve as the follow-up evidence instead of an additional GET request.
- For rejected, idempotent, or otherwise no-op mutations, use follow-up GET requests to prove that externally visible state did not change.
- Perform scenario setup through public API requests when setup beyond the shared seed is required. Do not arrange state by writing directly to the database from the test case.

### Running and Accepting Snapshots

1. Run `dotnet test tests/backend/ApesDb.Api.Tests/ApesDb.Api.Tests.csproj`.
2. Inspect every generated `.received.txt` file and compare it with the corresponding `.verified.txt` baseline.
3. Confirm that every difference represents the intended public HTTP contract. Never accept snapshots blindly.
4. Accept an intentional result by updating or creating the corresponding `.verified.txt` file.
5. Rerun the integration tests until they pass, then confirm that no unreviewed `.received.txt` files remain.
6. Commit the reviewed `.verified.txt` baselines together with the associated test or behavior changes.

Existing integration tests may predate these rules. Do not copy legacy patterns that use direct assertions or inspect application internals; apply this policy whenever an integration test is added or modified.
