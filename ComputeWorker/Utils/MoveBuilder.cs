using Common;
using Common.Types;

namespace ComputeWorker;

public interface IMoveBuilder
{
    IEnumerable<Position> GetValidMoves(Position position, PieceType type);
}

/// <summary>
/// This iterates over the MovesPerPiece static class for the specific piece type
/// (only support knight at present), creates a Position object, then filters out
/// the invalid positions
/// </summary>
public class MoveBuilder : IMoveBuilder
{
    public IEnumerable<Position> GetValidMoves(Position position, PieceType type)
    {
        if (type != PieceType.Knight)
            throw new ArgumentException($"{type} is not currently supported");

        return MovesPerPiece.Knight.Select(position.GetNewPosition).Where(x => x.IsValid());
    }
}