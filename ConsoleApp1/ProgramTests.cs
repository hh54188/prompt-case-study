using System.Net;
using Moq;

namespace ConsoleApp1;

public class ProgramTests
{
    [Fact]
    public async Task RunAsync_WhenApiReturnsValidJson_WritesFormattedJsonAndHeaders()
    {
        const string rawJson = @"{""results"":[],""info"":{""seed"":""x"",""version"":""1.4""}}";
        var mockApiClient = new Mock<IApiClient>();
        mockApiClient
            .Setup(m => m.GetApiResponseAsync(It.IsAny<string>()))
            .ReturnsAsync(rawJson);
        using var output = new StringWriter();

        await App.RunAsync(mockApiClient.Object, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("正在请求 Random User API...", text);
        Assert.Contains("API 返回结果：", text);
        Assert.Contains("\"results\"", text);
        Assert.Contains("\"info\"", text);
    }

    [Fact]
    public async Task RunAsync_WhenApiThrowsHttpRequestException_WritesRequestFailedMessage()
    {
        var mockApiClient = new Mock<IApiClient>();
        mockApiClient
            .Setup(m => m.GetApiResponseAsync(It.IsAny<string>()))
            .ThrowsAsync(new HttpRequestException("Connection refused"));
        using var output = new StringWriter();

        await App.RunAsync(mockApiClient.Object, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("请求失败", text);
    }

    [Fact]
    public async Task RunAsync_WhenApiThrowsBadRequest_Writes400Message()
    {
        var mockApiClient = new Mock<IApiClient>();
        mockApiClient
            .Setup(m => m.GetApiResponseAsync(It.IsAny<string>()))
            .ThrowsAsync(new HttpRequestException("Request failed", 
                null, HttpStatusCode.BadRequest));

        await using var output = new StringWriter();
        await App.RunAsync(mockApiClient.Object, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("请求失败", text);
    }

    [Fact]
    public async Task RunAsync_WhenApiThrowsNotFound_WritesRequestFailedMessage()
    {
        var mockApiClient = new Mock<IApiClient>();
        mockApiClient
            .Setup(m => m.GetApiResponseAsync(It.IsAny<string>()))
            .ThrowsAsync(new HttpRequestException(
                "Request failed", null, HttpStatusCode.NotFound));

        await using var output = new StringWriter();
        await App.RunAsync(mockApiClient.Object, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("请求失败", text);
    }

    [Fact]
    public async Task RunAsync_WhenApiThrowsInternalServerError_WritesRequestFailedMessage()
    {
        var mockApiClient = new Mock<IApiClient>();
        mockApiClient
            .Setup(m => m.GetApiResponseAsync(It.IsAny<string>()))
            .ThrowsAsync(new HttpRequestException("Request failed", 
                null, HttpStatusCode.InternalServerError));

        await using var output = new StringWriter();
        await App.RunAsync(mockApiClient.Object, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("请求失败", text);
    }

    [Fact]
    public async Task RunAsync_WhenApiThrowsTaskCanceledException_WritesTimeoutMessage()
    {
        var mockApiClient = new Mock<IApiClient>();
        mockApiClient
            .Setup(m => m.GetApiResponseAsync(It.IsAny<string>()))
            .ThrowsAsync(new TaskCanceledException());
        using var output = new StringWriter();

        await App.RunAsync(mockApiClient.Object, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("请求超时。", text);
    }

    [Fact]
    public async Task RunAsync_CallsApiClientWithGivenUrl()
    {
        const string url = "https://randomuser.me/api/";
        var mockApiClient = new Mock<IApiClient>();
        mockApiClient
            .Setup(m => m.GetApiResponseAsync(url))
            .ReturnsAsync("{}");
        using var output = new StringWriter();

        await App.RunAsync(mockApiClient.Object, output, url);

        mockApiClient.Verify(m => m.GetApiResponseAsync(url), Times.Once);
    }
}
