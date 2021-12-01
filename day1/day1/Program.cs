using System;
using System.IO;
using System.Linq;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split("\r\n").Select(l => int.Parse(l));
            Console.WriteLine("part1: " + input.Skip(1).Zip(input).Count(z => z.First > z.Second));

            var input3 = input.Skip(2).Zip(input.Skip(1).Zip(input)).Select(z => z.First + z.Second.First + z.Second.Second);
            Console.WriteLine("part2: " + input3.Skip(1).Zip(input3).Count(z => z.First > z.Second));
        }
    }
}
