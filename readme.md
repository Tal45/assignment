# Assignment – Suggest Task API

Small ASP.NET Core Web API that suggests a task from a user utterance.

## Requirements
- .NET SDK 9.0

## Dependencies
- API: FluentValidation.AspNetCore
- Tests: Microsoft.AspNetCore.Mvc.Testing

## Run the API
```
cd Assignment.Api
dotnet run
```

The app will print a local URL like: http://localhost:5299

## Endpoint
POST /suggestTask

Request (JSON):
```
{
    "utterance": "reset password",
    "userId": "12345",
    "sessionId": "abcde-67890",
    "timestamp": "2025-08-21T12:00:00Z"
}
```

Response (200):
```
{
    "task": "ResetPasswordTask",
    "timestamp": "2025-08-24T12:34:56.0000000Z"
}
```

Validation
- `utterance` is required. Empty/null leads to 400 Bad Request with error details.

## Matching logic
- Exact phrases (case-insensitive):
    - "reset password", "forgot password" --> ResetPasswordTask
    - "check order", "track order" --> CheckOrderStatusTask
- Extended matching (bonus): simple regex rules allow variants like "I forgot my password" and "track my order".

## Quick test

Windows CMD:
```
curl -X POST -H "Content-Type: application/json" -d "{\"utterance\":\"check order\",\"userId\":\"12345\",\"sessionId\":\"abcde-67890\",\"timestamp\":\"2025-08-21T12:00:00Z\"}" http://localhost:5299/suggestTask
```

Rider HTTP file:
- Assignment.Api/requests.http for ready requests.

## Run tests
```
cd Assignment.Tests
dotnet test
```
- Unit: TaskSuggester (“reset password” --> ResetPasswordTask)
- Integration: in-memory POST /suggestTask (200 on valid, 400 on invalid)

## Project structure
```
Assignment/
│
├── Assignment.Api/
│   ├── SuggestTask/
│   │   ├── Models.cs                       # request/response DTOs
│   │   ├── SuggestTaskRequestValidator.cs  # FluentValidation validator
│   │   ├── SuggestTaskController.cs        # controller
│   │   └── TaskSuggester.cs                # business logic
│   └── Program.cs                          # entry point
├── Assignment.Tests/
│   ├── TaskSuggesterTests.cs               # unit tests
│   └── ApiTests.cs                         # integration tests
└── README.md
```