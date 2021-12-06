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
            var numbers = new long[9];
            foreach (var i in input)
                numbers[i]++;

            for (int i = 0; i < days; i++)
            {
                long zeros = numbers[0];
                for (int j = 0; j < 8; j++)
                    numbers[j] = numbers[j + 1];
                numbers[6] += zeros;
                numbers[8] = zeros;
                return 0;
            });

            Console.WriteLine(numbers.Sum());
        }
    }
}
