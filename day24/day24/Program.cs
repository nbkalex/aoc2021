using System.Numerics;

var program = File.ReadAllLines("input.txt").Select(l =>
{
  string[] tokens = l.Split(" ");
  string secondArg = tokens.Length == 3 ? tokens[2] : "";
  return (tokens[0], tokens[1], secondArg);
});

BigInteger maxNumber = 36969794979199;
BigInteger minNumber = 11419161313147;
ProcessNumber(minNumber);

(BigInteger, BigInteger, BigInteger, BigInteger) ProcessNumber(BigInteger number)
{
  Queue<int> numberString = new Queue<int>(number.ToString().Select(c => int.Parse(c.ToString())));
  return ProcessInput(numberString);
}

(BigInteger, BigInteger, BigInteger, BigInteger) ProcessInput(Queue<int> input)
{
  Dictionary<string, BigInteger> variables = new Dictionary<string, BigInteger>()
  {
    { "w", 0 },
    { "x", 0 },
    { "y", 0 },
    { "z", 0 },
  };

  var instructions = new Dictionary<string, Func<BigInteger, BigInteger, BigInteger>>()
  {
    {"inp", (a,b) => input.Dequeue()},
    {"add", (a,b) => a+b},
    {"mul", (a,b) => a*b},
    {"div", (a,b) => a/b},
    {"mod", (a,b) => a%b},
    {"eql", (a,b) => a==b ? 1 : 0}
  };

  int i = 1;
  foreach (var instruction in program)
  {
    BigInteger valueArg2 = 0;
    if (instruction.Item3 != "" && !BigInteger.TryParse(instruction.Item3, out valueArg2))
      valueArg2 = variables[instruction.Item3];
    variables[instruction.Item2] = instructions[instruction.Item1](variables[instruction.Item2], valueArg2);

    if (instruction.Item1.StartsWith("inp"))
    {
      Console.WriteLine(" -> " + variables["z"]);
      Console.Write(i.ToString() + ": " + variables["w"]);
      i++;
    }
  }

  Console.WriteLine(" -> " + variables["z"]);

  return (variables["w"], variables["x"], variables["y"], variables["z"]);
}