using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ComputeWorker
{
    public class knightmovecompute
    {
        private readonly ILogger _logger;

        public knightmovecompute(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<knightmovecompute>();
        }

        [Function("knightmovecompute")]
        public void Run([QueueTrigger("knightmoverequests", Connection = "BlobEndpoint=https://venteur.blob.core.windows.net/;QueueEndpoint=https://venteur.queue.core.windows.net/;FileEndpoint=https://venteur.file.core.windows.net/;TableEndpoint=https://venteur.table.core.windows.net/;SharedAccessSignature=sv=2022-11-02&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2025-03-05T03:56:59Z&st=2024-03-04T19:56:59Z&spr=https&sig=W4msAT3figrs%2FDZNaGZYzVik3L0R5TniJEy1m1fZrhA%3D")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}

