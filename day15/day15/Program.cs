var map = File.ReadAllLines("input.txt").Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
List<(int, int)> neighbours = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0) };

int width = map[0].Length;
int height = map.Length;
(int, int) startPos = (0, 0);
(int, int) endPos = (width-1, height-1);

Dictionary<(int, int), int> mapRisk = new Dictionary<(int, int), int>();
for (int i = 0; i < height; i++)
    for (int j = 0; j < width; j++)
        mapRisk.Add((i, j), map[i][j]);


Queue<(int, int)> toVisit = new Queue<(int, int)>();
toVisit.Enqueue(startPos);
Dictionary<(int, int), int> roads = new Dictionary<(int, int), int>() { { startPos, 0 } };
while (toVisit.Any())
{
    var current = toVisit.Dequeue();
    var currentPos = (current.Item1, current.Item2);
    int currentRisk = roads[currentPos];

    foreach (var n in neighbours)
    {
        var nextPos = (currentPos.Item1 + n.Item1, currentPos.Item2 + n.Item2);
        if (nextPos.Item1 < 0 || nextPos.Item1 >= height || nextPos.Item2 < 0 || nextPos.Item2 >= width)
            continue;

        var nextRisk = currentRisk + mapRisk[nextPos];
        if (!roads.ContainsKey(nextPos) || roads[nextPos] > nextRisk)
        { 
            roads[nextPos] = nextRisk;
            toVisit.Enqueue(nextPos);
        }
    }

    //PrintRoads(roads);
}

Console.WriteLine();
Console.WriteLine(roads[endPos]);

void PrintRoads(Dictionary<(int, int), int> roads)
{
    Console.Clear();
    foreach(var road in roads)
    {
        Console.SetCursorPosition(road.Key.Item2, road.Key.Item1);
        Console.Write(road.Value + " ");
    }
}
