using Common.Types;

namespace ComputeWorker;

static class MovesPerPiece
{
    public static readonly MoveOffset[] Knight =
    {
        new() {ColumnDelta = 1, RowDelta = 2},
        new() {ColumnDelta = -1, RowDelta = 2},
        new() {ColumnDelta = 1, RowDelta = -2},
        new() {ColumnDelta = -1, RowDelta = -2},
        
        new() {ColumnDelta = 2, RowDelta = 1},
        new() {ColumnDelta = -2, RowDelta = 1},
        new() {ColumnDelta = 2, RowDelta = -1},
        new() {ColumnDelta = -2, RowDelta = -1},
    };
}

public class MoveBuilder
{
    public IEnumerable<Position> GetMoves(Position position, PieceType type)
    {
        if (type != PieceType.Knight)
            throw new ArgumentException($"{type} is not currently supported");

        return MovesPerPiece.Knight.Select(position.GetNewPosition);
    }
}