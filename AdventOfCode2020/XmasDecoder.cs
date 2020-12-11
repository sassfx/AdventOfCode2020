using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class XmasDecoder
    {
        public static long FindFirstInvalidValue(long[] input)
        {
            var queue = new Queue<long>();

            for (int index = 0; index < input.Length; index++)
            {
                var current = input[index];
                if (index < 25)
                {
                    queue.Enqueue(current);
                    continue;
                }

                if (!CheckSumExists(queue.ToArray(), current))
                {
                    return current;
                }

                queue.Dequeue();
                queue.Enqueue(current);
            }

            return -1;
        }

        private static bool CheckSumExists(long[] input, long target)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var newTarget = target - input[i];

                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[j] == newTarget)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static (bool IsEqual, long Value) CheckAllSumTo(long[] input, long target)
        {
            long current = 0;

            foreach (var item in input)
            {
                current += item;

                if (current > target)
                {
                    return (false, current);
                }
            }

            return (current == target, current);
        }

        public static long FindWeakness(long[] input)
        {
            var target = FindFirstInvalidValue(input);

            for (int windowSize = 2; windowSize < input.Length; windowSize++)
            {
                var queue = new Queue<long>();

                for (int index = 0; index < input.Length; index++)
                {
                    var current = input[index];
                    if (index < windowSize)
                    {
                        queue.Enqueue(current);
                        continue;
                    }

                    var array = queue.ToArray();
                    var checkAllSumTo = CheckAllSumTo(array, target);
                    if (checkAllSumTo.IsEqual)
                    {
                        return array.Min() + array.Max();
                    }

                    if (checkAllSumTo.Value > target)
                    {
                        continue;
                    }

                    queue.Dequeue();
                    queue.Enqueue(current);
                }
            }

            return -1;
        }
    }
}