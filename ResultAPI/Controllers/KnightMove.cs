using Azure.Data.Tables;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
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
    [HttpPost]
    public Response Post([FromQuery] Request requestParams)
    {
        var results = _tableClient.Query<ResultsData>("PartitionKey='CA' & RowKey ='123456789'")?.ToList();
        
        if (results == null || results.Count == 0)
        {
            return new Response
            {
                OperationId = requestParams.operationId,
                Message = "No results for this operation id, please check back later ;)"
            };
        }

        var result = results.First();
        return ResultAPI.Types.Response.From(result);
    }
}
