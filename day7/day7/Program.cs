using System;
using System.IO;
using System.Linq;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers =  File.ReadAllText("input.txt").Split(",").Select(n => int.Parse(n));
            Console.WriteLine("Part 1: " + Enumerable.Range(0, numbers.Max()).ToDictionary(n => n, n => numbers.Aggregate(0, (seed, n1) => seed + Math.Abs(n1 - n))).Min(dif => dif.Value));
            Console.WriteLine("Part 2: " + Enumerable.Range(0, numbers.Max()).ToDictionary(n => n, n => numbers.Aggregate(0, (seed, n1) => seed + (Math.Abs(n1 - n) * (Math.Abs(n1 - n)+1)/2))).Min(dif => dif.Value));
        }
    }
}
