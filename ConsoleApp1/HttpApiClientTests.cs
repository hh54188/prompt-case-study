using System.Net;

namespace ConsoleApp1;

public class HttpApiClientTests
{
    [Fact]
    public void Constructor_WithNullHttpClient_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new HttpApiClient(null!));
        Assert.Equal("httpClient", ex.ParamName);
    }

    [Fact]
    public async Task GetApiResponseAsync_WithSuccessStatusCode_ReturnsResponseContent()
    {
        const string expectedContent = @"{""results"":[],""info"":{}}";
        var handler = new FakeHttpMessageHandler(HttpStatusCode.OK, expectedContent);
        using var httpClient = new HttpClient(handler);
        var sut = new HttpApiClient(httpClient);

        var result = await sut.GetApiResponseAsync("https://example.com/api");

        Assert.Equal(expectedContent, result);
    }

    [Fact]
    public async Task GetApiResponseAsync_SendsGetRequestToGivenUrl()
    {
        var handler = new FakeHttpMessageHandler(HttpStatusCode.OK, "{}");
        using var httpClient = new HttpClient(handler);
        var sut = new HttpApiClient(httpClient);
        const string url = "https://randomuser.me/api/";

        await sut.GetApiResponseAsync(url);

        Assert.NotNull(handler.LastRequest);
        Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
        Assert.Equal(url, handler.LastRequest.RequestUri?.ToString());
    }

    [Fact]
    public async Task GetApiResponseAsync_WithNonSuccessStatusCode_ThrowsHttpRequestException()
    {
        var handler = new FakeHttpMessageHandler(HttpStatusCode.NotFound, "Not Found");
        using var httpClient = new HttpClient(handler);
        var sut = new HttpApiClient(httpClient);

        var ex = await Assert.ThrowsAsync<HttpRequestException>(
            () => sut.GetApiResponseAsync("https://example.com/api"));

        Assert.NotNull(ex.Message);
    }

    [Fact]
    public async Task GetApiResponseAsync_WithServerError_ThrowsHttpRequestException()
    {
        var handler = new FakeHttpMessageHandler(HttpStatusCode.InternalServerError, "Error");
        using var httpClient = new HttpClient(handler);
        var sut = new HttpApiClient(httpClient);

        await Assert.ThrowsAsync<HttpRequestException>(
            () => sut.GetApiResponseAsync("https://example.com/api"));
    }
}
