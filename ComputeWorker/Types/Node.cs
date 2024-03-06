using Common.Types;

namespace ComputeWorker;

internal class Node
{
    public Position Position { get; init; }
    public int Index { get; init; } = 0;
			
    public Node? Previous { get; init; }

    private Node() { }
    public static Node From(Position position)
    {
        return new Node { Position = position };
    }
			
    public Node Next(Position position)
    {
        return new Node { Position = position, Previous = this, Index = Index + 1};
    }

    /// <summary>
    /// This will iterate through the Node's parents (Previous) and append each respective position string to an array
    /// Once we append the position strings all the way up the root, we just return a string that is formed by joining the
    /// positions together
    /// </summary>
    /// <returns></returns>
    public string GetFullMovementPath()
    {
        var positions = new List<string>();

        var node = this;
        while(node != null)
        {
            positions.Insert(0, node.Position.ToString());
            node = node.Previous;
        }

        return string.Join(':', positions);
    }
			
}