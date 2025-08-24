namespace Assignment.Api.SuggestTask;

public class SuggestTaskRequest
{
    public string? Utterance { get; set; }
    public string? UserId { get; set; }
    public string? SessionId { get; set; }
    public string? Timestamp { get; set; }
}

public class SuggestTaskResponse
{
    public string Task { get; init; } = string.Empty;
    public string Timestamp { get; init; } = string.Empty;
}
