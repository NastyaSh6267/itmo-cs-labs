// Размер массива
const int n = 20;
// Исходный массив
var array = new double[n];

// Заполнение массива
Console.WriteLine("Часть 1");
var random = new Random();
for (var i = 0; i < n; i++)
{
    array[i] = random.Next(-100, 100);
    Console.Write(array[i] + " ");
}

// Находим минимальный элемент массива
var min = array.Min();
Console.WriteLine($"Минимальный элемент массива: {min}");

// Находим индексы первого и последнего положительного элемента
var firstPositiveIndex = Array.FindIndex(array, element => element > 0);
var lastPositiveIndex = Array.FindLastIndex(array, element => element > 0);

// Считаем сумму элементов между первым и последним положительными элементами
double sum = 0;
if (firstPositiveIndex != -1 && lastPositiveIndex != -1) // Если есть положительные элементы
{
    for (var i = firstPositiveIndex + 1; i < lastPositiveIndex; i++)
    {
        sum += array[i];
    }
}

Console.WriteLine($"Сумма элементов между первым и последним положительными элементами: {sum}");

// Преобразуем массив
Array.Sort(array, (a, b) => a == 0 ? -1 : b == 0 ? 1 : 0);

// Выводим преобразованный массив
Console.WriteLine("Преобразованный массив:");
foreach (var element in array)
{
    Console.Write(element + " ");
}

// Часть 2
Console.WriteLine("\nЧасть 2");
const int x = 4;
const int y = 6;

var matrix = new int[x, y];

// Заполнение массива
for (var i = 0; i < x; i++)
{
    for (var j = 0; j < y; j++)
    {
        matrix[i, j] = random.Next(-100, 100);
        Console.Write(matrix[i, j] + " ");
    }

    Console.WriteLine();
}

// Сумма элементов в строках, содержащих хотя бы один отрицательный элемент
sum = 0;
for (var i = 0; i < matrix.GetLength(0); i++)
{
    var hasNegative = false;
    for (var j = 0; j < matrix.GetLength(1); j++)
    {
        if (matrix[i, j] < 0)
        {
            hasNegative = true;
            break;
        }
    }

    if (hasNegative)
    {
        for (var j = 0; j < matrix.GetLength(1); j++)
        {
            sum += matrix[i, j];
        }
    }
}

Console.WriteLine($"Сумма элементов в строках с отрицательными элементами: {sum}");

// Находим седловые точки матрицы
for (var i = 0; i < matrix.GetLength(0); i++)
{
    var minInRow = matrix[i, 0];
    var col = 0;
    for (var j = 1; j < matrix.GetLength(1); j++)
    {
        if (matrix[i, j] < minInRow)
        {
            minInRow = matrix[i, j];
            col = j;
        }
    }

    var isSaddlePoint = true;
    for (var k = 0; k < matrix.GetLength(0); k++)
    {
        if (matrix[k, col] > minInRow)
        {
            isSaddlePoint = false;
            break;
        }
    }

    if (isSaddlePoint)
    {
        Console.WriteLine($"Седловая точка в строке {i}, столбце {col}, значение {minInRow}");
    }
}
