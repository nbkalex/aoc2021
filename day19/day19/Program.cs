var lines = File.ReadAllLines("input.txt");

List<Scanner> scanners = new List<Scanner>();

Scanner currentScanner = null;
foreach (var line in lines)
{
  if (!line.Any())
    continue;

  if (line.StartsWith("---"))
  {
    currentScanner = new Scanner() { Name = line };
    scanners.Add(currentScanner);
  }
  else
  {
    var coords = line.Split(",").Select(coord => int.Parse(coord)).ToList();
    currentScanner.Beacons.Add((coords[0], coords[1], coords[2]), new Dictionary<(int, int, int), int>());
  }
}

// compute distances between beacons
Dictionary<(Scanner, int), ((int, int, int), (int, int, int))> distPair = new Dictionary<(Scanner, int), ((int, int, int), (int, int, int))>();
foreach (var scanner in scanners)
{
  foreach (var beacon1 in scanner.Beacons.Keys)
  {
    foreach (var beacon2 in scanner.Beacons.Keys)
    {
      if (beacon1 == beacon2)
        continue;

      int dist = Math.Abs(beacon1.Item1 - beacon2.Item1) +
                 Math.Abs(beacon1.Item2 - beacon2.Item2) +
                 Math.Abs(beacon1.Item3 - beacon2.Item3);

      scanner.Beacons[beacon1].Add(beacon2, dist);

      //if(!distPair.ContainsKey((scanner, dist)))
      //  distPair.Add((scanner, dist), (beacon1, beacon2));
    }
  }
}

List<((int, int, int), (int, int, int))> commonBeacons = new List<((int, int, int), (int, int, int))>();

List<int> commonDistances = new List<int>();

foreach (var scanner in scanners)
{
  foreach (var scanner2 in scanners)
  {
    if (scanner2 == scanner)
      continue;

    foreach (var beacon1 in scanner.Beacons)
    {
      HashSet<int> beaconSet = new HashSet<int>();

      foreach (var beacon2 in scanner2.Beacons)
      {
        var intersection = beacon1.Value.Values.Intersect(beacon2.Value.Values).ToHashSet();
        commonDistances.AddRange(intersection);

        if (intersection.Count == 11)
        {         
          foreach(var common in intersection)
            beaconSet.Add(common);

          commonBeacons.Add((beacon1.Key, beacon2.Key));
        }
      }
    }
  }
}

var uniques = commonDistances.ToHashSet();

Console.WriteLine();

class Scanner
{
  public string Name { get; set; }
  public Dictionary<(int, int, int), Dictionary<(int, int, int), int>> Beacons { get; set; } = new Dictionary<(int, int, int), Dictionary<(int, int, int), int>>();
}