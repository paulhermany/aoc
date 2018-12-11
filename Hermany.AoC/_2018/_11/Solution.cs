﻿using System;
using System.Collections.Generic;
using System.IO;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._11
{
    public class Solution : ISolution
    {
        private const int Rows = 300;
        private const int Cols = 300;
        private const int MaxSquareSize = 300;

        public string Part1(params string[] input)
        {
            var serial = int.Parse(input[0]);
            
            var cells = GenerateCells(Rows, Cols, serial);

            var maxSquare = string.Empty;
            var maxPowerLevel = 0;

            for (var row = 0; row < Rows - 2; row++)
            {
                for (var col = 0; col < Cols - 2; col++)
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
            
            var cells = GenerateCells(Rows, Cols, serial);

            var maxSquare = string.Empty;
            var maxPowerLevel = 0;

            // Optimization 1: dictionary of tuples to store the power level for each (row, col, size)
            var squares = new Dictionary<ValueTuple<int, int, int>, int>();
            
            for (var size = 1; size <= MaxSquareSize; size++)
            {
                for (var row = 0; row < Rows - size; row++)
                {
                    for (var col = 0; col < Cols - size; col++)
                    {
                        var powerLevel = 0;

                        /*
                       
                        Optimization 1: store each square in a dictionary of tuples of (row, col, size)

                        For each increase in the size of the square,
                          the power level is the power level of the previous size plus
                          the power level of the last row/column of the square.
                        
                        Ex: for square size 3x3, the power level is the power of the 2x2 square (1+2+4+5) plus the row (7+8+9) plus the col (3+6)
                    
                        +-------+
                        | 1   2 | 3
                        |       |
                        | 4   5 | 6
                        +-------+
                          7   8   9
                        
                         */

                        // if the square size is 1, the power level is just the power of the single (last) cell in the square
                        if (size > 1)
                            powerLevel += squares[(col, row, size - 1)];

                        for (var dr = 0; dr < size; dr++)
                            powerLevel += cells[row + dr, col + size - 1];

                        for (var dc = 0; dc < size - 1; dc++)
                            powerLevel += cells[row + size - 1, col + dc];

                        // store the power level of the square
                        squares.Add((col, row, size), powerLevel);

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

        public int[,] GenerateCells(int rows, int cols, int serial)
        {
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

            return cells;
        }
    }
}
