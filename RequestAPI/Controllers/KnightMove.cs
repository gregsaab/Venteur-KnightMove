using System.Text.Json;
using Azure.Storage.Queues;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
using RequestAPI.Types;

namespace RequestAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class KnightMoveController : ControllerBase
{
    private readonly ILogger<KnightMoveController> _logger;
    private readonly QueueClient _queueClient;

    public KnightMoveController(ILogger<KnightMoveController> logger, QueueClient queueClient)
    {
        _logger = logger;
        _queueClient = queueClient ?? throw new Exception("Did not receive queue client");
    }

    /// <summary>
    /// This function takes knight move requests via a POST, does validation on the parameters
    /// then enqueues valid requests onto message queue
    /// </summary>
    /// <param name="requestParams">This is deserialized from query parameters</param>
    /// <returns>A response with a message and operation id</returns>
    [HttpPost]
    public Response Post([FromQuery] Request requestParams)
    {
        var guid = Guid.NewGuid();
        var startPosition = new Position(requestParams.source);
        var endPosition = new Position(requestParams.target);

        if (!startPosition.IsValid())
        {
            _logger.LogError($"Received invalid source parameter: {requestParams.source}");
            return new Response { Message = $"The source parameter is not valid: {requestParams.source}", OperationId = guid};
        }

        if (!endPosition.IsValid())
        {
            _logger.LogError($"Received invalid target parameter: {requestParams.target}");
            return new Response { Message = $"The target parameter is not valid: {requestParams.target}", OperationId = guid};
        }
        
        var request = new SolveRequest { Start = "A1", End = "F5", RequestId = guid, Callback = requestParams.callback };
        _queueClient.SendMessageAsync(JsonSerializer.Serialize(request));
        
        return new Response{OperationId = guid, Message = $"Operation Id {guid} was created. Please query it to find your results."};
     }
}
