namespace MatrixOperations;

public class Matrix
{
    // Should I tell why not [,]?
    private double[][] _innerArray;

    public readonly int RowsCount;
    public readonly int ColumnsCount;

    public int Size => RowsCount * ColumnsCount;
    
    public Matrix(int rows, int columns)
    {
        RowsCount = rows;
        ColumnsCount = columns;
        _innerArray = new double[rows][];
        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            _innerArray[rowIndex] = new double[columns];
        }
    }

   
    public double this[int row, int column]
    {
        get => _innerArray[row][column];
        set => _innerArray[row][column] = value;
    }
    
    public Span<double> RowAsSpan(int rowIndex)
    {
        return _innerArray[rowIndex].AsSpan();
    }

    public int Count(Predicate<double> predicate)
    {
        var count = 0;
        for (int rowIndex = 0; rowIndex < RowsCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < ColumnsCount; columnIndex++)
            {
                if (predicate(_innerArray[rowIndex][columnIndex]))
                {
                    count++;
                }
            }
        }
        return count;
    }
}