using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class BagHandler
    {
        public class Bag
        {
            public Bag(string name, Dictionary<string, int> contents)
            {
                Name = name;
                Contents = contents;
            }

            public string Name { get; }
            public Dictionary<string, int> Contents { get; }
        }

        private Dictionary<string, Bag> allBags = new Dictionary<string, Bag>();

        private Regex lineRegex = new Regex("(?<container>.+) bags contain (?<content>.+)");
        private Regex contentsRegex = new Regex("(?<amount>\\d)(?<type>.+)bags?");

        public Bag AddBag(string bagLine)
        {
            var match = lineRegex.Match(bagLine);

            var bagName = match.GetGroupValue("container").Trim();
            var contentsGroup = match.GetGroupValue("content");

            var contents = new Dictionary<string, int>();
            foreach (var item in contentsGroup.Split(","))
            {

                if (contentsRegex.IsMatch(item))
                {
                    var contentsMatch = contentsRegex.Match(item);
                    var type = contentsMatch.GetGroupValue("type").Trim();
                    var amount = contentsMatch.GetGroupValue("amount", int.Parse);

                    contents.Add(type, amount);
                }
            }

            var bag = new Bag(bagName, contents);
            allBags.Add(bagName, bag);

            return bag;
        }

        public int FindBagsThatCanContainBag(string bagName)
        {
            var count = 0;
            foreach (var bag in allBags.Values)
            {
                if (CanBagContain(bag, bagName))
                {
                    count += 1;
                }
            }

            return count;
        }

        private bool CanBagContain(Bag bag, string bagToFind)
        {
            foreach (var innerBagName in bag.Contents.Keys)
            {
                if (innerBagName == bagToFind)
                {
                    return true;
                }

                var innerBag = allBags[innerBagName];
                if (CanBagContain(innerBag, bagToFind))
                {
                    return true;
                }
            }

            return false;
        }

        public int CountBagsInsideBag(string bagName)
        {
            var bag = allBags[bagName];
            var count = 0;

            foreach (var item in bag.Contents)
            {
                count += item.Value;
                count += item.Value * CountBagsInsideBag(item.Key);
            }

            return count;
        }
    }
}