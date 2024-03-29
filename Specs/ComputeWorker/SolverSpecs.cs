using Common.Types;
using ComputeWorker;
using ComputeWorker.Utils;

namespace Specs.ComputeWorker;

public class SolverSpecs
{
    private readonly Solver _solver = new(new MoveBuilder());

    [TestCase("A1", "C2", "A1:C2")]
    [TestCase("A1", "E3", "A1:C2:E3")]
    [TestCase("A1", "F5", "A1:B3:D4:F5")]
    [TestCase("E3", "A1", "E3:C2:A1")]
    [TestCase("F5", "A1", "F5:E3:C2:A1")]
    [TestCase("C2", "A1", "C2:A1")]
    [TestCase("C7", "B5", "C7:B5")]
    [TestCase("H8", "H4", "H8:G6:H4")]
    [TestCase("H8", "G2", "H8:G6:H4:G2")]
    [TestCase("F6", "A8", "F6:E8:C7:A8")]
    public void Should_be_able_to_solve_for_knight(string start, string end, string expectedMoves)
    {
        var solution = _solver.Solve(start, end, PieceType.Knight);
        var expectedLength = expectedMoves.Split(":").Length - 1;
        
        Assert.That(solution.Moves, Is.EqualTo(expectedMoves));
        Assert.That(solution.NumberOfMoves, Is.EqualTo(expectedLength));
    }
    
    [TestCase(PieceType.Bishop)]
    [TestCase(PieceType.King)]
    [TestCase(PieceType.Queen)]
    [TestCase(PieceType.Rook)]
    [TestCase(PieceType.Pawn)]
    public void Should_throw_exception_on_unsupported_type(PieceType type)
    {
        Assert.Throws<ArgumentException>(() => _solver.Solve("A1", "C2", type));
    }
}