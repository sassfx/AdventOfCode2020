namespace AdventOfCode2020
{
    public class TreeCounter
    {
        public static int CountTreesPartOne(char[][] grid)
        {
            return TreeCount(grid, 1, 3);
        }

        public static int CountTreesPartTwo(char[][] grid)
        {
            var first = TreeCount(grid, 1, 1);
            var second = TreeCount(grid, 3, 1);
            var third = TreeCount(grid, 5, 1);
            var fourth = TreeCount(grid, 7, 1);
            var fifth = TreeCount(grid, 1, 2);


            return first * second * third * fourth * fifth;
        }

        private static int TreeCount(char[][] grid, int xDelta, int yDelta)
        {
            var xPos = 0;
            var treeCount = 0;

            for (var yPos = 0; yPos < grid.Length; yPos += yDelta)
            {
                var row = grid[yPos];

                var current = row[xPos];
                if (current == '#')
                {
                    treeCount += 1;
                }

                xPos = (xPos + xDelta) % row.Length;
            }

            return treeCount;
        }
    }
}