namespace MatrixOperations;

public static class MatrixPrinter
{
    public static void Print(Matrix matrix)
    {
        Console.Write($"{matrix.RowsCount}x{matrix.ColumnsCount}: ");
        
        if (matrix.RowsCount == 1)
        {
            PrintSingleRow(matrix, 0);
            return;
        }

        var initialPosition = Console.GetCursorPosition();
        PrintBorder(initialPosition.Left, initialPosition.Top, matrix.RowsCount);

        var leftOffset = Console.GetCursorPosition().Left;
        for (int columnIndex = 0; columnIndex < matrix.ColumnsCount; columnIndex++)
        {
            var width = PrintColumn(leftOffset, initialPosition.Top, matrix, columnIndex);
            leftOffset += width;
        }

        PrintBorder(leftOffset, initialPosition.Top, matrix.RowsCount);
        Console.WriteLine();
    }


    private static void PrintBorder(int left, int top, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.SetCursorPosition(left, top + i);
            Console.Write(" | ");
        }
    }

    private static int PrintColumn(int left, int top, Matrix matrix, int columnIndex)
    {
        var width = 0;
        for (int rowIndex = 0; rowIndex < matrix.RowsCount; rowIndex++)
        {
            Console.SetCursorPosition(left, top + rowIndex);
            Console.Write($" {matrix[rowIndex, columnIndex]} ");

            var cellWidth = Console.GetCursorPosition().Left - left;
            if (width < cellWidth)
            {
                width = cellWidth;
            }
        }
        return width;
    }


    private static void PrintSingleRow(Matrix matrix, int rowIndex)
    {
        Console.Write(" | ");
        for (int columnIndex = 0; columnIndex < matrix.ColumnsCount; columnIndex++)
        {
            Console.Write($" {matrix[rowIndex, columnIndex]} ");
            Console.Write(" |");
        }

    }
}