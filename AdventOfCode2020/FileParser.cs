using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    public static class FileParser
    {
        public static IEnumerable<T> ParseDataFileByLine<T>(string relativeFilePath, Func<string, T> lineFunction)
        {
            using var fileStream = new FileStream(relativeFilePath, FileMode.Open);
            using var streamReader = new StreamReader(fileStream);

            while (!streamReader.EndOfStream)
            {
                var input = streamReader.ReadLine();
                yield return lineFunction(input);
            }
        }

        public static IEnumerable<T> ParseDataFileByEmptyLine<T>(string relativeFilePath, Func<string, T> lineFunction, Func<T, T, T> mergeFunction) where T : class
        {
            using var fileStream = new FileStream(relativeFilePath, FileMode.Open);
            using var streamReader = new StreamReader(fileStream);

            T current = null;
            while (!streamReader.EndOfStream)
            {
                var input = streamReader.ReadLine();

                if (input.Trim() == "")
                {
                    yield return current;
                    current = null;
                    continue;
                }
                
                var item = lineFunction(input);

                if (current == null)
                {
                    current = item;
                }
                else
                {
                    current = mergeFunction(current, item);
                }
            }

            if (current != null)
            {
                yield return current;
            }
        }
    }
}