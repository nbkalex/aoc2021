// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText("input.txt")
                .Split("\r\n");

int currentH = 0;
int currentD = 0;
int currentA = 0;

foreach (var line in input)
{
    int val = int.Parse(line.Split(" ")[1]);
    if (line.StartsWith("f"))
    { 
        currentH += val;
        currentD += val * currentA;
    }
    if (line.StartsWith("u"))
        currentA -= val;
    if (line.StartsWith("d"))
        currentA += val;
}

Console.WriteLine(currentH *  currentD);
