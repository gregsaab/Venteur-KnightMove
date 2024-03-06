using Azure.Data.Tables;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ResultAPI.Types;

namespace ResultAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class KnightMoveController : ControllerBase
{
    private readonly TableClient _tableClient;

    public KnightMoveController(TableClient tableClient)
    {
        _tableClient = tableClient;
    }

    /// <summary>
    /// This api call returns the result of a knight move compute
    /// </summary>
    /// <param name="requestParams">This is deserialized from query parameters</param>
    /// <returns>A response with the result</returns>
    [HttpGet]
    public object Get([FromQuery] Request requestParams)
    {
        try
        {
            var result = _tableClient.GetEntityIfExists<ResultsData>("partition", requestParams.operationId);

            return Types.Response.From(result.HasValue ? result.Value : null);
        }
        catch (Exception e)
        {
            return new Response { Message = e.Message };
        }

    }
}
