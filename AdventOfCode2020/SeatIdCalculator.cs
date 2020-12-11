using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class SeatIdCalculator
    {
        public static int FindHighestSeatId(string[] input)
        {
            return input.Select(CalculateSeatId).Max();
        }

        public static int FindMySeat(string[] input)
        {
            var presentSeats = input.Select(CalculateSeatId).ToHashSet();

            for (var row = 0; row < 128; row++)
            {
                for (var column = 0; column < 8; column++)
                {
                    var seatId = row * 8 + column;
                    if (!presentSeats.Contains(seatId) && presentSeats.Contains(seatId + 1) &&
                        presentSeats.Contains(seatId - 1))
                    {
                        return seatId;
                    }
                }
            }

            return -1;
        }

        private static int CalculateSeatId(string input)
        {
            var lowerRow = 0;
            var upperRow = 127;

            var lowerColum = 0;
            var upperColumn = 7;

            foreach (var item in input)
            {
                if (item == 'F')
                {
                    upperRow = (upperRow + lowerRow) / 2;
                }
                else if (item == 'B')
                {
                    lowerRow = (upperRow + lowerRow) / 2 + 1;
                }
                else if (item == 'L')
                {
                    upperColumn = (upperColumn + lowerColum) / 2;
                }
                else if (item == 'R')
                {
                    lowerColum = (upperColumn + lowerColum) / 2 + 1;
                }
            }

            if (!(lowerRow == upperRow && lowerColum == upperColumn))
            {
                throw new Exception();
            }

            return lowerRow * 8 + lowerColum;
        }
    }
}