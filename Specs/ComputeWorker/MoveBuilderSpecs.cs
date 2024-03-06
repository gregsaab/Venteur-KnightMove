using Common.Types;
using ComputeWorker;
using ComputeWorker.Utils;

namespace Specs.ComputeWorker;

public class MoveBuilderSpecs
{
    private readonly MoveBuilder _moveBuilder = new();

    [Test]
    public void Should_return_all_valid_moves_for_knight()
    {
        var moves = _moveBuilder.GetValidMoves(new Position(3, 3), PieceType.Knight).ToList();
        
        Assert.That(moves.Count, Is.EqualTo(8));
        
        CollectionAssert.Contains(moves, new Position(5, 2));
        CollectionAssert.Contains(moves, new Position(5, 4));
        
        CollectionAssert.Contains(moves, new Position(1, 2));
        CollectionAssert.Contains(moves, new Position(1, 4));
        
        CollectionAssert.Contains(moves, new Position(4, 1));
        CollectionAssert.Contains(moves, new Position(4, 5));
        
        CollectionAssert.Contains(moves, new Position(2, 1));
        CollectionAssert.Contains(moves, new Position(2, 5));
    }
    
    [TestCase(PieceType.Bishop)]
    [TestCase(PieceType.King)]
    [TestCase(PieceType.Queen)]
    [TestCase(PieceType.Rook)]
    [TestCase(PieceType.Pawn)]
    public void Should_throw_exception_on_unsupported_type(PieceType type)
    {
        Assert.Throws<ArgumentException>(() => _moveBuilder.GetValidMoves(new Position(3, 3), type));
    }
}