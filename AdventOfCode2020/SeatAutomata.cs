using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class SeatAutomata
    {
        public const char Seat = '#';
        public const char Floor = '.';
        public const char Empty = 'L';

        private char[][] _grid;

        public SeatAutomata(char[][] grid)
        {
            _grid = grid;
        }

        public void MutateUntilChanged()
        {
            var changed = Step();

            while (changed)
            {
                changed = Step();
            }
        }

        public int CountOccupiedSeats()
        {
            return _grid.Aggregate(0, (sum, row) => sum + row.Count(x => x == Seat));
        }

        private bool Step()
        {
            var newGrid = new char[_grid.Length][];
            var changed = false;
            for (var y = 0; y < _grid.Length; y++)
            {
                var row = _grid[y];
                var newRow = new char[row.Length];

                for (var x = 0; x < row.Length; x++)
                {
                    var current = row[x];
                    var next = current;
                    var occupiedSeatCount = GetSeatCount(x, y);
                    if (current == Empty && occupiedSeatCount == 0)
                    {
                        next = Seat;
                        changed = true;
                    }
                    else if (current == Seat && occupiedSeatCount >= 5)
                    {
                        next = Empty;
                        changed = true;
                    }

                    newRow[x] = next;
                }

                newGrid[y] = newRow;
            }

            _grid = newGrid;
            return changed;
        }

        private int GetSeatCount(int x, int y)
        {
            var positions = new[]
            {
                GetFirstSeatInDirection(x, y, p => (p.X - 1, p.Y - 1)),
                GetFirstSeatInDirection(x, y, p => (p.X, p.Y - 1)),
                GetFirstSeatInDirection(x, y, p => (p.X + 1, p.Y - 1)),
                GetFirstSeatInDirection(x, y, p => (p.X - 1, p.Y)),
                GetFirstSeatInDirection(x, y, p => (p.X + 1, p.Y)),
                GetFirstSeatInDirection(x, y, p => (p.X - 1, p.Y + 1)),
                GetFirstSeatInDirection(x, y, p => (p.X, p.Y + 1)),
                GetFirstSeatInDirection(x, y, p => (p.X + 1, p.Y + 1)),
            };

            var occupiedSeatCount = positions.Count(x => x == Seat);

            return occupiedSeatCount;
        }

        private char GetSeatAt(int x, int y)
        {
            if (y < 0 || y >= _grid.Length)
            {
                return Empty;
            }

            var row = _grid[y];

            if (x < 0 || x >= row.Length)
            {
                return Empty;
            }

            return row[x];
        }

        private char GetFirstSeatInDirection(int x, int y, Func<(int X, int Y), (int X, int Y)> increment)
        {
            var next = Floor;
            var currentPosition = (x, y);

            while (next == Floor)
            {
                var nextPosition = increment(currentPosition);
                next = GetSeatAt(nextPosition.X, nextPosition.Y);
                currentPosition = nextPosition;
            }

            return next;
        }
    }
}