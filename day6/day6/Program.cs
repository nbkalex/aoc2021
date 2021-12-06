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
            var input = File.ReadAllLines("input.txt")[0].Split(",").Select(l => int.Parse(l)).ToList();
            long days = 256;

            var numbers = new Dictionary<long, long>();
            for (int i = 0; i <= 8; i++)
                numbers.Add(i, 0);

            foreach (var i in input)
                numbers[i]++;

            for (int i = 0; i < days; i++)
            {
                long zeros = numbers[0];

                long lastValue = numbers[8];
                for (int j = 7; j >= 0; j--)
                {
                    var current = numbers[j];
                    numbers[j] = lastValue;
                    lastValue = current;
                }

                numbers[6] += zeros;
                numbers[8] = zeros;
            }

            Console.WriteLine(numbers.Values.Sum());
        }
    }
}
