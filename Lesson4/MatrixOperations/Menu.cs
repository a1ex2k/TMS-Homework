namespace MatrixOperations;

public class Menu
{
    private Matrix CreateMatrix()
    {
        var rowsCount = InputHelper.ReadInt32(n => n > 0 && n < 10, "Введите количество строк матрицы от 0 по 10");
        var columnsСount = InputHelper.ReadInt32(n => n > 0 && n < 10, "Введите количество столбцов матрицы от 0 по 10");
        var matrix = new Matrix(rowsCount, columnsСount);
        return matrix;
    }


    private void FillMatrix(Matrix matrix)
    {
        for (int rowIndex = 0; rowIndex < matrix.RowsCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < matrix.ColumnsCount; columnIndex++)
            {
                Console.Clear();
                MatrixPrinter.Print(matrix);
                Console.WriteLine("\r\n");
                matrix[rowIndex, columnIndex] = InputHelper.ReadMatrixElementOrRandom(rowIndex, columnIndex);
            }
        }
    }


    public void Run()
    {
        var matrix = CreateMatrix();
        FillMatrix(matrix);
        var manipulator = new MatrixManipulator(matrix);
        Console.Clear();
        MatrixPrinter.Print(matrix);

        var menu = "\r\nВведите номер операции:" +
                              "\r\n\t1. Найти количество положительных чисел в матрице" +
                              "\r\n\t2. Найти количество отрицательных чисел в матрице" +
                              "\r\n\t3. Сортировка элементов матрицы построчно (по возрастанию)" +
                              "\r\n\t4. Сортировка элементов матрицы построчно (по убыванию)" +
                              "\r\n\t5. Инверсия элементов матрицы построчно";

        var rowsPredicate = new Predicate<int>((value) => value > 0 && value <= matrix.RowsCount);
        Console.WriteLine(menu);
        
        while (true)
        {
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine($"Положительных: {manipulator.CountPositive()}");
                    continue;
                case "2":
                    Console.WriteLine($"Отрицательных: {manipulator.CountPositive()}");
                    continue;
                case "3":
                {
                    var row = InputHelper.ReadInt32(rowsPredicate, "Строка для сортировки по возрастанию");
                    manipulator.SortRowAscending(row - 1);
                }
                    break;
                case "4":
                {
                    var row = InputHelper.ReadInt32(rowsPredicate, "Строка для сортировки по убыванию");
                    manipulator.SortRowDescending(row - 1);
                }
                    break;
                case "5":
                {
                    var row = InputHelper.ReadInt32(rowsPredicate, "Строка для инверсии");
                    manipulator.Inverse(row - 1);
                }
                    continue;
                default:
                    continue;
            }

            Console.Clear();
            MatrixPrinter.Print(matrix);
            Console.WriteLine(menu);
        }

    }
}