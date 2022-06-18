int Input(string msg)
{
    bool firstIterCompleted = false;
    int ret;
    do
    {
        if (firstIterCompleted)
        {
            Console.WriteLine("Convertion error");
        }
        Console.WriteLine(msg);
        firstIterCompleted = true;
    }
    while(!int.TryParse(Console.ReadLine(), out ret));
    return ret;
}

int PrintMessageAndChooseValue(string menu, int minValue, int maxValue)
{
    int ret;
    do
    {
        Console.WriteLine(menu);
        ret = Input($"Input value from {minValue} to {maxValue}");
    }
    while (ret < minValue || ret > maxValue);
    return ret;
}

void PrintMatrix(int[][] matrix)
{
    if (matrix == null)
    {
        throw new ArgumentNullException(nameof(matrix));
    }

    if (matrix.Length == 0)
    {
        throw new ArgumentException("Matrix length is 0", nameof(matrix));
    }

    string max = matrix.Select(r => r.Max()).Max().ToString();
    string min = matrix.Select(r => r.Min()).Min().ToString();
    int padLen = max.Length > min.Length ? max.Length : min.Length;
    foreach (var row in matrix)
    {
        Console.WriteLine(string.Join(" ", row.Select(el => el.ToString().PadLeft(padLen))));
    }
}

int[][] GenerateMatrix(int columns, int rows, int minValue, int maxValue)
{
    if (columns <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(columns));
    }

    if (rows <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(rows));
    }

    Random random = new Random();
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

void GetAmountOfPositiveAndNegativeElements(int[][]matrix, out int negatives, out int positives)
{
    positives = 0;
    negatives = 0;
    foreach(var row in matrix)
    {
        foreach(var value in row)
        {
            if (value < 0)
            {
                negatives++; 
            }
            else
            {
                positives++;
            }
        }
    }
}

void BubleSortDesc(int[] arr)
{
    for (int j = 0; j <= arr.Length - 2; j++)
    {
        for (int i = 0; i <= arr.Length - 2; i++)
        {
            if (arr[i] < arr[i + 1])
            {
                (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
            }
        }
    }
}

void BubleSort(int[] arr)
{
    for (int j = 0; j <= arr.Length - 2; j++)
    {
        for (int i = 0; i <= arr.Length - 2; i++)
        {
            if (arr[i] > arr[i + 1])
            {
                (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
            }
        }
    }
}

void Reverse(int[] arr)
{
    for (int i = 0; i < arr.Length / 2; i++)
    {
        (arr[i], arr[^(i + 1)]) = (arr[^(i + 1)], arr[i]);
    }  
}

int columnAmm = Input("Input columns amount"), rowsAmm = Input("Input rows amount"), minValue = Input("Input minimal value"), maxValue = Input("Input maximal value");
//int columnAmm = 10, rowsAmm = 10, minValue = -1, maxValue = 5;
int[][] matrix = GenerateMatrix(columnAmm, rowsAmm, minValue, maxValue);
string menu = "0.Print matrix\n1.Get amount of positive and negative numbers\n2.Sort selected row\n3.Sort selected row (desc)\n4.Reverse selected row\n5.Quit";
PrintMatrix(matrix);
int option;
while (true)
{
    option = PrintMessageAndChooseValue(menu, 0, 5);
    if (option == 0)
    {
        PrintMatrix(matrix);
    }
    else if (option == 1)
    {
        GetAmountOfPositiveAndNegativeElements(matrix, out int negatives, out int positives);
        Console.WriteLine($"Amount of postives - {positives}; negatives - {negatives}");
    }
    else if (option == 2)
    {
        int rowNumber = PrintMessageAndChooseValue("Input row's number", 0, matrix.Length - 1);
        int[] row = matrix[rowNumber];
        Console.WriteLine("Selected row: " + String.Join(" ", row));
        BubleSort(row);
        Console.WriteLine("Sorted row: " + String.Join(" ", row));
    }
    else if (option == 3)
    {
        int rowNumber = PrintMessageAndChooseValue("Input row's number", 0, matrix.Length - 1);
        int[] row = matrix[rowNumber];
        Console.WriteLine("Selected row: " + String.Join(" ", row));
        BubleSortDesc(row);
        Console.WriteLine("Sorted row: " + String.Join(" ", row));
    }
    else if (option == 4)
    {
        int rowNumber = PrintMessageAndChooseValue("Input row's number", 0, matrix.Length - 1);
        int[] row = matrix[rowNumber];
        Console.WriteLine("Selected row: " + String.Join(" ", row));
        Reverse(row);
        Console.WriteLine("Reversed row: " + String.Join(" ", row));
    }
    else
    {
        break;
    }
}