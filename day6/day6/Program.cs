using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            int days = 256;
            var input = File.ReadAllLines("input.txt")[0].Split(",").Select(l => int.Parse(l));
            var numbers = Enumerable.Range(0, 9).ToDictionary(n => (long)n, n => (long)input.Count(i => i == n));
            Enumerable.Range(0, days+1).Aggregate((_,__) =>
            {
                long zeros = numbers[0];
                for (int j = 0; j < 8; j++)
                    numbers[j] = numbers[j + 1];

                numbers[6] += zeros;
                numbers[8] = zeros;
                return 0;
            });

            Console.WriteLine(numbers.Values.Sum());
        }
    }
}
