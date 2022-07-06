namespace testRepo
{
    public class Programm
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            (b, a) = (a, b);
        }

        public static int SelectItemIndexFromArray<T>(in string msg, T[] arr, bool doUseCancel = true, in string afterArrPrintMsg = "")
        {
            if (arr == null || arr.Length == 0)
            {
                throw new ArgumentNullException(nameof(arr), "Input array in null or empty");
            }

            if (!string.IsNullOrWhiteSpace(msg))
            {
                Console.WriteLine(CONSOLE_SEPARATOR + msg + "\n" + CONSOLE_SEPARATOR);
            }
            else
            {
                Console.WriteLine(CONSOLE_SEPARATOR);
            }

            if (doUseCancel)
            {
                Console.WriteLine("-1. Cancel");
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"{i}. {arr[i]}");
            }

            Console.WriteLine(CONSOLE_SEPARATOR);

            return PrintMessageAndGetValueInRange(afterArrPrintMsg, doUseCancel ? -1 : 0, arr.Length - 1);
        }

        public static int ReadIntFromConsole(in string msg)
        {
            bool isFirstIterCompleted = false;
            int ret;
            do
            {
                if (isFirstIterCompleted)
                {
                    Console.WriteLine("Convertion error");
                }

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    Console.WriteLine(msg);
                }

                isFirstIterCompleted = true;
            }
            while (!int.TryParse(Console.ReadLine(), out ret));
            return ret;
        }

        public static int PrintMessageAndGetValueInRange(in string menu, int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                (maxValue, minValue) = (minValue, maxValue);
            }

            int ret;
            do
            {
                if (!string.IsNullOrWhiteSpace(menu))
                {
                    Console.WriteLine(CONSOLE_SEPARATOR + menu + CONSOLE_SEPARATOR);
                }

                ret = ReadIntFromConsole($"Input value from {minValue} to {maxValue}");
            }
            while (ret < minValue || ret > maxValue);
            return ret;
        }

        public static void PrintMatrix(in int[][] matrix)
        {
            if (matrix == null || matrix.Length == 0)
            {
                throw new ArgumentNullException(nameof(matrix), "Array is null or empty");
            }

            int maxLen = matrix.Select(r => r.Max()).Max().ToString().Length;
            int minLen = matrix.Select(r => r.Min()).Min().ToString().Length;
            int padLen = maxLen > minLen ? maxLen : minLen;
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row.Select(el => el.ToString().PadLeft(padLen))));
            }
        }

        public static int[][] GenerateMatrix(in int columns, in int rows, int minValue, int maxValue)
        {
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            if (maxValue < minValue)
            {
                (maxValue, minValue) = (minValue, maxValue);
            }

            Random random = new();
            int[][] matrix = new int[rows][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new int[columns];
                for (int j = 0; j < columns; j++)
                {
                    matrix[i][j] = random.Next(minValue, maxValue);
                }
            }

            return matrix;
        }

        public static void GetAmountOfPositiveAndNegativeElements(in int[][] matrix, out int negativesCount, out int positivesCount, out int zerosCount)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            positivesCount = 0;
            negativesCount = 0;
            zerosCount = 0;
            foreach (var row in matrix)
            {
                foreach (var value in row)
                {
                    if (value < 0)
                    {
                        negativesCount++;
                    }
                    else if (value > 0)
                    {
                        positivesCount++;
                    }
                    else
                    {
                        zerosCount++;
                    }
                }
            }
        }

        public static void BubleSort(in int[] arr, bool isDescending = false)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr));
            }

            for (int j = 0; j < arr.Length - 1; j++)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if ((isDescending && arr[i] < arr[i + 1]) || (!isDescending && arr[i] > arr[i + 1]))
                    {
                        (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
                    }
                }
            }
        }

        public static void Reverse(in int[] arr)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr));
            }

            for (int i = 0; i < arr.Length / 2; i++)
            {
                Swap(ref arr[i], ref arr[^(i + 1)]);
            }
        }

        private static readonly string CONSOLE_SEPARATOR = new string('-', 30) + '\n';
        public static void Main()
        {
            int columnAmm = ReadIntFromConsole("Input columns amount");
            int rowsAmm = ReadIntFromConsole("Input rows amount");
            int minValue = ReadIntFromConsole("Input minimal value");
            int maxValue = ReadIntFromConsole("Input maximal value");
            //int columnAmm = 10, rowsAmm = 10, minValue = -1, maxValue = 5;
            int[][] matrix = GenerateMatrix(columnAmm, rowsAmm, minValue, maxValue);
            string menu = "0.Print matrix\n1.Get amount of positive and negative numbers\n2.Sort selected row\n3.Sort selected row (desc)\n4.Reverse selected row\n5.Quit";
            PrintMatrix(matrix);
            int option;
            int rowNumber;
            int[] row;
            while (true)
            {
                try
                {
                    option = PrintMessageAndGetValueInRange(menu, 0, 5);
                    switch (option)
                    {
                        case 0:
                            PrintMatrix(matrix);
                            break;
                        case 1:
                            GetAmountOfPositiveAndNegativeElements(matrix, out int negatives, out int positives, out int zeros);
                            Console.WriteLine($"Amount of postives - {positives}; negatives - {negatives}; zeros - {zeros}");
                            break;
                        case 2:
                            rowNumber = PrintMessageAndGetValueInRange("Input row's number", 0, matrix.Length - 1);
                            row = matrix[rowNumber];
                            Console.WriteLine("Selected row: " + String.Join(" ", row));
                            BubleSort(row);
                            Console.WriteLine("Sorted row: " + String.Join(" ", row));
                            break;
                        case 3:
                            rowNumber = PrintMessageAndGetValueInRange("Input row's number", 0, matrix.Length - 1);
                            row = matrix[rowNumber];
                            Console.WriteLine("Selected row: " + String.Join(" ", row));
                            BubleSort(row, true);
                            Console.WriteLine("Sorted row: " + String.Join(" ", row));
                            break;
                        case 4:
                            rowNumber = PrintMessageAndGetValueInRange("Input row's number", 0, matrix.Length - 1);
                            row = matrix[rowNumber];
                            Console.WriteLine("Selected row: " + String.Join(" ", row));
                            Reverse(row);
                            Console.WriteLine("Reversed row: " + String.Join(" ", row));
                            break;

                        default:
                            Console.WriteLine("Bye...");
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}