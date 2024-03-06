
using Common.Types;

namespace ResultAPI.Types;

public class Response
{
    public string OperationId { get; set; }
    public string? ShortestPath { get; set; }
    public int? NumberOfMoves { get; set; }
    public string? Starting { get; set; }
    public string? Ending { get; set; }
    public string? Error { get; set; }
    public string? Message { get; set; }

    public static Response From(ResultsData? results)
    {
        if (results == null)
            return new Response { Message = "No results for this operation id, please check back later ;)" };

        if (!string.IsNullOrEmpty(results.Error))
            return new Response { OperationId = results.OperationId, Message = results.Error };
        
        return new Response
        {
            OperationId = results.OperationId, 
            ShortestPath = results.ShortestPath,
            NumberOfMoves = results.NumberOfMoves, 
            Ending = results.Ending, 
            Starting = results.Starting
        };
    }
    
}