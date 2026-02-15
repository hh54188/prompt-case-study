namespace ConsoleApp1;

/// <summary>
/// 定义获取 API 响应内容的契约。
/// </summary>
public interface IApiClient
{
    /// <summary>
    /// 向指定 URL 发送 GET 请求并返回响应内容的字符串。
    /// </summary>
    /// <param name="url">请求的目标 URL。</param>
    /// <returns>响应体的原始字符串，通常为 JSON。</returns>
    /// <exception cref="HttpRequestException">请求失败或响应状态码表示失败时抛出。</exception>
    /// <exception cref="TaskCanceledException">请求被取消或超时时抛出。</exception>
    Task<string> GetApiResponseAsync(string url);
}
