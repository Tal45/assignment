using System.Net;

namespace Assignment.Tests;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

using Api.SuggestTask;

public sealed class ApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SuggestTask_HappyPath_Returns200_WithResetPasswordTask()
    {
        var payload = new
        {
            utterance = "reset password",
            userId = "12345",
            sessionId = "abcde-67809",
            timestamp = "2025-08-21T12:00:00Z"
        };
        
        var resp = await _client.PostAsJsonAsync("/suggestTask", payload);
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        
        var body = await resp.Content.ReadFromJsonAsync<SuggestTaskResponse>();
        Assert.NotNull(body);
        Assert.Equal("ResetPasswordTask", body!.Task);
        Assert.False(string.IsNullOrWhiteSpace(body.Timestamp));
    }
    
    [Fact]
    public async Task SuggestTask_WhitespaceUtterance_Returns400()
    {
        var resp = await _client.PostAsJsonAsync("/suggestTask", new { utterance = "   " });
        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
        
        var text = await resp.Content.ReadAsStringAsync();
        Assert.Contains("utterance", text, System.StringComparison.OrdinalIgnoreCase);
    }
}