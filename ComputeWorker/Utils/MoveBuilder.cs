using Common.Types;

namespace ComputeWorker.Utils;

internal static class MovesPerPiece
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

public interface IMoveBuilder
{
    IEnumerable<Position> GetValidMoves(Position position, PieceType type);
}

public class MoveBuilder : IMoveBuilder
{
    public IEnumerable<Position> GetValidMoves(Position position, PieceType type)
    {
        if (type != PieceType.Knight)
            throw new ArgumentException($"{type} is not currently supported");

        return MovesPerPiece.Knight.Select(position.GetNewPosition).Where(x => x.IsValid());
    }
}