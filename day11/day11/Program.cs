using System.Drawing;

List<(int, int)> neighbours = new List<(int, int)>()
{
    (0, 1),
    (0,-1),
    (1, 0),
    (-1,0),
    (-1,-1),
    (1,1),
    (1,-1),
    (-1,1),
};

var map = File.ReadAllLines("input.txt").Select(l => l.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

int total = 0;
bool allFlash = false;
int step = 0;

while(!allFlash)
{
    step++;
    Queue<(int, int)> toFlash = new Queue<(int, int)>();
    HashSet<(int, int)> visited = new HashSet<(int, int)>();
    for (int i = 0; i < map.Length; i++)
    {
        for (int j = 0; j < map[0].Length; j++)
        {
            map[i][j]++;
            if (map[i][j] == 10)
            {
                toFlash.Enqueue((i, j));
                visited.Add((i, j));
                total++;
            }
        }
    }

    while (toFlash.Any())
    {
        var next = toFlash.Dequeue();
        int x = next.Item1;
        int y = next.Item2;

        map[x][y] = map[x][y] % 10;
        foreach (var neighbour in neighbours)
        {
            int xn = x + neighbour.Item1;
            int yn = y + neighbour.Item2;
            if (xn < 0 || yn < 0 || xn >= map.Count() || yn >= map[0].Count())
                continue;

            if (!visited.Contains((xn, yn)))
            {
                map[xn][yn]++;

                if (map[xn][yn] >= 10)
                { 
                    toFlash.Enqueue((xn, yn));
                    visited.Add((xn, yn));
                    total++;
                }
            }
        }
    }

    allFlash = visited.Count == map.Length * map[0].Length;
    //PrintMap(map);
}

Console.WriteLine("part2: " + step);
Console.WriteLine(total);

void PrintMap(int[][] map)
{
    Console.Clear();
    for (int i = 0; i < map.Length; i++)
    {
        Console.WriteLine();
        for (int j = 0; j < map[0].Length; j++)
            Console.Write(map[i][j]);
    }
}