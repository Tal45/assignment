using Microsoft.AspNetCore.Mvc;
namespace Assignment.Api.SuggestTask;

[ApiController]
[Route("suggestTask")]
public sealed class SuggestTaskController : ControllerBase
{
    private readonly ILogger<SuggestTaskController> _logger;
    private readonly TaskSuggester _taskSuggester;

    public SuggestTaskController(TaskSuggester taskSuggester, ILogger<SuggestTaskController> logger)
    {
        _taskSuggester = taskSuggester;
        _logger = logger;
    }

    private static Task SimulateExternalDep()
    {
        if (Random.Shared.NextDouble() < 0.5) // fail randomly
            throw new HttpRequestException("Simulated failure.");
        return Task.CompletedTask;
    }

    [HttpPost]
    public async Task<ActionResult<SuggestTaskResponse>> Post(SuggestTaskRequest request)
    {
        var task = _taskSuggester.Match(request.Utterance);

        var ok = false;
        for (int attempt = 1; attempt <= 3; attempt++)
        {
            try
            {
                await SimulateExternalDep();
                ok = true;
                break;
            }
            catch when (attempt < 3)
            {
                _logger.LogWarning("External dependency failure (attempt: {Attempt}), retrying...", attempt);
                await Task.Delay(100);
            }
            catch (Exception e) // if failed on third attempt
            {
                _logger.LogError(e, "External step failed after {Attempt} attempts", attempt);
            }
        }
        var response = new SuggestTaskResponse
        {
            Task = task,
            Timestamp = DateTime.UtcNow.ToString("O") // utc for consistency
        };
        
        _logger.LogInformation("POST /suggestTask sessionId={SessionId} result={Result} externalOk={ExtOk}", request.SessionId, task, ok);
        
        return Ok(response);
    }
}