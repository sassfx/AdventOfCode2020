using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = FileParser.ParseDataFileByLine("./day11_input.txt",
                line => line.ToArray()).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var automata = new SeatAutomata(input);
            automata.MutateUntilChanged();
            var answer = automata.CountOccupiedSeats();

            stopWatch.Stop();

            Console.WriteLine($"Solution: {answer}");
            Console.WriteLine($"Took {stopWatch.ElapsedMilliseconds}ms");
        }
    }
}