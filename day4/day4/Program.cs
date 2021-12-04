// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

var input = File.ReadAllLines("input.txt");

var numbers = input[0].Split(",").Select(token => int.Parse(token)).ToList();

List<int[,]> sets = new List<int[,]>();

int[,] current = new int[5, 5];
int currentLine = 0;
foreach (var line in input.Skip(1))
{
    if (!line.Any())
    {
        current = new int[5, 5];
        sets.Add(current);
        currentLine = 0;
        continue;
    }

    var lineValues = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(token => int.Parse(token.Trim())).ToList();
    foreach (var index in Enumerable.Range(0, 5))
        current[currentLine, index] = lineValues[index];

    currentLine++;
}

Debug.WriteLine(sets.Count);

HashSet<int> numbersExtracted = new HashSet<int>(numbers.Take(5));
int currentNumberIndex = 4;
while (true)
{
    for (int i = 0; i < sets.Count; i++)
    {
        int[,] result;
        if (IsWinner(sets[i], numbersExtracted, out result))
        {
            sets.RemoveAt(i);
            i--;

            if (!sets.Any())
            {
                Console.WriteLine(result.Cast<int>().Where(nr => nr != -1).Sum() * numbers[currentNumberIndex]);
                return;
            }
        }
    }

    currentNumberIndex++;
    numbersExtracted.Add(numbers[currentNumberIndex]);
}


bool IsWinner(int[,] set, HashSet<int> numbers, out int[,] result)
{
    int[,] copy = set.Clone() as int[,];

    for (int i = 0; i < 5; i++)
        for (int j = 0; j < 5; j++)
            if (numbers.Contains(copy[i, j]))
                copy[i, j] = -1;

    result = copy;

    for (int i = 0; i < 5; i++)
    {
        //lines
        bool found = true;
        for (int j = 0; j < 5; j++)
        {

            if (copy[i, j] != -1)
            {
                found = false;
                break;
            }
        }

        if (found)
            return true;

        found= true;
        // columns
        for (int j = 0; j < 5; j++)
        {
            if (copy[j, i] != -1)
            {
                found = false;
                break;
            }
        }

        if (found)
            return true;

    }

    return false;

}