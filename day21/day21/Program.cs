int player1 = 8;
int player2 = 5;

int player1Score = 0;
int player2Score = 0;

var values = new List<int>();
for (int turn1 = 1; turn1 <= 3; turn1++)
  for (int turn2 = 1; turn2 <= 3; turn2++)
    for (int turn3 = 1; turn3 <= 3; turn3++)
      values.Add(turn1 + turn2 + turn3);

var cachedStates = new Dictionary<(int, int, int, int), (long, long)>();

//Part1();
var result2 = Part2((player1, player2, 0, 0));

Console.WriteLine(Math.Max(result2.Item1, result2.Item2));

(long, long) Part2((int, int, int, int) state)
{
  if (cachedStates.ContainsKey(state))
    return cachedStates[state];

  long total1 = 0;
  long total2 = 0;

  foreach (int v1 in values)
  {
    int p1 = (state.Item1 + v1 - 1) % 10 + 1;
    int s1 = state.Item3 + p1;
    if (s1 >= 21)
    {
      total1++;
      var newState = (p1, state.Item2, s1, state.Item4);
      if (!cachedStates.ContainsKey(newState))
        cachedStates.Add(newState, (1, 0));
      continue;
    }

    foreach (int v2 in values)
    {
      int p2 = (state.Item2 + v2 - 1) % 10 + 1;
      int s2 = state.Item4 + p2;
      var newState = (p1, p2, s1, s2);

      if (s2 >= 21)
      {
        total2++;
        if (!cachedStates.ContainsKey(newState))
          cachedStates.Add(newState, (0, 1));
        continue;
      }
      else
      {
        var result = Part2(newState);
        total1 += result.Item1;
        total2 += result.Item2;
      }
    }
  }


  var total = (total1, total2);
  cachedStates.Add(state, total);
  return total;
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
