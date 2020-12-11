using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class PaswordPolicyChecker
    {
        public static int ExecutePasswordPolicy()
        {
            var regex = new Regex("(?<lower>\\d+)-(?<upper>\\d+) (?<letter>\\w): (?<password>\\w+)");
            var validCount = FileParser.ParseDataFileByLine("./day2_input.txt", line => regex.Match(line))
                .Count(match =>
                {
                    var lower = MatchExtensions.GetGroupValue(match, "lower", int.Parse);
                    var upper = MatchExtensions.GetGroupValue(match, "upper", int.Parse);
                    var letter = MatchExtensions.GetGroupValue(match, "letter");
                    var password = MatchExtensions.GetGroupValue(match, "password");

                    var first = password.Substring(lower - 1, 1);
                    var second = password.Substring(upper - 1, 1);

                    if ((first == letter || second == letter) && first != second)
                    {
                        return true;
                    }

                    return false;
                });

            return validCount;
        }
    }
}