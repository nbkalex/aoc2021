string range = "x=96..125, y=-144..-98";

// x = n(n+1)/2 > 96 

// my input
int minX = 96;
int maxX = 125;
int minY = -144;
int maxY = -98;

// example
// int minX = 20;
// int maxX = 30;
// int minY = -10;
// int maxY = -5;

HashSet<int> xs = new HashSet<int>();
for (int i = 1; i <= maxX; i++)
{
  int velocity = i;
  int x = 0;
  while (x + velocity <= maxX && velocity != 0)
  {
    x += velocity;

    if (velocity > 0)
      velocity--;
  }

  if (x >= minX)
    xs.Add(i);
}

var yRange = Enumerable.Range(1, 1000);

Dictionary< int,(int,int)> pairs = new Dictionary<int,(int,int)> ();
HashSet<(int,int)> pairs2 = new HashSet<(int,int)> ();

foreach (var x in xs)
{
  for(int y = minY; y < 10000; y++)
  {
    int xVel = x;
    int xPos = 0;

    int yPos = 0;
    int yVel = y;

    int maxPosY = 0;
    while(xPos + xVel <= maxX && yPos >= minY)
    {
      xPos += xVel;

      if (xVel > 0)
        xVel--;

      yPos += yVel;
      yVel--;

      if(maxPosY < yPos)
        maxPosY = yPos;

      if (yPos >= minY && yPos <= maxY && xPos >= minX && xPos <= maxX)
      {
        pairs2.Add((x,y));
        if (!pairs.ContainsKey(maxPosY))
          pairs.Add(maxPosY, (x,y));
      else if(pairs[maxPosY].Item2 < y)
          pairs[maxPosY] = (x,y);
      }
    }
  }
}

Console.WriteLine(pairs.Keys.Max());
Console.WriteLine(pairs2.Count());

foreach(var pair in pairs2)
{
  Console.WriteLine(pair.ToString());
}