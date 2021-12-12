// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var map = File.ReadAllLines("input.txt").Select(l => (l.Split("-")[0], l.Split("-")[1])).ToList();
map.AddRange(map.Select(n => (n.Item2, n.Item1)).ToList());

const string start = "start";
const string end = "end";

Stack<(string, HashSet<string>)> toVisit = new Stack<(string, HashSet<string>)>();
toVisit.Push((start, new HashSet<string>()));

int pathsCount = 0;
while (toVisit.Any())
{
    var current = toVisit.Pop();
    string currentNode = current.Item1;
    if (currentNode == end)
    {
        pathsCount++;
        continue;
    }

    HashSet<string> currendVisited = current.Item2;

    if (char.IsLower(currentNode[0]) && !currendVisited.Add(currentNode))
        continue;

    var nextNodes = map.Where(n => n.Item1 == currentNode).ToArray();
    foreach (var adiacentNode in nextNodes)
        toVisit.Push((adiacentNode.Item2, new HashSet<string>(currendVisited)));
}

Console.WriteLine(pathsCount);