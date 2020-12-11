using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class JoltCalculator
    {
        public static int FindDifferences(int[] input)
        {
            Array.Sort(input);

            var current = 0;
            var numberOfThreeSteps = 0;
            var numberOfOneSteps = 0;
            foreach (var item in input)
            {
                var difference = item - current;

                if (difference == 3)
                {
                    numberOfThreeSteps += 1;
                }
                else if (difference == 1)
                {
                    numberOfOneSteps += 1;
                }

                current = item;
            }

            return numberOfOneSteps * (numberOfThreeSteps + 1);
        }

        public static long FindArrangements(int[] input)
        {
            var newInput = input.ToList();
            newInput.Add(input.Max() + 3);
            newInput.Sort();

            var allNodes = new Dictionary<int, Node>();

            var first = new Node(0);

            PopulateTree(first, newInput.ToArray(), allNodes);

            var result = CountNodeMultiplier(first);

            return result;
        }


        private static long CountNodeMultiplier(Node node)
        {
            if (node.Counted)
            {
                return node.Multiplier.Value;
            }

            if (node.Next.Count == 0)
            {
                node.Multiplier = 1;
                return 1;
            }

            long result = 0;
            foreach (var next in node.Next)
            {
                result += CountNodeMultiplier(next);
            }

            node.Multiplier = result;
            node.Counted = true;
            return result;
        }

        private static void CountTree(Node node, ref int current)
        {
            if (node.Next.Count == 0)
            {
                current += 1;
            }

            foreach (var next in node.Next)
            {
                CountTree(next, ref current);
            }
        }

        private static void PopulateTree(Node node, int[] input, Dictionary<int, Node> allNodes)
        {
            foreach (var value in input)
            {
                if (value <= node.Value)
                {
                    continue;
                }

                if ((value - node.Value) > 3)
                {
                    return;
                }

                if (allNodes.TryGetValue(value, out var next))
                {
                    next.TimesAccessed += 1;
                }
                else
                {
                    next = new Node(value);
                    allNodes[value] = next;
                    PopulateTree(next, input, allNodes);
                }

                node.Next.Add(next);
            }
        }
    }

    public class Node
    {
        public Node(int value)
        {
            Value = value;
            TimesAccessed = 1;
        }

        public int TimesAccessed { get; set; }

        public int Value { get; set; }
        public List<Node> Next { get; set; } = new List<Node>();

        public bool Counted { get; set; } = false;

        public long? Multiplier { get; set; }
    }
}