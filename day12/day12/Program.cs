// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var map = File.ReadAllLines("input.txt").Select(l => (l.Split("-")[0], l.Split("-")[1])).ToList();
map.AddRange(map.Select(n => (n.Item2, n.Item1)).ToList());

const string start = "start";
const string end = "end";

var visited = map.Select(n => n.Item1).ToHashSet().ToDictionary(n => n, n => 0);

Stack<(string, Dictionary<string, int>, string)> toVisit = new Stack<(string, Dictionary<string, int>, string)>();
toVisit.Push((start, visited, start));

int pathsCount = 0;
while (toVisit.Any())
{
    var current = toVisit.Pop();
    string currentNode = current.Item1;
    string currentPath = current.Item3;
    if (currentNode == end)
    {
        pathsCount++;
        //Console.WriteLine(currentPath);
        continue;
    }

    Dictionary<string, int> currendVisited = current.Item2;

    if (currentNode == start && currentPath != start)
        continue;

    bool visitedTwice = currendVisited.Where(kvp => char.IsLower(kvp.Key[0])).Any(kvp => kvp.Value == 2);
    if (char.IsLower(currentNode[0]))
        if(currendVisited[currentNode] == 2 || (visitedTwice && currendVisited[currentNode] == 1))
            continue;

    currendVisited[currentNode]++;

    var nextNodes = map.Where(n => n.Item1 == currentNode).OrderByDescending(n => n.Item2).ToArray();
    foreach (var adiacentNode in nextNodes)
        toVisit.Push((adiacentNode.Item2, new Dictionary<string, int>(currendVisited), currentPath + "," + adiacentNode.Item2));
}

Console.WriteLine(pathsCount);