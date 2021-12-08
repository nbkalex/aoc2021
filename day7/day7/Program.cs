using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllText("input.txt").Split(",").Select(n => int.Parse(n));
            Console.WriteLine("Part 1: " + Enumerable.Range(0, numbers.Max()).Select(n => numbers.Select(n1 => Math.Abs(n1 - n)).Sum()).Min());
            Console.WriteLine("Part 2: " + Enumerable.Range(0, numbers.Max()).Select(n => numbers.Select(n1 => Math.Abs(n1 - n) * (Math.Abs(n1 - n) + 1) / 2).Sum()).Min());
        }
    }
}
