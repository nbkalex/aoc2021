using System.Diagnostics;

var input = File.ReadAllLines("input.txt");
var template = input[0];
var pairs = input.Skip(2).ToDictionary(l => l.Split(" -> ")[0], l => l.Split(" -> ")[1]);

var tests = new Dictionary<int, string>()
{
    {0, "NCNBCHB" },
    {1, "NBCCNBBBCBHCB" },
    {2, "NBBBCNCCNBBNBNBBCHBHHBCHB" },
    {3, "NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB" },
};

var freqPairs = pairs.ToDictionary(kvp => kvp.Key, kvp => template.IndexOf(kvp.Key) != -1 ? (long)1 : 0);
Dictionary<string, long> count2 = template.Distinct().ToDictionary(c => c.ToString(), c => (long)0);
foreach (var c in template)
    count2[c.ToString()]++;

foreach (var step in Enumerable.Range(0, 10))
{
    var freqPairsCopy = freqPairs.ToDictionary(f => f.Key, f => f.Value);

    Dictionary<string, long> newFreq = freqPairs.ToDictionary(f => f.Key, f => (long)0);
    foreach (var pair in pairs)
    {
        long current = freqPairsCopy[pair.Key];
        if (current == 0)
            continue;

        freqPairs[pair.Key] = 0;

        if(count2.ContainsKey(pair.Value))
            count2[pair.Value] += current;
        else
            count2.Add(pair.Value, current);

        newFreq[pair.Key[0] + pair.Value] += current;
        newFreq[pair.Value + pair.Key[1]] += current;
    }

    foreach (var pair in pairs)
        freqPairs[pair.Key] = newFreq[pair.Key];

    var count = freqPairs.Values.Sum();
}

var allElements = freqPairs.Keys.Aggregate("", (acc, k) => acc + k).ToHashSet().ToDictionary(c => c, c => (long)0);
foreach (var pair in freqPairs)
{
    allElements[pair.Key[0]] += pair.Value;
    allElements[pair.Key[1]] += pair.Value;
}


Console.WriteLine(count2.Values.Max() - count2.Values.Min());
