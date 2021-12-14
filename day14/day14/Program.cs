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



foreach (var step in Enumerable.Range(0, 10))
{
    //Console.WriteLine("----------------------");

    //var freqPairsCopy = freqPairs.ToDictionary(f => f.Key, f => f.Value);
    Console.WriteLine(freqPairs.Values.Sum());
    Dictionary<string, long> newFreq = freqPairs.ToDictionary(f => f.Key, f => (long)0);
    foreach (var pair in pairs)
    {
        long current = freqPairs[pair.Key];
        if (current == 0)
            continue;

        newFreq[pair.Key[0] + pair.Value] += current;
        newFreq[pair.Value + pair.Key[1]] += current;
    }

    foreach (var pair in pairs)
    { 
        if(newFreq[pair.Key] > 0)
            freqPairs[pair.Key] = newFreq[pair.Key];
    }

    //    var checkSize = freqPairs.Values.Sum();

    //List<(string, int)> pairsFound = new List<(string, int)>();

    //foreach (var pair in pairs)
    //{
    //    int offSet = 0;
    //    while (true)
    //    {
    //        int indexFound = template.IndexOf(pair.Key, offSet);
    //        if (indexFound == -1)
    //            break;

    //        pairsFound.Add((pair.Value, ++indexFound));

    //        offSet = indexFound;
    //    }
    //}

    //int offset = 0;
    //foreach (var pair in pairsFound.OrderBy(p => p.Item2))
    //{
    //    template = template.Insert(offset + pair.Item2, pair.Item1);
    //    offset++;
    //}

    //Console.WriteLine();

    //if(tests.ContainsKey(step))  
    //    Debug.Assert(template == tests[step]);

    //Console.WriteLine(template);

    //var freqs = template.Distinct().ToDictionary(c => c, c => template.Count(c2 => c == c2));
    //Console.WriteLine(freqs.Values.Max().ToString() + "-" + freqs.Values.Min());
}

var allElements = freqPairs.Keys.Aggregate("", (acc, k) => acc + k).ToHashSet().ToDictionary(c => c, c => (long)0);
foreach (var pair in freqPairs)
{
    allElements[pair.Key[0]] += pair.Value;
    allElements[pair.Key[1]] += pair.Value;
}


var freqs2 = template.Distinct().ToDictionary(c => c, c => template.Count(c2 => c == c2));
Console.WriteLine(allElements.Values.Max() - allElements.Values.Min());
