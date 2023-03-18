using System;

namespace MatrixCalc
{
    /// <summary>
    /// Класс Programm.
    /// </summary>
    class Program
    {

        /// <summary>
        /// Метод чтения.
        /// </summary>
        /// <param name="matrix">Матрица, с которой мы работаем</param>
        static void Input(out double[,] matrix)
        {
            uint lines, columns;
            InputSizes(out lines, out columns);
            matrix = new double[lines, columns];
            InputMatrix(matrix);
        }

        /// <summary>
        /// Чтение параметров матрицы.
        /// </summary>
        /// <param name="lines">Количество строк матрицы</param>
        /// <param name="columns">Количество столбцов матрицы</param>
        static void InputSizes(out uint lines, out uint columns)
        {
            string inputLines, inputColumns;
            Console.Write("Введите количество строк матрицы: ");
            inputLines = Console.ReadLine();
            while (!uint.TryParse(inputLines, out lines))
            {
                Console.Write("Неправильный ввод, попробуйте снова: ");
                inputLines = Console.ReadLine();
            }
            Console.Write("Введите количество столбцов матрицы: ");
            inputColumns = Console.ReadLine();
            while (!uint.TryParse(inputColumns, out columns))
            {
                Console.Write("Неправильный ввод, попробуйте снова: ");
                inputColumns = Console.ReadLine();
            }
        }

        /// <summary>
        /// Проверка корректности введенных пользователем строк матрицы.
        /// </summary>
        /// <param name="line">Введенная строка</param>
        /// <param name="length">Нужная длина строки</param>
        /// <returns></returns>
        static bool Check(string[] line, int length)
        {
            if (line.Length != length)
            {
                return false;
            }
            for (int i = 0; i < length; ++i)
            {
                double val;
                if (!double.TryParse(line[i], out val))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Чтение матрицы.
        /// </summary>
        /// <param name="matrix">Матрица, которую необходимо ввести</param>
        static void InputMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                Console.WriteLine("Введите значения " + (i + 1) + "-ой строки матрицы через пробел");
                string[] line = Console.ReadLine().Split();
                while (!Check(line, matrix.GetLength(1)))
                {
                    Console.WriteLine("Некорректный ввод, введите строку снова: ");
                    line = Console.ReadLine().Split();
                }
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    matrix[i, j] = double.Parse(line[j]);
                }
            }
        }

        /// <summary>
        /// Вывод матрицы в консоль. 
        /// </summary>
        /// <param name="matrix">Матрица, которую нужно вывести</param>
        static void OutputMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    Console.Write("{0,7}", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Вывод возможностей калькулятора.
        /// </summary>
        static void ShowOpportunities()
        {
            Console.WriteLine("Введите операцию, которую хотите совершить с матрицей:");
            Console.WriteLine("\"tr\" - Найти след матрицы (только для квадратной матрицы)");
            Console.WriteLine("\"T\" - провести транспонирование матрицы");
            Console.WriteLine("\"+\" - сложить с другой матрицей");
            Console.WriteLine("\"-\" - вычесть другую матрицу");
            Console.WriteLine("\"*\" - умножить на другую матрицу");
            Console.WriteLine("\"*v\" - умножить на число");
            Console.WriteLine("\"det\" - Найти определитель матрицы (только для квадратной матрицы)");
            Console.WriteLine("\"Gauss\" - данная реализация поддерживает решения систем уравнений, гарантированно имеющих одно решение. Матрица системы имеет вид n * (n+1) и не содержит нулей на главной диагонали. Иначе могут выводиться некорректные ответы");
        }

        /// <summary>
        /// След матрицы.
        /// </summary>
        /// <param name="matrix">Матрица, для нахождения следа</param>
        static void Trace(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                Console.WriteLine("Матрица не квадратная, невозможно найти след");
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Первая матрица:");
                OutputMatrix(matrix);
                Console.WriteLine();
                double tr = 0;
                for (int i = 0; i < matrix.GetLength(0); ++i)
                {
                    tr += matrix[i, i];
                }
                Console.WriteLine("След матрицы равен " + tr);
            }
        }

        /// <summary>
        /// Транспонирование матрицы.
        /// </summary>
        /// <param name="matrix">Матрица для транспонирования</param>
        static void Transposition(double[,] matrix)
        {
            Console.Clear();
            Console.WriteLine("Первая матрица:");
            OutputMatrix(matrix);
            Console.WriteLine();
            double[,] tMatrix = new double[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    tMatrix[j, i] = matrix[i, j];
                }
            }
            Console.WriteLine("Транспонированная матрица:");
            OutputMatrix(tMatrix);
        }

        /// <summary>
        /// Сумма матриц.
        /// </summary>
        /// <param name="matrix1">Первое слагаемое суммы</param>
        static void MatrixSum(double[,] matrix1)
        { 
            Console.Clear();
            uint lines, columns;
            InputSizes(out lines, out columns);
            if (lines != matrix1.GetLength(0) || columns != matrix1.GetLength(1))
            {
                Console.WriteLine("Размеры второй матрицы должны быть такими же, как и у первой матрицы");
                return;
            }
            double[,] matrix2 = new double[lines, columns];
            InputMatrix(matrix2);
            Console.Clear();
            Console.WriteLine("Первая матрица:");
            OutputMatrix(matrix1);
            Console.WriteLine("Вторая матрица:");
            OutputMatrix(matrix2);
            double[,] matrixSum = new double[lines, columns];
            for (int i = 0; i < lines; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    matrixSum[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            Console.WriteLine("Сумма матриц:");
            OutputMatrix(matrixSum);
        }

        /// <summary>
        /// Разность матриц.
        /// </summary>
        /// <param name="matrix1">Уменьшаемое разности</param>
        static void MatrixDif(double[,] matrix1)
        {
            Console.Clear();
            uint lines, columns;
            InputSizes(out lines, out columns);
            if (lines != matrix1.GetLength(0) || columns != matrix1.GetLength(1))
            {
                Console.WriteLine("Размеры второй матрицы должны быть такими же, как и у первой матрицы");
                return;
            }
            double[,] matrix2 = new double[lines, columns];
            InputMatrix(matrix2);
            Console.Clear();
            Console.WriteLine("Первая матрица:");
            OutputMatrix(matrix1);
            Console.WriteLine("Вторая матрица:");
            OutputMatrix(matrix2);
            double[,] matrixDif = new double[lines, columns];
            for (int i = 0; i < lines; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    matrixDif[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            Console.WriteLine("Разность матриц:");
            OutputMatrix(matrixDif);
        }

        /// <summary>
        /// Произведение матриц.
        /// </summary>
        /// <param name="matrix1">Первый множетель умножения</param>
        static void MatrixMult(double[,] matrix1)
        {
            Console.Clear();
            uint lines, columns;
            InputSizes(out lines, out columns);
            if (lines != matrix1.GetLength(1))
            {
                Console.WriteLine("Количество столбцов первой матрицы должно совпадать с количеством строк второй матрицы");
                return;
            }
            double[,] matrix2 = new double[lines, columns];
            InputMatrix(matrix2);
            Console.Clear();
            Console.WriteLine("Первая матрица:");
            OutputMatrix(matrix1);
            Console.WriteLine("Вторая матрица:");
            OutputMatrix(matrix2);
            double[,] matrixMult = new double[matrix1.GetLength(0), columns];
            for (int i = 0; i < matrix1.GetLength(0); ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    double sum = 0;
                    for (int k = 0; k < matrix1.GetLength(1); ++k)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    matrixMult[i, j] = sum;
                }
            }
            Console.WriteLine("Произведение матриц");
            OutputMatrix(matrixMult);
        }

        /// <summary>
        /// Умножение матрицы на число.
        /// </summary>
        /// <param name="matrix">Матрица для умножения</param>
        static void MatrixMultValue(double[,] matrix)
        {
            Console.Clear();
            Console.WriteLine("Первая матрица:");
            OutputMatrix(matrix);
            double[,] matrixRes = new double[matrix.GetLength(0), matrix.GetLength(1)];
            double value;
            Console.WriteLine("Введите число, на которое нужно умножить матрицу");
            string inputValue = Console.ReadLine();
            if (!double.TryParse(inputValue, out value))
            {
                Console.WriteLine("Некорректное значение");
                return;
            }
            Console.WriteLine("Первая матрица:");
            Console.WriteLine("Множитель равен " + value);
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    matrixRes[i, j] = matrix[i, j] * value;
                }
            }
            Console.WriteLine("Конечная матрица:");
            OutputMatrix(matrixRes);
        }

        /// <summary>
        /// Определитель матрицы.
        /// </summary>
        /// <param name="matrix">Матрица для нахождения определителя</param>
        /// <returns></returns>
        static double Determinant(double[,] matrix)
        {
            if(matrix.GetLength(0) == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            else
            {
                double[,] matrixHelp = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
                double determinant = 0;
                for(int k = 0; k < matrix.GetLength(0); ++k)
                {
                    for(int i = 1; i < matrix.GetLength(0); ++i)
                    {
                        for(int j = 0; j < matrix.GetLength(1); ++j)
                        {
                            if(j > k)
                            {
                                matrixHelp[i - 1, j - 1] = matrix[i, j];
                            }
                            if(j < k)
                            {
                                matrixHelp[i - 1, j] = matrix[i, j];
                            }
                        }
                    }
                    if(k % 2 == 0)
                    {
                        determinant += matrix[0, k] * Determinant(matrixHelp);
                    }
                    else
                    {
                        determinant -= matrix[0, k] * Determinant(matrixHelp);
                    }
                }
                return determinant;
            }
        }

        /// <summary>
        /// Метод для нахождения определителя.
        /// </summary>
        /// <param name="matrix">Матрица для нахождения определителя</param>
        static void FindDeterminant(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                Console.WriteLine("Матрица должна быть квадратной");
                return;
            }
            Console.WriteLine("Определитель матрицы равен " + Determinant(matrix));
        }

        /// <summary>
        /// Ступенчатый вид матрицы.
        /// </summary>
        /// <param name="matrix">Матрица, введенная пользователем</param>
        /// <param name="matrixHelp">Вспомогательная матрица</param>
        static void SteppedView(double [,] matrix, double[,] matrixHelp)
        {
            for (int i = 0; i < matrixHelp.GetLength(0); i++)
                for (int j = 0; j < matrixHelp.GetLength(1); j++)
                    matrixHelp[i, j] = matrix[i, j];
            for (int k = 0; k < matrixHelp.GetLength(0); k++)
            {
                for (int i = 0; i < matrixHelp.GetLength(1); i++)
                    matrixHelp[k, i] = matrixHelp[k, i] / matrix[k, k];
                for (int i = k + 1; i < matrixHelp.GetLength(0); i++)
                {
                    double K = matrixHelp[i, k] / matrixHelp[k, k];
                    for (int j = 0; j < matrixHelp.GetLength(1); j++)
                        matrixHelp[i, j] = matrixHelp[i, j] - matrixHelp[k, j] * K;
                }
                for (int i = 0; i < matrixHelp.GetLength(0); i++)
                    for (int j = 0; j < matrixHelp.GetLength(1); j++)
                        matrix[i, j] = matrixHelp[i, j];
            }
        }

        /// <summary>
        /// Канонический вид матрицы.
        /// </summary>
        /// <param name="matrixHelp">Вспомогательная матрица</param>
        static void CanonicialView(double[,] matrixHelp)
        {
            for (int k = matrixHelp.GetLength(0) - 1; k > -1; k--)
            {
                for (int i = matrixHelp.GetLength(1) - 1; i > -1; i--)
                    matrixHelp[k, i] = matrixHelp[k, i] / matrixHelp[k, k];
                for (int i = k - 1; i > -1; i--)
                {
                    double K = matrixHelp[i, k] / matrixHelp[k, k];
                    for (int j = matrixHelp.GetLength(0); j > -1; j--)
                        matrixHelp[i, j] = matrixHelp[i, j] - matrixHelp[k, j] * K;
                }
            }
        }

        /// <summary>
        /// Поиск решений в матрице.
        /// </summary>
        /// <param name="matrixHelp">Вспомогательная матрица</param>
        /// <returns></returns>
        static double[] FindSolutions(double[,] matrixHelp)
        {
            double[] result = new double[matrixHelp.GetLength(0)];
            for (int i = 0; i < matrixHelp.GetLength(0); i++)
                result[i] = matrixHelp[i, matrixHelp.GetLength(0)];
            return result;
        }

        /// <summary>
        /// Метод, для нахождения решений системы.
        /// </summary>
        /// <param name="matrix">Матрица, введенная пользователем</param>
        /// <returns></returns>
        public static double[] Gauss(double[,] matrix)
        {
            double[,] matrixHelp = new double[matrix.GetLength(0), matrix.GetLength(1)];
            SteppedView(matrix, matrixHelp);
            CanonicialView(matrixHelp);
            return FindSolutions(matrixHelp);
        }

        /// <summary>
        /// Метод Гаусса.
        /// </summary>
        /// <param name="matrix">Матрица, введенная пользователем</param>
        static void GaussMethod(double[,] matrix)
        {
            if(matrix.GetLength(0) != matrix.GetLength(1) - 1) {
                Console.WriteLine("Неподходящий размер матрицы");
                return;
            }
            double[] result = Gauss(matrix);
            for(int i = 0; i < result.Length; ++i)
            {
                Console.WriteLine("x" + (i + 1) + " = " + "{0:f4}", result[i]);
            }
        }

        /// <summary>
        /// Работа калькулятора.
        /// </summary>
        /// <param name="matrix">Матрица, введенная пользователем</param>
        static void Working(double[,] matrix)
        {
            string operation = Console.ReadLine();
            if (operation == "tr")
            {
                Trace(matrix);
            }
            else if (operation == "T")
            {
                Transposition(matrix);
            }
            else if (operation == "+")
            {
                MatrixSum(matrix);
            }
            else if (operation == "-")
            {
                MatrixDif(matrix);
            }
            else if (operation == "*")
            {
                MatrixMult(matrix);
            }
            else if (operation == "*v")
            {
                MatrixMultValue(matrix);
            }
            else if (operation == "det")
            {
                FindDeterminant(matrix);
            }
            else if(operation == "Gauss"){
                GaussMethod(matrix);
            }
            else
            {
                Console.WriteLine("Неверный ввод операции");
            }
        }

        /// <summary>
        /// Точка входа.
        /// </summary>
        static void Main()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Калькулятор матриц");
                Input(out double[,] matrix);
                do
                {
                    Console.Clear();
                    Console.WriteLine("Первая матрица:");
                    OutputMatrix(matrix);
                    ShowOpportunities();
                    Working(matrix);
                    Console.WriteLine("Хотите ли вы продолжить работу с введенной матрицей");
                    Console.WriteLine("Нажмите Enter, если да, нажмите любую другую клавишу, если нет: ");
                }
                while (Console.ReadKey().Key == ConsoleKey.Enter);
                Console.Clear();
                Console.WriteLine("Если хотите выйти, нажмите Escape, иначе - любую другую клавишу");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
