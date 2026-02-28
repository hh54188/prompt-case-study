namespace ConsoleApp1;

public class HttpApiClientTests
{
    [Fact]
    public async Task GetApiResponseAsync_WithNullUrl_ThrowsInvalidOperationException()
    {
        var sut = new HttpApiClient();

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.GetApiResponseAsync(null!));
    }

    [Fact]
    public async Task GetApiResponseAsync_WithRelativeUrl_ThrowsInvalidOperationException()
    {
        var sut = new HttpApiClient();

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.GetApiResponseAsync("/api"));
    }
}
