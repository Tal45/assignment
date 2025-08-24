namespace Assignment.Api.SuggestTask;

public class TaskSuggester
{
    private const string NoTaskFound = "NoTaskFound";
    private const string ResetPasswordTask =  "ResetPasswordTask";
    private const string CheckOrderStatusTask = "CheckOrderStatusTask";

    private static readonly Dictionary<string, string> TaskDictionary = new(StringComparer.OrdinalIgnoreCase)
    {
        ["reset password"] = ResetPasswordTask,
        ["forgot password"] = ResetPasswordTask,
        ["check order"] = CheckOrderStatusTask,
        ["track order"] = CheckOrderStatusTask
    };

    public string Match(string? utterance)
    {
        if (string.IsNullOrWhiteSpace(utterance)) return NoTaskFound;

        foreach (var pair in TaskDictionary)
        {
            if (utterance.Contains(pair.Key, StringComparison.OrdinalIgnoreCase)) 
                return pair.Value;
        }
        
        return NoTaskFound;
    }
}
