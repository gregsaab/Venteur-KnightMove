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

    public string GetAllMoveStrings()
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