
namespace AsyncDemo;

public class AsyncWebClient
{

    public static async Task DemoAsyncHttpClient()
    {
        var response1 = FetchDataAsync("http://localhost:5002/Product/GetProductById/1");
        var response2 = FetchDataAsync("http://localhost:5002/Product/GetProducts");

        var result1 = await response1;
        var result2 = await response2;

        Console.WriteLine($"Result1: {result1}");
        Console.WriteLine($"Result2: {result2}");


        Console.WriteLine("All complented.");
    }
    public static async Task DemoAsyncHttpClientWithTaskWithAsync()
    {
        var response1 = FetchDataAsync("http://localhost:5002/Product/GetProductById/1");
        var response2 = FetchDataAsync("http://localhost:5002/Product/GetProducts");

        Task<string> firstCompletedTask = await Task.WhenAny(response1, response2);

        Console.WriteLine($"Response: {await firstCompletedTask}");

        Task<string> remainingTask = firstCompletedTask == response1 ? response2 : response1;
        Console.WriteLine("dupa" + remainingTask.Result);

        Console.WriteLine("All requests completed.");
    }

    private static async Task<string> FetchDataAsync(string url)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

}
