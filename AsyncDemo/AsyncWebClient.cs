
namespace AsyncDemo;

public class AsyncWebClient
{

    public static async Task DemoAsyncHttpClient()
    {
        var response1 = await FetchDataAsync("http://localhost:5002/Product/GetProductById/1");
        var response2 = await FetchDataAsync("http://localhost:5002/Product/GetProducts");

        Console.WriteLine($"Result1: {response1}");
        Console.WriteLine($"Result2: {response2}");

        Console.WriteLine("All complented.");
    }
    public static void DemoSyncHttpClientWithLazyAsync()
    {
        var response1 = new Lazy<Task<string>>(() => FetchDataAsync("http://localhost:5002/Product/GetProductById/1"));
        var response2 = new Lazy<Task<string>>(() => FetchDataAsync("http://localhost:5002/Product/GetProducts"));

        Console.WriteLine($"Result1: { response1.Value.Result}");
        Console.WriteLine($"Result2: { response2.Value.Result}");

        Console.WriteLine("All complented.");
    }

    public static async Task DemoAsyncHttpClientWithLazyAsync()
    {
        var response1 = new Lazy<Task<string>>(() => FetchDataAsync("http://localhost:5002/Product/GetProductById/1"));
        var response2 = new Lazy<Task<string>>(() => FetchDataAsync("http://localhost:5002/Product/GetProducts"));

        Console.WriteLine($"Result1: {await response1.Value}");
        Console.WriteLine($"Result2: {await response2.Value}");

        Console.WriteLine("All complented.");
    }

    public static async Task DemoAsyncHttpClientWithTaskWithAsync()
    {
        var response1 = FetchDataAsync("http://localhost:5002/Product/GetProductById/1");
        var response2 = FetchDataAsync("http://localhost:5002/Product/GetProducts");

        Task<string> firstCompletedTask = await Task.WhenAny(response1, response2);

        Console.WriteLine($"Response: {await firstCompletedTask}");

        Task<string> remainingTask = firstCompletedTask == response1 ? response2 : response1;
        Console.WriteLine("Remainig task:" + remainingTask.Result);

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
