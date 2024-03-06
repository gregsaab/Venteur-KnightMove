using Common.Types;

namespace ComputeWorker.Utils
{
    public interface ISolver
	{
		Solution Solve(string start, string end, PieceType type);
    }

	public class Solver : ISolver
	{
		private readonly IMoveBuilder _moveBuilder;

		public Solver(IMoveBuilder moveBuilder)
		{
			_moveBuilder = moveBuilder;
		}

		/// <summary>
		/// This solver uses a breath-first-search algorithm to find the shortest
		/// path from the start position to the end position. This uses a FIFO queue
		/// to facilitate the BFS. We start off by enqueuing the starting position then
		/// start looping until the queue is empty (or we find the ending position).
		/// While looping, we enqueue all possible valid moves for the current position.
		/// </summary>
		/// <param name="start">Starting position e.g. A1</param>
		/// <param name="end">Ending position e.g. C4</param>
		/// <returns>The solution object with the shortest path and number of moves</returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		public Solution Solve(string start, string end, PieceType type)
		{
			var startPosition = new Position(start);
			var endPosition = new Position(end);

			if (startPosition.IsValid())
				throw new ArgumentException($"{start} is not a valid starting position");

            if (endPosition.IsValid())
                throw new ArgumentException($"{start} is not a valid ending position");

            var stack = new Queue<Node>();
			
			stack.Enqueue(Node.From(startPosition));

			while (stack.Count > 0)
			{
				var currentNode = stack.Dequeue();
				var currentPosition = currentNode.Position;

				if (currentPosition.Equals(endPosition))
					return new Solution { Moves = currentNode.GetAllMoveStrings(), NumberOfMoves = currentNode.Index};

				foreach (var validMove in _moveBuilder.GetValidMoves(currentPosition, type))
				{
					stack.Enqueue(currentNode.Next(validMove));
				}
			}

			throw new Exception("Cannot solve");
		}
	}
}

