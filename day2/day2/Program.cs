// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText("input.txt")
                .Split("\r\n");

int currentH = 0;
int currentD = 0;
int currentA = 0;

foreach (var line in input)
{
    var directionTokens = line.Split(" ");
    string direction = directionTokens[0];
    var val = int.Parse(directionTokens[1]);

    switch(direction)
    {
        case "forward":
            currentH += val;
            currentD += val * currentA;
            break;
        case "up":
            currentA -= val;
            break;
        case "down":
            currentA += val;
            break;
    }
}

Console.WriteLine(currentH *  currentD);
