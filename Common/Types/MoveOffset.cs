namespace Common.Types;

/// <summary>
/// This represents a col/row move for a piece
/// </summary>
public class MoveOffset
{
    public int RowDelta { get; init; }
    public int ColumnDelta { get; init; }
}