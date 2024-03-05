namespace Common.Types;

public class Solution
{
    public required Position[] Moves { get; init; }
    public int NumberOfMoves => Moves.Length;
}