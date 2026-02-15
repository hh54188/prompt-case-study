using System.Net;

namespace ConsoleApp1;

public class ProgramTests
{
    [Fact]
    public async Task RunAsync_WhenApiReturnsValidJson_WritesFormattedJsonAndHeaders()
    {
        const string rawJson = @"{""results"":[],""info"":{""seed"":""x"",""version"":""1.4""}}";
        var apiClient = new StubApiClient(rawJson);
        using var output = new StringWriter();

        await App.RunAsync(apiClient, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("正在请求 Random User API...", text);
        Assert.Contains("API 返回结果：", text);
        Assert.Contains("\"results\"", text);
        Assert.Contains("\"info\"", text);
    }

    [Fact]
    public async Task RunAsync_WhenApiThrowsHttpRequestException_WritesRequestFailedMessage()
    {
        var apiClient = new StubApiClient(ex: new HttpRequestException("Connection refused"));
        using var output = new StringWriter();

        await App.RunAsync(apiClient, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("请求失败: Connection refused", text);
    }

    [Fact]
    public async Task RunAsync_WhenApiThrowsTaskCanceledException_WritesTimeoutMessage()
    {
        var apiClient = new StubApiClient(ex: new TaskCanceledException());
        using var output = new StringWriter();

        await App.RunAsync(apiClient, output, "https://example.com/api");

        var text = output.ToString();
        Assert.Contains("请求超时。", text);
    }

    [Fact]
    public async Task RunAsync_CallsApiClientWithGivenUrl()
    {
        const string url = "https://randomuser.me/api/";
        var apiClient = new StubApiClient("{}", recordUrl: true);
        using var output = new StringWriter();

        await App.RunAsync(apiClient, output, url);

        Assert.Equal(url, apiClient.LastRequestedUrl);
    }

    /// <summary>
    /// 测试用 IApiClient 桩，可配置返回内容或抛出异常。
    /// </summary>
    private sealed class StubApiClient : IApiClient
    {
        private readonly string? _json;
        private readonly Exception? _exception;

        public StubApiClient(string? json = null, Exception? ex = null, bool recordUrl = false)
        {
            _json = json;
            _exception = ex;
            RecordUrl = recordUrl;
        }

        private bool RecordUrl { get; }
        public string? LastRequestedUrl { get; private set; }

        public Task<string> GetApiResponseAsync(string url)
        {
            if (RecordUrl) LastRequestedUrl = url;
            if (_exception != null) throw _exception;
            return Task.FromResult(_json ?? "{}");
        }
    }
}
