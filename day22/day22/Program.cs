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

Dictionary<(int, int, int), bool> cubesFound = new Dictionary<(int, int, int), bool>();
var initialized = new List<(bool, (int, int), (int, int), (int, int))>();
long total = 0;
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
    long vol = GetVol(cub);
    if(cub.Item1)
      total += vol;

    foreach (var initCub in initialized)
    {
      var intersection = GetIntersection(cub, initCub);
      total-= GetVol(intersection);
    }

    initialized.Add(cub);
  }
}


Console.WriteLine(total);


long GetVol((bool, (int, int), (int, int), (int, int)) cub)
{
  int x1 = cub.Item2.Item1;
  int x2 = cub.Item2.Item2;

  int y1 = cub.Item3.Item1;
  int y2 = cub.Item3.Item2;

  int z1 = cub.Item4.Item1;
  int z2 = cub.Item4.Item2;

  return Math.Abs(x2 - x1) * Math.Abs(y2 - y1) * Math.Abs(z2 - z1);
}

(bool, (int, int), (int, int), (int, int)) GetIntersection((bool, (int, int), (int, int), (int, int)) cub1, (bool, (int, int), (int, int), (int, int)) cub2)
{
  int x11 = cub1.Item2.Item1;
  int x12 = cub1.Item2.Item2;

  int y11 = cub1.Item3.Item1;
  int y12 = cub1.Item3.Item2;

  int z11 = cub1.Item4.Item1;
  int z12 = cub1.Item4.Item2;

  int x21 = cub2.Item2.Item1;
  int x22 = cub2.Item2.Item2;

  int y21 = cub2.Item3.Item1;
  int y22 = cub2.Item3.Item2;

  int z21 = cub2.Item4.Item1;
  int z22 = cub2.Item4.Item2;

  int x1Dif = 0;
  int x2Dif = 0;

  if (x11 <= x21 && x12 >= x21)
  {
    x1Dif = x21;
    x2Dif = Math.Min(x12, x22);
  }

  if (x11 <= x22 && x11 >= x21)
  {
    x1Dif = x11;
    x2Dif = Math.Min(x12, x22);
  }

  int y1Dif = 0;
  int y2Dif = 0;

  if (y11 <= y21 && y12 >= y21)
  {
    y1Dif = y21;
    y2Dif = Math.Min(y12, y22);
  }

  if (y11 <= y22 && y11 >= y21)
  {
    y1Dif = y11;
    y2Dif = Math.Min(y12, y22);
  }

  int z1Dif = 0;
  int z2Dif = 0;

  if (z11 <= z21 && z12 >= z21)
  {
    z1Dif = z21;
    z2Dif = Math.Min(z12, z22);
  }

  if (z11 <= z22 && z11 >= z21)
  {
    z1Dif = z11;
    z2Dif = Math.Min(z12, z22);
  }

  return (cub2.Item1, (x1Dif, x2Dif), (y1Dif, y1Dif), (z1Dif, z1Dif));
}