namespace AdventOfCode2020
{
    public class ExpenseReportValidator
    {
        private const int Target = 2020;

        public static int FindTwoEntriesThatSumToTarget(int[] numbers)
        {
            for (var start = 0; start < numbers.Length; start++)
            {
                var first = numbers[start];
                for (var index = start; index < numbers.Length; index++)
                {
                    var second = numbers[index];

                    if (first + second == Target)
                    {
                        return first * second;
                    }
                }
            }

            return -1;
        }

        public static int FindThreeEntriesThatSumToTarget(int[] numbers)
        {
            for (var firstIndex = 0; firstIndex < numbers.Length; firstIndex++)
            {
                var first = numbers[firstIndex];
                for (var secondIndex = firstIndex; secondIndex < numbers.Length; secondIndex++)
                {
                    var second = numbers[secondIndex];

                    if (first + second >= 2020)
                    {
                        continue;
                    }

                    for (var thirdIndex = secondIndex; thirdIndex < numbers.Length; thirdIndex++)
                    {
                        var third = numbers[thirdIndex];
                        if (first + second + third == Target)
                        {
                            return first * second * third;
                        }
                    }
                }
            }

            return -1;
        }
    }
}