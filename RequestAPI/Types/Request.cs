namespace RequestAPI.Types;

public class Request
{
    public string source { get; set; }
    public string target { get; set; }
    public string? callback { get; set; }
}