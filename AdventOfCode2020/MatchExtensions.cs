using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class MatchExtensions
    {
        public static string GetGroupValue(this Match match, string groupName) => GetGroupValue(match, groupName, x => x);

        public static T GetGroupValue<T>(this Match match, string groupName, Func<string, T> convertFunc)
        {
            var group = match.Groups.Cast<Group>().FirstOrDefault(x => x.Name == groupName);
            return convertFunc(group.Value);
        }
    }
}