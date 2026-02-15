namespace ConsoleApp1;

/// <summary>
/// 基于 <see cref="HttpClient"/> 的 <see cref="IApiClient"/> 实现。
/// </summary>
public class HttpApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// 使用指定的 <see cref="HttpClient"/> 创建 <see cref="HttpApiClient"/> 实例。
    /// </summary>
    /// <param name="httpClient">用于发送 HTTP 请求的客户端实例。</param>
    /// <exception cref="ArgumentNullException"><paramref name="httpClient"/> 为 <c>null</c> 时抛出。</exception>
    public HttpApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    /// <inheritdoc />
    public async Task<string> GetApiResponseAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
