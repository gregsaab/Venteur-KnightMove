using System.Net.Http.Json;
using Common.Types;

namespace ComputeWorker.Utils;

public static class WebhookCallback
{
    public static async Task Send(string url, ResultsData resultsData)
    {
        try
        {
            var httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(url, resultsData);
        }
        catch (Exception) { }
    }
    
}