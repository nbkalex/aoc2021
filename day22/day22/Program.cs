var input = File.ReadAllLines("input.txt");

var cubes = new List<(bool, (int, int), (int, int), (int, int))>();
foreach (var line in input)
{
  bool on = line.StartsWith("on");
  var boundsTokens = line.Split(" ")[1].Split(",");
  (int, int) xs = (int.Parse(boundsTokens[0].Substring(2).Split("..")[0]), int.Parse(boundsTokens[0].Substring(2).Split("..")[1]));
  (int, int) ys = (int.Parse(boundsTokens[1].Substring(2).Split("..")[0]), int.Parse(boundsTokens[1].Substring(2).Split("..")[1]));
  (int, int) zs = (int.Parse(boundsTokens[2].Substring(2).Split("..")[0]), int.Parse(boundsTokens[2].Substring(2).Split("..")[1]));

  cubes.Add((on, xs, ys, zs));
}

Dictionary<(int,int,int), bool> cubesFound = new Dictionary<(int, int, int), bool>();

foreach (var cub in cubes)
{
  int x1 = cub.Item2.Item1;
  int x2 = cub.Item2.Item2;

  int y1 = cub.Item3.Item1;
  int y2 = cub.Item3.Item2;

  int z1 = cub.Item4.Item1;
  int z2 = cub.Item4.Item2;

  if (x1 >= -50 && x2 <= 50
   && y1 >= -50 && y2 <= 50
   && z1 >= -50 && z2 <= 50)
  {
    for (int i = x1; i <= x2; i++)
      for (int j = y1; j <= y2; j++)
        for (int k = z1; k <= z2; k++)
        {
          if(cubesFound.ContainsKey((i,j,k)))
            cubesFound[(i,j,k)] = cub.Item1;
          else
            cubesFound.Add((i,j,k), cub.Item1);
        }
  }
}

Console.WriteLine(cubesFound.Values.Count(v => v));