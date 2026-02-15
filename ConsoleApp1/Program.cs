// See https://aka.ms/new-console-template for more information

using System.Text.Json;

const string apiUrl = "https://randomuser.me/api/";

using var httpClient = new HttpClient();
try
{
    Console.WriteLine("正在请求 Random User API...\n");
    var json = await GetApiResponseAsync(httpClient, apiUrl);

    // 格式化输出 JSON（缩进便于阅读）
    using var doc = JsonDocument.Parse(json);
    var formatted = JsonSerializer.Serialize(doc, new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });
    Console.WriteLine("API 返回结果：\n");
    Console.WriteLine(formatted);
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"请求失败: {ex.Message}");
}
catch (TaskCanceledException)
{
    Console.WriteLine("请求超时。");
}

/// <summary>
/// 向指定 URL 发送 GET 请求并返回响应内容的字符串。
/// </summary>
/// <param name="client">用于发送请求的 <see cref="HttpClient"/> 实例。</param>
/// <param name="url">请求的目标 URL。</param>
/// <returns>响应体的原始字符串，通常为 JSON。</returns>
/// <exception cref="HttpRequestException">请求失败或响应状态码表示失败时抛出。</exception>
/// <exception cref="TaskCanceledException">请求被取消或超时时抛出。</exception>
static async Task<string> GetApiResponseAsync(HttpClient client, string url)
{
    var response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadAsStringAsync();
}