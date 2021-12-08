int total = 0;
var lines = File.ReadAllLines("input.txt");
List<string> numbers = new List<string>() { "abcefg", "cf", "acdeg", "acdfg", "bcdf", "abdfg", "abdefg", "acf", "abcdefg", "abcdfg" };
string segments = "abcdefg";
var mappedNumbers = numbers.ToDictionary(n => n, n => numbers.IndexOf(n));
foreach (var line in lines)
{

    string[] input = line.Split(" | ")[0].Split(" ");
    
    string[] toCount = line.Split(" | ")[1].Split(" ");

    var freq = segments.ToDictionary(s => s.ToString(), s => numbers.Count(n => n.Contains(s)));
    var freqCodded = segments.ToDictionary(s => s.ToString(), s => input.Count(n => n.Contains(s)));

    var decoded = new Dictionary<string, string>();

    // 1
    decoded.Add(numbers[1], input.FirstOrDefault(i => i.Length == 2));

    // 4
    decoded.Add(numbers[4], input.FirstOrDefault(i => i.Length == 4));

    // 7
    decoded.Add(numbers[7], input.FirstOrDefault(i => i.Length == 3));

    // 8
    decoded.Add(numbers[8], input.FirstOrDefault(i => i.Length == 7));

    // 2
    var maxFreqCodded = freqCodded.Values.Max();
    string eCodded = freqCodded.FirstOrDefault(fc => fc.Value == maxFreqCodded).Key;
    decoded.Add(numbers[2], input.FirstOrDefault(i => !i.Contains(eCodded)));

    // 6 (6 + 1 = 8)
    decoded.Add(numbers[6], input.FirstOrDefault(i => i.Length == 6 && (i + decoded[numbers[1]]).Distinct().OrderBy(c => c).SequenceEqual(numbers[8])));

    // 3 (1 + 3 = 3)
    decoded.Add(numbers[3], input.FirstOrDefault(i => i.Length == 5 && (i + decoded[numbers[1]]).Distinct().OrderBy(c => c).SequenceEqual(i.OrderBy(c => c))));

    // 9 (4 + 3 = 9)
    decoded.Add(numbers[9], input.FirstOrDefault(i => (decoded[numbers[4]] + decoded[numbers[3]]).Distinct().OrderBy(c => c).SequenceEqual(i.OrderBy(c => c))));

    // 5 (5 + 1 = 9)
    decoded.Add(numbers[5], input.FirstOrDefault(i => i.Length == 5 && (decoded[numbers[1]] + i).Distinct().OrderBy(c => c).SequenceEqual(decoded[numbers[9]].OrderBy(c => c))));

    // 0
    decoded.Add(numbers[0], input.FirstOrDefault(i => !decoded.Values.Contains(i)));

    var decodded2 = decoded.ToDictionary(kvp => new string(kvp.Value.OrderBy(c => c).ToArray()), kvp => kvp.Key);

    int number = 0;
    foreach (var digit in toCount.Select(tc => new string(tc.OrderBy(c => c).ToArray())).Select(tc => mappedNumbers[decodded2[tc]]))
        number = number * 10 + digit;

    total += number;
}

Console.WriteLine(total);