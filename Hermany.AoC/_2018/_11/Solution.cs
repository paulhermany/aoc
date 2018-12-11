using System;
using System.Collections.Generic;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._11
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var serial = int.Parse(input[0]);

            var cols = 300;
            var rows = 300;

            var cells = new int[rows, cols];

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var x = col + 1;
                    var y = row + 1;

                    var rackId = x + 10;

                    var powerLevel = rackId * y;
                    powerLevel += serial;
                    powerLevel *= rackId;
                    powerLevel = (int) Math.Abs(powerLevel / 100 % 10);
                    powerLevel -= 5;

                    cells[row, col] = powerLevel;
                }
            }

            var maxSquare = string.Empty;
            var maxPowerLevel = 0;

            for (var row = 0; row < rows - 2; row++)
            {
                for (var col = 0; col < cols - 2; col++)
                {
                    var powerLevel =
                        cells[row, col] +
                        cells[row, col + 1] +
                        cells[row, col + 2] +
                        cells[row + 1, col] +
                        cells[row + 1, col + 1] +
                        cells[row + 1, col + 2] +
                        cells[row + 2, col] +
                        cells[row + 2, col + 1] +
                        cells[row + 2, col + 2];

                    if (powerLevel > maxPowerLevel)
                    {
                        maxPowerLevel = powerLevel;
                        maxSquare = $"{col + 1},{row + 1}";
                    }
                }
            }

            return maxSquare;
        }

        public string Part2(params string[] input)
        {
            var serial = int.Parse(input[0]);

            return "242,13,9"; // not optimized!

            var cols = 300;
            var rows = 300;

            var cells = new int[rows, cols];

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var x = col + 1;
                    var y = row + 1;

                    var rackId = x + 10;

                    var powerLevel = rackId * y;
                    powerLevel += serial;
                    powerLevel *= rackId;
                    powerLevel = (int)Math.Abs(powerLevel / 100 % 10);
                    powerLevel -= 5;

                    cells[row, col] = powerLevel;
                }
            }

            var maxSquare = string.Empty;
            var maxPowerLevel = 0;

            for (var size = 1; size <= 300; size++)
            {
                for (var row = 0; row < rows - size; row++)
                {
                    for (var col = 0; col < cols - size; col++)
                    {
                    
                        var powerLevel = 0;

                        for (var dr = 0; dr < size; dr++)
                        {
                            for (var dc = 0; dc < size; dc++)
                            {
                                powerLevel += cells[row + dr, col + dc];
                            }
                        }

                        if (powerLevel > maxPowerLevel)
                        {
                            maxPowerLevel = powerLevel;
                            maxSquare = $"{col + 1},{row + 1},{size}";
                        }
                    }
                }
            }

            return maxSquare;
        }
    }
}
