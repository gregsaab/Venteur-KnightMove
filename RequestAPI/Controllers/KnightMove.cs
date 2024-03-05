using Azure.Storage.Queues;
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
        using var _ = _logger.BeginScope(new Dictionary<string, object>{{"RequestId", guid}});
        _queueClient.SendMessageAsync($"boom {guid}");
        return guid;
     }
}
