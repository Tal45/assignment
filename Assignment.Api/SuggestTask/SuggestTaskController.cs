using Microsoft.AspNetCore.Mvc;
namespace Assignment.Api.SuggestTask;

[Route("suggestTask")]
[ApiController]
public sealed class SuggestTaskController : ControllerBase
{
    private readonly ILogger<SuggestTaskController> _logger;
    private readonly TaskSuggester _taskSuggester;

    public SuggestTaskController(TaskSuggester taskSuggester, ILogger<SuggestTaskController> logger)
    {
        _taskSuggester = taskSuggester;
        _logger = logger;
    }

    [HttpPost]
    public ActionResult<SuggestTaskResponse> Post(SuggestTaskRequest request)
    {
        var task = _taskSuggester.Match(request.Utterance);

        var response = new SuggestTaskResponse
        {
            Task = task,
            Timestamp = DateTime.UtcNow.ToString("O") // utc for consistency
        };
        
        _logger.LogInformation("POST /suggestTask sessionId={SessionId} result={Result}", request.SessionId, task);
        
        return Ok(response);
    }
}