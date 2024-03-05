using Common.Types;
using ComputeWorker;

namespace Specs.ComputeWorker;

public class MoveBuilderSpecs
{
    private MoveBuilder _moveBuilder = new();

    [Test]
    public void Should_return_all_moves_for_knight_regardless_of_validity()
    {
        var moves = _moveBuilder.GetMoves(new Position(3, 3), PieceType.Knight).ToList();
        
        Assert.That(moves.Count, Is.EqualTo(8));
        
        // Assert.That(moves, );
        
        // CollectionAssert.Contains(moves, );
        
    }
}