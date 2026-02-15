// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using ConsoleApp1;

const string apiUrl = "https://randomuser.me/api/";

using var httpClient = new HttpClient();
IApiClient apiClient = new HttpApiClient(httpClient);

try
{
    Console.WriteLine("正在请求 Random User API...\n");
    var json = await apiClient.GetApiResponseAsync(apiUrl);

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