namespace Common.Types;

public class SolveRequest
{
    public string Start { get; set; }
    public string End { get; set; }
    public Guid RequestId { get; set; }
    public string Callback { get; set; }
}