// See https://aka.ms/new-console-template for more information

using ConsoleApp1;

const string apiUrl = "https://randomuser.me/api/";

using var httpClient = new HttpClient();
IApiClient apiClient = new HttpApiClient(httpClient);

await App.RunAsync(apiClient, Console.Out, apiUrl);