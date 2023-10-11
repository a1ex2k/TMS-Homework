namespace MatrixOperations;

public class MatrixManipulator
{
    public readonly Matrix Matrix;

    public MatrixManipulator(Matrix matrix)
    {
        Matrix = matrix;
    }

    public int CountPositive()
    {
        return Matrix.Count(v => v > 0);
    }

    public int CountNegative()
    {
        return Matrix.Count(v => v < 0);
    }

    public void SortRowAscending(int rowIndex)
    {
        var span = Matrix.RowAsSpan(rowIndex);
        BubbleSort(span);
    }

    public void SortRowDescending(int rowIndex)
    {
        var span = Matrix.RowAsSpan(rowIndex);
        BubbleSort(span, false);
    }

    // let say it means mirror row
    public void Inverse(int rowIndex)
    {
        for (int i = 0; i < Matrix.ColumnsCount / 2; i++)
        {
            Matrix[rowIndex, i] = Matrix[rowIndex, Matrix.ColumnsCount - 1 - i]; 
        }
    }


    // from TheMostBoringTaskEver
    private void BubbleSort(Span<double> span, bool ascending = true)
    {
        double temp;
        for (int write = 0; write < span.Length; write++)
        {
            for (int sort = 0; sort < span.Length - 1; sort++)
            {
                if (ascending && span[sort] > span[sort + 1] || !ascending && span[sort] < span[sort + 1])
                {
                    temp = span[sort + 1];
                    span[sort + 1] = span[sort];
                    span[sort] = temp;
                }
            }
        }
    }
    
}