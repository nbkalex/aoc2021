var lines = File.ReadAllLines("input.txt");

List<(int, int, int)> orientations = new List<(int, int, int)>()
  {
    (1,1,1),
    (1,1,-1),
    (1,-1,1),
    (1,-1,-1),
    (-1,1,1),
    (-1,1,-1),
    (-1,-1,1),
    (-1,-1,-1)
  };

List<Func<(int, int, int), (int, int, int)>> transforms = new List<Func<(int, int, int), (int, int, int)>>()
  {
    b => (b.Item1, b.Item2, b.Item3),
    b => (b.Item1, b.Item3, b.Item2),
    b => (b.Item2, b.Item1, b.Item3),
    b => (b.Item2, b.Item3, b.Item1),
    b => (b.Item3, b.Item1, b.Item2),
    b => (b.Item3, b.Item2, b.Item1)
  };


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
    currentScanner.Beacons.Add((coords[0], coords[1], coords[2]));
  }
}

Dictionary<Scanner, (int, int, int)> detectedScanners = new Dictionary<Scanner, (int, int, int)>()
{
  { scanners[0], (0,0,0) }
};

HashSet<(int, int, int)> beacons = scanners[0].Beacons.ToHashSet();

while (detectedScanners.Count != scanners.Count)
{
  foreach (var scanner in scanners)
  {
    foreach (var currentScanner2 in scanners)
    {
      if (scanner == currentScanner2)
        continue;

      if (detectedScanners.ContainsKey(currentScanner2))
        continue;

      if (!detectedScanners.ContainsKey(scanner))
        continue;

      var currentScannerPos = detectedScanners[scanner];
      var scannerPos = GetOverlappingBeacons(scanner, currentScanner2);

      if (scannerPos != (0, 0, 0))
      {
        foreach (var beacon in currentScanner2.Beacons)
          beacons.Add(beacon);

        detectedScanners[currentScanner2] = scannerPos;
      }
    }
  }
}

int maxDist = 0;
foreach (var scannerPos in detectedScanners.Values)
{
  foreach (var scannerPos2 in detectedScanners.Values)
  {
    int dist = Math.Abs(scannerPos.Item1 - scannerPos2.Item1)
             + Math.Abs(scannerPos.Item2 - scannerPos2.Item2)
             + Math.Abs(scannerPos.Item3 - scannerPos2.Item3);
    if (dist > maxDist)
      maxDist = dist;
  }
}

Console.WriteLine(beacons.Count);
Console.WriteLine(maxDist);


(int, int, int) GetOverlappingBeacons(Scanner scanner1, Scanner scanner2)
{
  foreach (var orientation in orientations)
  {
    // apply orientation
    var oriented_beacons = scanner2.Beacons.Select(b => (b.Item1 * orientation.Item1, b.Item2 * orientation.Item2, b.Item3 * orientation.Item3));

    foreach (var transform in transforms)
    {
      // apply transform
      var transformed_beacons = oriented_beacons.Select(b => transform(b));

      foreach (var b1 in scanner1.Beacons)
      {
        foreach (var bt in transformed_beacons)
        {
          (int, int, int) offset = (bt.Item1 - b1.Item1, bt.Item2 - b1.Item2, bt.Item3 - b1.Item3);

          // apply offset
          var offsetted = transformed_beacons.Select(bt => (bt.Item1 - offset.Item1, bt.Item2 - offset.Item2, bt.Item3 - offset.Item3));
          var intersection = scanner1.Beacons.Intersect(offsetted);
          if (intersection.Count() == 12)
          {
            scanner2.Beacons = offsetted.ToList();
            return (offset.Item1, offset.Item2, offset.Item3);
          }
        }
      }
    }
  }

  return (0, 0, 0);
}

class Scanner
{
  public string Name { get; set; }
  public List<(int, int, int)> Beacons { get; set; } = new List<(int, int, int)>();
}