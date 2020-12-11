using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class PassportValidator
    {
        public const string BirthYear = "byr";
        public const string IssueYear = "iyr";
        public const string ExpirationYear = "eyr";
        public const string Height = "hgt";
        public const string HairColor = "hcl";
        public const string EyeColor = "ecl";
        public const string PassportId = "pid";
        public const string CountryId = "cid";

        public static readonly string[] Fields = { BirthYear, IssueYear, ExpirationYear, Height, HairColor, EyeColor, PassportId, CountryId };


        //byr(Birth Year) - four digits; at least 1920 and at most 2002.
        //iyr(Issue Year) - four digits; at least 2010 and at most 2020.
        //eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
        //hgt(Height) - a number followed by either cm or in:
        //If cm, the number must be at least 150 and at most 193.
        //If in, the number must be at least 59 and at most 76.
        //hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
        //    ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
        //    pid(Passport ID) - a nine-digit number, including leading zeroes.
        //    cid(Country ID) - ignored, missing or not.

        public static readonly string[] EyeColors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        public static readonly Regex HeightRegex = new Regex("(?<value>\\d+)(?<unit>\\w+)");

        public static readonly Dictionary<string, Func<string, bool>> ValidationRules = new Dictionary<string, Func<string, bool>>
        {
            { BirthYear, value => int.TryParse(value, out var intValue) && intValue >= 1920 && intValue <= 2002 },
            { IssueYear, value => int.TryParse(value, out var intValue) && intValue >= 2010 && intValue <= 2020 },
            { ExpirationYear, value => int.TryParse(value, out var intValue) && intValue >= 2020 && intValue <= 2030 },
            { Height, value =>
            {
                var match = HeightRegex.Match(value);
                int.TryParse(match.GetGroupValue("value"), out var height);
                var unit = match.GetGroupValue("unit");

                return unit == "cm" ? height >= 150 && height <= 193 :
                    unit == "in" && (height >= 59 && height <= 76);
            } },
            { HairColor, value => Regex.IsMatch(value, "^#[0-9a-f]{6}$") },
            { EyeColor, value =>  EyeColors.Contains(value) },
            { PassportId, value =>  Regex.IsMatch(value, "^[0-9]{9}$") },
            { CountryId, value => true }
        };

        public static int ValidatePassports(Dictionary<string, string>[] passports)
        {
            return passports.Count(passport =>
            {
                var invalidFields = Fields.Where(field =>
                {
                    var found = passport.TryGetValue(field, out var value);
                    return !found || !ValidationRules[field](value);
                });

                return invalidFields.Count() == 0 ||
                       invalidFields.Count() == 1 && invalidFields.FirstOrDefault() == CountryId;
            });
        }

        public static int ValidatePassportFieldsExist(Dictionary<string, string>[] passports)
        {
            return passports.Count(passport =>
            {
                var missingFields = Fields.Where(field => !passport.ContainsKey(field));

                return missingFields.Count() == 0 ||
                       missingFields.Count() == 1 && missingFields.FirstOrDefault() == CountryId;
            });
        }
    }
}