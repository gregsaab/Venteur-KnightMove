using Common.Types;

namespace ComputeWorker.Utils
{
    public interface ISolver
	{
		Solution Solve(string start, string end);
    }

	public class Solver : ISolver
	{
		private readonly IMoveBuilder _moveBuilder;

		public Solver(IMoveBuilder moveBuilder)
		{
			_moveBuilder = moveBuilder;
		}
		public Solution Solve(string start, string end)
		{
			var startPosition = new Position(start);
			var endPosition = new Position(end);

			var stack = new Queue<Node>();
			
			stack.Enqueue(Node.From(startPosition));

			while (stack.Count > 0)
			{
				var currentNode = stack.Dequeue();
				var currentPosition = currentNode.Position;

				if (currentPosition.Equals(endPosition))
					return new Solution { Moves = currentNode.GetAllMoveStrings(), NumberOfMoves = currentNode.Index};

				foreach (var validMove in _moveBuilder.GetValidMoves(currentPosition, PieceType.Knight))
				{
					stack.Enqueue(currentNode.Next(validMove));
				}
			}

			throw new Exception("Cannot solve");
		}
	}
}

