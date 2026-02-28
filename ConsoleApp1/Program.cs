// See https://aka.ms/new-console-template for more information

using ConsoleApp1;

const string apiUrl = "https://randomuser.me/api/";

IApiClient apiClient = new HttpApiClient();

await App.RunAsync(apiClient, Console.Out, apiUrl);