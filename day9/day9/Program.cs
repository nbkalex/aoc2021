// See https://aka.ms/new-console-template for more information
using System.Drawing;

List<(int, int)> neighbours = new List<(int, int)>()
{
    (0, 1),
    (0,-1),
    (1, 0),
    (-1,0)
};

var map = File.ReadAllLines("input.txt").Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();


Dictionary<Point, int> lowPoints = new Dictionary<Point, int>();
for (int i = 0; i < map.Count(); i++)
{
    for (int j = 0; j < map[0].Count(); j++)
    {
        bool isLower = true;
        foreach (var neighbour in neighbours)
        {
            int x = i + neighbour.Item1;
            int y = j + neighbour.Item2;

            if (x < 0 || y < 0 || x >= map.Count() || y >= map[0].Count())
                continue;

            if (map[x][y] <= map[i][j])
            {
                isLower = false;
                break;
            }
        }

        if (isLower)
            lowPoints.Add(new Point(i,j), map[i][j]);
    }
}

List<int> basinsSizes = new List<int>();
foreach(var lowPoint in lowPoints)
    basinsSizes.Add(GetBasin(map, lowPoint.Key.X, lowPoint.Key.Y, neighbours, new HashSet<Point>()));

basinsSizes = basinsSizes.OrderByDescending(s => s).ToList();

Console.WriteLine(lowPoints.Values.Sum() + lowPoints.Count);
Console.WriteLine(basinsSizes[0] * basinsSizes[1] * basinsSizes[2]);


static int GetBasin(int[][] map, int x, int y, List<(int, int)> neighbours, HashSet<Point> aScanned)
{
    int size = 1;

    foreach (var neighbour in neighbours)
    {
        int x1 = x + neighbour.Item1;
        int y1 = y + neighbour.Item2;

        if (x1 < 0 || y1 < 0 || x1 >= map.Count() || y1 >= map[0].Count())
            continue;

        if (map[x1][y1] != 9 && map[x1][y1] > map[x][y] && aScanned.Add(new Point(x1,y1)))
            size += GetBasin(map, x1, y1, neighbours, aScanned);
    }

    return size;
}




