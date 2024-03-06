using System.Text.Json;
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

        [Function("knightmovecompute")]
        [TableOutput("Results", Connection = "STORAGE")]
        public ResultsData? Run([QueueTrigger("knightmoverequests", Connection = "REQUEST_QUEUE")] string myQueueItem)
        {
            var request = JsonSerializer.Deserialize<SolveRequest>(myQueueItem);

            if (request == null)
                return null;

            var solution = _solver.Solve(request.Start, request.End);

            return new ResultsData(request.RequestId.ToString(), solution.Moves, solution.NumberOfMoves, request.Start, request.End);
        }
    }
}

