using System.Numerics;

int index = 0;

var program = File.ReadAllLines("input.txt").Select(l =>
{
  string[] tokens = l.Split(" ");
  string secondArg = tokens.Length == 3 ? tokens[2] : "";
  return (tokens[0], tokens[1], secondArg);
});

//var output = ProcessInput(new Stack<int>(new List<int>() { 7 }));
HashSet<BigInteger> results = new HashSet<BigInteger>();

BigInteger count = 0;
BigInteger min = 99999999999999;
BigInteger max = 0;

for (BigInteger i = 99999999999999; i > 9999999999999; i-= 1)
{
  if(i.ToString().Contains('0'))
    continue;

  //Console.Clear();
  var output = ProcessNumber(i);

  if (min >= output.Item4)
  {
    Console.WriteLine(i.ToString() + " : " + min.ToString() + " -> " + output.Item4.ToString());
    min = output.Item4;
  }

  //if (max < output.Item4)
  //{
  //  Console.WriteLine(i.ToString() + " : " + max.ToString() + " -> " + output.Item4.ToString());
  //  max = output.Item4;
  //}
}


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

  int count = 0;
  foreach (var instruction in program)
  {
    count++;
    if(count == 241)
    { 
    }

    BigInteger valueArg2 = 0;
    if (instruction.Item3 != "" && !BigInteger.TryParse(instruction.Item3, out valueArg2))
      valueArg2 = variables[instruction.Item3];
    variables[instruction.Item2] = instructions[instruction.Item1](variables[instruction.Item2], valueArg2);

    //Console.WriteLine($"{instruction.Item1} {instruction.Item2} {instruction.Item3} => " + (variables["w"], variables["x"], variables["y"], variables["z"]));
  }

  return (variables["w"], variables["x"], variables["y"], variables["z"]);
}