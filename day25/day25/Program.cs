using System.Text;

var input = File.ReadAllLines("input.txt");


int step = 0;

bool repeat = true;

while (repeat)
{
  repeat = false;
  step++;

  var inputCopy = input.Clone() as string[];
  for (int i = 0; i < inputCopy.Length; i++)
  {
    for (int j = 0; j < inputCopy[i].Length; j++)
    {
      if (input[i][j] == '>')
      {
        int nextPos = (j + 1) % inputCopy[i].Length;
        if (input[i][nextPos] == '.')
        {
          var newVal = new StringBuilder(inputCopy[i]);
          newVal[nextPos] = '>';
          newVal[j] = '.';
          inputCopy[i] = newVal.ToString();
          j++;
          repeat = true;
          continue;
        }
      }
    }
  }

  input = inputCopy.Clone() as string[];

  for (int i = 0; i < inputCopy.Length; i++)
  {
    for (int j = 0; j < inputCopy[i].Length; j++)
    {
      if (input[i][j] == 'v')
      {
        int nextPos = (i + 1) % inputCopy.Length;
        if (input[nextPos][j] == '.')
        {
          var newVal1 = new StringBuilder(inputCopy[i]);
          newVal1[j] = '.';
          inputCopy[i] = newVal1.ToString();

          var newVal = new StringBuilder(inputCopy[nextPos]);
          newVal[j] = 'v';
          inputCopy[nextPos] = newVal.ToString();
          repeat = true;
        }
      }
    }
  }

  input = inputCopy;
  //Console.WriteLine();
  //foreach (string line in input)
  //  Console.WriteLine(line);
}

Console.WriteLine(step);