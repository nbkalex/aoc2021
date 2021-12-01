using System;
using System.IO;
using System.Linq;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split("\r\n").Select(l => int.Parse(l)).ToArray();
            Console.WriteLine("part1: " + input.Skip(1).Zip(input).Count(z => z.First > z.Second));
            Console.WriteLine("part2: " + input.Skip(3).Zip(input).Count(z => z.First > z.Second));
        }
    }
}
