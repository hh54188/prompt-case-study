using System.Net;
using System.Text.Json;

namespace ConsoleApp1;

/// <summary>
/// 应用程序主流程，可注入 <see cref="IApiClient"/> 与输出目标以便测试。
/// </summary>
public static class App
{
    /// <summary>
    /// 请求指定 URL、格式化 JSON 并写入 <paramref name="output"/>，异常时写入错误信息。
    /// </summary>
    public static async Task RunAsync(IApiClient apiClient, TextWriter output, string url)
    {
        try
        {
            await output.WriteLineAsync("正在请求 Random User API...\n");
            var json = await apiClient.GetApiResponseAsync(url);

            using var doc = JsonDocument.Parse(json);
            var formatted = JsonSerializer.Serialize(doc, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
            await output.WriteLineAsync("API 返回结果：\n");
            await output.WriteLineAsync(formatted);
        }
        catch (HttpRequestException ex)
        {
            await output.WriteLineAsync($"请求失败");
        }
        catch (TaskCanceledException)
        {
            await output.WriteLineAsync("请求超时。");
        }
    }
}
