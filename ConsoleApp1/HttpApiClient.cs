namespace ConsoleApp1;

/// <summary>
/// 基于 <see cref="HttpClient"/> 的 <see cref="IApiClient"/> 实现。
/// </summary>
public class HttpApiClient : IApiClient
{
    private readonly HttpClient _httpClient = new();

    /// <inheritdoc />
    public async Task<string> GetApiResponseAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
