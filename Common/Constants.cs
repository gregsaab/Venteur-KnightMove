using Common.Types;

namespace Common;

public static class Constants
{
    public static short MaxRowIndex => 7;
    public static short MinRowIndex => 0;
    
    public static short MaxColumnIndex => 7;
    public static short MinColumnIndex => 0;
}

public static class MovesPerPiece
{
    public static readonly MoveOffset[] Knight =
    {
        new() {ColumnDelta = 1, RowDelta = 2},
        new() {ColumnDelta = -1, RowDelta = 2},
        new() {ColumnDelta = 1, RowDelta = -2},
        new() {ColumnDelta = -1, RowDelta = -2},

        new() {ColumnDelta = 2, RowDelta = 1},
        new() {ColumnDelta = -2, RowDelta = 1},
        new() {ColumnDelta = 2, RowDelta = -1},
        new() {ColumnDelta = -2, RowDelta = -1},
    };
}