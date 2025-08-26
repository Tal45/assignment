namespace Assignment.Tests;
using Xunit;
using Api.SuggestTask;

// with respect to MethodUnderTest_Condition_ExpectedResult naming convention
// and arrange - act - assert pattern

public class TaskSuggesterTests
{
    [Fact]
    public void Match_TextContainsResetPassword_ReturnsResetPasswordTask()
    {
        var sut = new TaskSuggester();
        var result = sut.Match("help me reset password");
        Assert.Equal("ResetPasswordTask", result);
    }
    
    [Fact]
    public void Match_TextContainsForgotPasswordCapitalized_ReturnsResetPasswordTask()
    {
        var sut = new TaskSuggester();
        var result = sut.Match("FORGOT password");
        Assert.Equal("ResetPasswordTask", result);
    }
    
    [Fact]
    public void Match_TextContainsCheckOrder_ReturnsCheckOrderStatusTask()
    {
        var sut = new TaskSuggester();
        var result = sut.Match("check order");
        Assert.Equal("CheckOrderStatusTask", result);
    }
    
    [Fact]
    public void Match_TextContainsForgotMyPassword_ReturnsResetPasswordTask()
    {
        var sut = new TaskSuggester();
        var result = sut.Match("I forgot my password");
        Assert.Equal("ResetPasswordTask", result);
    }
    
    [Fact]
    public void Match_TextNoMatch_ReturnsNoTaskFound()
    {
        var sut = new TaskSuggester();
        var result = sut.Match("how are you");
        Assert.Equal("NoTaskFound", result);
    }
    
    [Fact]
    public void Match_TextContainsWhitespaceOnly_ReturnsNoTaskFound()
    {
        var sut = new TaskSuggester();
        var result = sut.Match("    ");
        Assert.Equal("NoTaskFound", result);
    }
}