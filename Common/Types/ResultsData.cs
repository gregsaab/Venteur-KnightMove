using Azure;
using Azure.Data.Tables;

namespace Common.Types
{
	public class ResultsData : ITableEntity
	{
		public ResultsData(string rowKey, string shortestPath, int numberOfMoves, string starting, string ending)
		{
			OperationId = RowKey = rowKey;
			ShortestPath = shortestPath;
			NumberOfMoves = numberOfMoves;
			Starting = starting;
			Ending = ending;
		}

		public ResultsData(string rowKey, string error)
		{
            OperationId = RowKey = rowKey;
			Error = error;
		}
		
		public ResultsData(){}

		public string PartitionKey { get; set; } = "partition";
		public string RowKey { get; set; }
		public DateTimeOffset? Timestamp { get; set; } = DateTimeOffset.Now;
		public ETag ETag { get; set; }
		public string OperationId { get; set; }
		public string ShortestPath { get; set; }
		public int NumberOfMoves { get; set; }
		public string Starting { get; set; }
		public string Ending { get; set; }
		public string Error { get; set; }
	}
}

