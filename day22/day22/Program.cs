using System.Numerics;

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

var initialized = new List<(bool, (int, int), (int, int), (int, int))>();
var negatives = new Dictionary<(bool, (int, int), (int, int), (int, int)), int>();

foreach (var cube in cubes)
{
  int x1 = cube.Item2.Item1;
  int x2 = cube.Item2.Item2;

  int y1 = cube.Item3.Item1;
  int y2 = cube.Item3.Item2;

  int z1 = cube.Item4.Item1;
  int z2 = cube.Item4.Item2;

  var updateIntersections = new List<(bool, (int, int), (int, int), (int, int))>();
  foreach (var initCube in initialized)
  {
    var intersection = GetIntersection(initCube, cube);
    if (intersection == null)
      continue;

    updateIntersections.Add((!initCube.Item1, intersection.Value.Item2, intersection.Value.Item3, intersection.Value.Item4));
  }

  if (cube.Item1)
    initialized.Add(cube);

  initialized.AddRange(updateIntersections);
}

BigInteger total = 0;
foreach (var cube in initialized)
{
  if (cube.Item1)
    total += GetVol(cube);
  else
    total -= GetVol(cube);
}

Console.WriteLine(total);


BigInteger GetVol((bool, (int, int), (int, int), (int, int)) cub)
{
  int x1 = cub.Item2.Item1;
  int x2 = cub.Item2.Item2;

  int y1 = cub.Item3.Item1;
  int y2 = cub.Item3.Item2;

  int z1 = cub.Item4.Item1;
  int z2 = cub.Item4.Item2;

  return (BigInteger)(Math.Abs(x2 - x1) + 1) * (BigInteger)(Math.Abs(y2 - y1) + 1) * (BigInteger)(Math.Abs(z2 - z1) + 1);
}

(bool, (int, int), (int, int), (int, int))? GetIntersection((bool, (int, int), (int, int), (int, int)) cub1, (bool, (int, int), (int, int), (int, int)) cub2)
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

  int x1Dif = Math.Max(x11, x21);
  int x2Dif = Math.Min(x12, x22);

  if (x1Dif > x2Dif)
    return null;

  int y1Dif = Math.Max(y11, y21);
  int y2Dif = Math.Min(y12, y22);

  if (y1Dif > y2Dif)
    return null;

  int z1Dif = Math.Max(z11, z21);
  int z2Dif = Math.Min(z12, z22);

  if (z1Dif > z2Dif)
    return null;

  return (cub2.Item1, (x1Dif, x2Dif), (y1Dif, y2Dif), (z1Dif, z2Dif));
}

bool IsEmpty((bool, (int, int), (int, int), (int, int)) cube)
{
  return cube.Item2.Item1 == 0 || cube.Item2.Item2 == 0
      || cube.Item3.Item1 == 0 || cube.Item3.Item2 == 0
      || cube.Item4.Item1 == 0 || cube.Item4.Item2 == 0;
}