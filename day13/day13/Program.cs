using System.Drawing;

var lines = File.ReadAllLines("input.txt");
HashSet<(int, int)> map = new HashSet<(int, int)>();

bool readPoints = true;
var folds = new List<(string, int)>();
foreach (var line in lines)
{
  if (!line.Any())
  {
    readPoints = false;
    continue;
  }

  if (readPoints)
    map.Add((int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1])));
  else
  {
    var tokens = line.Split(" ")[2].Split("=");
    folds.Add((tokens[0], int.Parse(tokens[1])));
  }
}


foreach (var fold in folds)
{
  HashSet<(int,int)> foldedMap = new HashSet<(int, int)>();

  int value = fold.Item2;
  bool isX = fold.Item1 == "x";
  foreach(var point in map)
  {
    if(isX)
    {
      if(point.Item1 < value)
        foldedMap.Add(point);
      else
        foldedMap.Add((value - (point.Item1 - value), point.Item2));
    }
    else
    {
      if (point.Item2 < value)
        foldedMap.Add(point);
      else
        foldedMap.Add((point.Item1, value - (point.Item2 - value)));
    }
  }

  map = foldedMap;
}

int minX = map.Min(p => p.Item1);
int minY = map.Min(p => p.Item2);

int offX = minX < 0 ? Math.Abs(minX) : 0;
int offY = minY < 0 ? Math.Abs(minY) : 0;
Console.Clear();
foreach (var point in map)
{
  Console.SetCursorPosition(point.Item1 + offX, point.Item2 + offY);
  Console.Write("#");
}

Console.ReadKey();