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
    currentScanner.Beacons.Add((coords[0], coords[1], coords[2]));
  }
}

Dictionary<Scanner, (int, int, int)> detectedScanners = new Dictionary<Scanner, (int, int, int)>()
{
  { scanners[0], (0,0,0) }
};

currentScanner = scanners[0];

HashSet<(int,int,int)> beacons = scanners[0].Beacons.ToHashSet();

while (detectedScanners.Count != scanners.Count)
{
  foreach (var scanner in scanners)
  {
    var scannerPos = GetOverlappingBeacons(currentScanner, scanner);
    if (scannerPos != (0, 0, 0))
    {
      var currentScannerPos = detectedScanners[currentScanner];
      scannerPos = ((currentScannerPos.Item1 + scannerPos.Item1, currentScannerPos.Item2 + scannerPos.Item2, currentScannerPos.Item3 + scannerPos.Item3));
      detectedScanners[scanner] = scannerPos;
      currentScanner = scanner;

      foreach(var scannerBeacons in scanner.Beacons)
        beacons.Add((scannerBeacons.Item1 + scannerPos.Item1, scannerBeacons.Item2 + scannerPos.Item2, scannerBeacons.Item3 + scannerPos.Item3));
    }
  }
}



Console.WriteLine();


(int, int, int) GetOverlappingBeacons(Scanner scanner1, Scanner scanner2)
{
  List<(int, int, int)> result = new List<(int, int, int)>();

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


  foreach (var orientation in orientations)
  {
    foreach (var transform in transforms)
    {
      // apply orientation
      var oriented_beacons = scanner2.Beacons.Select(b => (b.Item1 * orientation.Item1, b.Item2 * orientation.Item2, b.Item3 * orientation.Item3));

      // apply transform
      var transformed_beacons = oriented_beacons.Select(b => transform(b));

      foreach (var b1 in scanner1.Beacons)
      {
        foreach (var bt in transformed_beacons)
        {
          (int, int, int) offset = (b1.Item1 - bt.Item1, b1.Item2 - bt.Item2, b1.Item3 - bt.Item3);

          // apply offset
          var offsetted = transformed_beacons.Select(bt => (bt.Item1 + offset.Item1, bt.Item2 + offset.Item2, bt.Item3 + offset.Item3));
          var intersection = scanner1.Beacons.Intersect(offsetted).ToList();
          if (intersection.Count == 12)
          {
            return offset;
            //var rollbackOffset = transform(offset);
            //return (rollbackOffset.Item1 * orientation.Item1, rollbackOffset.Item2 * orientation.Item2, rollbackOffset.Item3 * orientation.Item3);
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