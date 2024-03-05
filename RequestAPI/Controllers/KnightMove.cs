using System.Text.Json;
using Azure.Storage.Queues;
using Common.Types;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public Guid Get()
    {
        var guid = Guid.NewGuid();
        var request = new SolveRequest { Start = "A1", End = "F5", RequestId = guid };
        using var _ = _logger.BeginScope(new Dictionary<string, object>{{"RequestId", guid}});
        _queueClient.SendMessageAsync(JsonSerializer.Serialize(request));
        return guid;
     }
}
