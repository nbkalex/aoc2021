
var lines = File.ReadAllLines("input.txt");
var chars = new Dictionary<char, char>()
{
    {')', '(' },
    {']', '[' },
    {'}', '{' },
    {'>', '<' },
};

var charsRev = new Dictionary<char, char>()
{
    {'(',')' },
    {'[',']' },
    {'{','}' },
    {'<','>' },
};

var scores = new Dictionary<char, int>()
{
    {')', 3 },
    {']', 57 },
    {'}', 1197 },
    {'>', 25137 },
};

var scores2 = new Dictionary<char, int>()
{
    {')', 1 },
    {']', 2 },
    {'}', 3 },
    {'>', 4 },
};

int total = 0;
List<long> linespoints = new List<long>();
foreach (var line in lines)
{
    Stack<char> vs = new Stack<char>();
    long points = 0;
    bool isValid = true;
    foreach (var c in line)
    {
        if (chars.Values.Contains(c))
            vs.Push(c);
        else if (vs.Pop() != chars[c])
        {
            total += scores[c];
            isValid = false;
            break;
        }
    }

    if (!isValid)
        continue;

    while (vs.Any())
    {
        char c = vs.Pop();
        points = points * 5 + scores2[charsRev[c]];
    }

    Console.WriteLine(points);
    linespoints.Add(points);
}

Console.WriteLine("part 1: " + total);
Console.WriteLine("part 2: " + linespoints.OrderBy(p => p).ElementAt(linespoints.Count() / 2));