int player1 = 4;
int player2 = 8;

int player1Score = 0;
int player2Score = 0;

//Part1();
Part2();

void Part2()
{
  var values = Enumerable.Range(3, 7);

  var states = new Queue<(int, int, int, int)>();
  var found = new HashSet<(int, int, int, int)>();

  states.Enqueue((4, 8, 0, 0));

  while (states.Any())
  {
    var current = states.Dequeue();
    // player1
    foreach (int v1 in values)
    {
      int p1 = (current.Item1 + v1 - 1) % 10 + 1;
      int s1 = current.Item3 + p1;
      foreach (int v2 in values)
      {
        int p2 = (current.Item2 + v2 - 1) % 10 + 1;
        int s2 = current.Item4 + p2;

        if (s1 < 21 && s2 < 21)
        {
          var newState = (p1, p2, s1, s2);
          states.Enqueue(newState);
          found.Add(newState);
        }
      }
    }
  }

  Console.WriteLine(values);
}

void Part1()
{
  int currentDice = 1;
  while (true)
  {
    player1 = ((player1 + ((currentDice + 1) * 3) - 1) % 10) + 1;
    player1Score += player1;
    currentDice += 3;

    if (player1Score >= 1000)
      break;

    player2 = ((player2 + ((currentDice + 1) * 3) - 1) % 10) + 1;
    player2Score += player2;
    currentDice += 3;

    if (player2Score >= 1000)
      break;
  }

  Console.WriteLine((currentDice - 1) * Math.Min(player1Score, player2Score));
}
