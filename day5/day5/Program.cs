// See https://aka.ms/new-console-template for more information
using System.Drawing;

var input = File.ReadAllLines("input.txt");

var lines = input.Select(line =>
    line.Split(" -> ").Select(point => new Point(int.Parse(point.Split(",")[0]), int.Parse(point.Split(",")[1]))).ToArray());

var points = new Dictionary<Point, int>();

foreach (var line in lines)
{
    //if (line[0].X != line[1].X && line[0].Y != line[1].Y)
    //    continue;

    int xDif = Math.Abs(line[0].X - line[1].X);
    int yDif = Math.Abs(line[0].Y - line[1].Y);

    // diagonals handling
    if (line[0].X != line[1].X && line[0].Y != line[1].Y)
    {
        for (int i = 0; i <= xDif; i++)
        {
            int directionX = line[0].X < line[1].X ? 1 : -1;
            int directionY = line[0].Y < line[1].Y ? 1 : -1;

            Point p = new Point(line[0].X + (i * directionX), line[0].Y + (i * directionY));
            if (points.ContainsKey(p))
                points[p]++;
            else
                points.Add(p, 1);
        }

        continue;
    }

    int minX = Math.Min(line[0].X, line[1].X);
    if (xDif != 0)
    {
        for (int x = 0; x <= xDif; x++)
        {
            var p = new Point(minX + x, line[0].Y);
            if (points.ContainsKey(p))
                points[p]++;
            else
                points.Add(p, 1);
        }
    }

    if (yDif != 0)
    {
        int minY = Math.Min(line[0].Y, line[1].Y);
        for (int y = 0; y <= yDif; y++)
        {
            var p = new Point(line[0].X, minY + y);
            if (points.ContainsKey(p))
                points[p]++;
            else
                points.Add(p, 1);
        }
    }
}

//Print(points);
Console.WriteLine(points.Count(p => p.Value > 1));

void Print(Dictionary<Point, int> values)
{
    Console.Clear();
    foreach (var val in values)
    {
        Console.SetCursorPosition(val.Key.X, val.Key.Y);
        Console.Write(val.Value);
    }

    Console.SetCursorPosition(0, 30);
}