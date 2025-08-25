namespace Assignment.Api.SuggestTask;
using System.Text.RegularExpressions;

public class TaskSuggester
{
    private const string NoTaskFound = "NoTaskFound";
    private const string ResetPasswordTask =  "ResetPasswordTask";
    private const string CheckOrderStatusTask = "CheckOrderStatusTask";
    
    // hardcoded dictionary for exact matching
    private static readonly Dictionary<string, string> TaskDictionary = new(StringComparer.OrdinalIgnoreCase)
    {
        ["reset password"] = ResetPasswordTask,
        ["forgot password"] = ResetPasswordTask,
        ["check order"] = CheckOrderStatusTask,
        ["track order"] = CheckOrderStatusTask
    };
    
    // Extended matching: regex pattern matching dictionary
    // if utterance contains (reset OR forgot) AND (password) return ResetPasswordTask
    // if utterance contains (check OR track) AND (order) return CheckOrderStatusTask
    // ** order matters - no lookaheads
    private static readonly Dictionary<string, string> TaskRegExPatterns = new()
    {
        [@"\b(reset|forgot)\b.*?\b(password)\b"] = ResetPasswordTask,
        [@"\b(check|track)\b.*?\b(order)\b"] = CheckOrderStatusTask
    };

    public string Match(string? utterance)
    {
        if (string.IsNullOrWhiteSpace(utterance)) return NoTaskFound;
        
        // Exact matching
        foreach (var pair in TaskDictionary)
        {
            if (utterance.Contains(pair.Key, StringComparison.OrdinalIgnoreCase)) 
                return pair.Value;
        }
        
        // Extended matching
        foreach (var pair in TaskRegExPatterns)
        {
            if (Regex.IsMatch(utterance, pair.Key, RegexOptions.IgnoreCase))
                return pair.Value;
        }
        
        return NoTaskFound;
    }
}
