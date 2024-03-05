namespace Common.Types;

public class MoveOffset
{
    public int RowDelta { get; init; }
    public int ColumnDelta { get; init;  }

    public static explicit operator MoveOffset(int[] offsets)
    {
        if (offsets.Length != 2)
            throw new ArgumentException($"{nameof(offsets)} needs to have exactly two entries");
        return new MoveOffset { ColumnDelta = offsets[0], RowDelta = offsets[1] };
    }
}