using ComputeWorker;

namespace Specs.ComputeWorker;

public class SolverSpecs
{
    private Solver _solver = new(new MoveBuilder());

    [TestCase("A1", "C2", "A1:C2")]
    [TestCase("A1", "E3", "A1:C2:E3")]
    [TestCase("A1", "F5", "A1:B3:D4:F5")]
    //[TestCase("A1", "C2", "A1:C2")]
    //[TestCase("A1", "C2", "A1:C2")]
    public void Should_be_able_to_solve(string start, string end, string expectedMoves)
    {
        var solution = _solver.Solve(start, end);
        var expectedLength = expectedMoves.Split(":").Length - 1;
        
        Assert.That(solution.Moves, Is.EqualTo(expectedMoves));
        Assert.That(solution.NumberOfMoves, Is.EqualTo(expectedLength));

    }
}