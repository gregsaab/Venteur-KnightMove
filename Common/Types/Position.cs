using System.Text.RegularExpressions;

namespace Common.Types;

public partial class Position
{
    public int Column { get; init; } = -1;
    public int Row { get; init; } = -1;

    private string StringValue { get; init; }
    

    public override string ToString()
    {
        return StringValue;
    }

    public bool IsValid()
    {
        return Row >= Constants.MinRowIndex && Row <= Constants.MaxRowIndex 
                    && Column >= Constants.MinColumnIndex && Column <= Constants.MaxColumnIndex;
    }

    public Position(string position)
    {
        var regex = PositionStringRegex();
        var match = regex.Match(StringValue = position);

        if (!match.Success)
        {
            return;
        }
        
        var column = match.Groups[1].Value;
        var row = match.Groups[2].Value;

        Column = column[0] - 65;
        Row = int.Parse(row) - 1;

        StringValue = position;
    }

    public Position(int column, int row)
    {
        Column = column;
        Row = row;
        StringValue = $"{(char)(Column + 64)}{Row + 1}";
    }
    public Position GetNewPosition(MoveOffset move)
    {
        return new Position(Column + move.ColumnDelta, Row + move.RowDelta);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj is not Position objAsPosition)
            return false;

        return Equals(objAsPosition);
    }

    private bool Equals(Position other)
    {
        return Column == other.Column && Row == other.Row;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Column, Row, StringValue);
    }


    public IEnumerable<Position> GetValidMoves(PieceType type)
    {
        if (type != PieceType.Knight)
            throw new ArgumentException($"{type} ia not currently supported.");

        
        if (Row + 2 < Constants.MaxRowIndex - 1)
        {
            if (Column > Constants.MinColumnIndex)
            {
                yield return new Position("AA");
            }
        }
    }

    [GeneratedRegex("^([A-H])([1-8])$")]
    private static partial Regex PositionStringRegex();
}
    
