using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AsyncDemo;

public class SyncWebClient
{
    public static void DemoSyncHttpClient()
    {
        var response1 = FetchData("http://localhost:5002/Product/GetProductById/1");
        var response2 = FetchData("http://localhost:5002/Product/GetProducts");

        Console.WriteLine($"Response1: {response1}");
        Console.WriteLine($"Response2: {response2}");


        Console.WriteLine("All complented.");
    }
    public static void DemoSyncHttpClientWithTask()
    {
        var response1 = Task.Run(() => FetchData("http://localhost:5002/Product/GetProductById/1"));
        var response2 = Task.Run(() => FetchData("http://localhost:5002/Product/GetProducts"));

        response1.Wait();
        response2.Wait();
        Console.WriteLine($"Respons e1: {response1.Result}");
        Console.WriteLine($"Response2: {response2.Result}");


        Console.WriteLine("All complented.");
    }

    public static void DemoSyncHttpClientWithTaskWithoutAsync()
    {
        var response1 = Task.Run(() => FetchData("http://localhost:5002/Product/GetProductById/1"));
        var response2 = Task.Run(() => FetchData("http://localhost:5002/Product/GetProducts"));

        var continuationTask = Task.WhenAny(response1, response2).ContinueWith(completedTask=>
        {
            var completeResponse = completedTask.Result;
            System.Console.WriteLine($"Response: {completeResponse.Result}");

            var remainingTask = completeResponse == response1 ? response2 : response1;
            System.Console.WriteLine("dupa" + remainingTask.Result);

            Console.WriteLine("All requests completed.");
        });


        continuationTask.Wait();
    }

    public static async Task DemoSyncHttpClientWithTaskWithAsync()
    {
        var response1 = Task.Run(() => FetchData("http://localhost:5002/Product/GetProductById/1"));
        var response2 = Task.Run(() => FetchData("http://localhost:5002/Product/GetProducts"));

        Task<string> firstCompletedTask = await Task.WhenAny(response1, response2);

        Console.WriteLine($"Response: {await firstCompletedTask}");

        Task<string> remainingTask = firstCompletedTask == response1 ? response2 : response1;
        Console.WriteLine("dupa" + remainingTask.Result);

        Console.WriteLine("All requests completed.");
    }
    
    public static string FetchData(string url)
    {
        using WebClient webClient = new WebClient();
        try
        {
            return webClient.DownloadString(url);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }

    }

}
