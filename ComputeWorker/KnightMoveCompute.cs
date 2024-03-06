using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Common.Types;
using ComputeWorker.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ComputeWorker
{
    public class KnightMoveCompute
    {
        private readonly ILogger _logger;
        private readonly ISolver _solver;

        public KnightMoveCompute(ILoggerFactory loggerFactory, ISolver solver)
        {
            _logger = loggerFactory.CreateLogger<KnightMoveCompute>();
            _solver = solver;
        }

        /// <summary>
        /// This is a Function App method that is triggered off of items being enqueued
        /// onto the knightmoverequest queue. We get the connection string from the configured vault
        /// in Azure. We return an entry that we store in a NoSQL Table. 
        /// </summary>
        /// <param name="myQueueItem">The base64 json string that the api enqueues</param>
        /// <returns></returns>
        [Function("knightmovecompute")]
        [TableOutput("Results", Connection = "STORAGE")]
        public async Task<ResultsData?> Run([QueueTrigger("knightmoverequests", Connection = "REQUEST_QUEUE")] string myQueueItem)
        {
            var request = JsonSerializer.Deserialize<SolveRequest>(myQueueItem);
            
            if (request == null)
            {
                _logger.LogWarning("Invalid request");
                return null;
            }
            
            _logger.LogInformation("Processing request");
            
            try
            {
                var solution = _solver.Solve(request.Start, request.End, PieceType.Knight);

                var results = new ResultsData(request.RequestId.ToString(), solution.Moves, solution.NumberOfMoves,
                    request.Start, request.End);

                if (string.IsNullOrEmpty(request.Callback)) return results;

                // if the request has a callback url, let's try to send a webhook callback
                await WebhookCallback.Send(request.Callback, results);

                return results;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred processing request {request.RequestId}", e);
                return new ResultsData(request.RequestId.ToString(), e.Message);
            }
        }
    }
}

