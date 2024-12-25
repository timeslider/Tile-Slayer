using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tile_Slayer.Util;

namespace Tile_Slayer
{
    internal class ValidPositionGeneratorV4
    {
        private int N;
        private readonly bool[,] _board;
        public HashSet<string> Positions { get; } = new();
        private HashSet<ulong> bitboards = new();

        public ValidPositionGeneratorV4(int n)
        {
            N = n;
            _board = new bool[n, n];

        }

        public void Generate(int x, int y, string currentPattern = "")
        {
            _board[x, y] = true;

            // Add the current position if it's not the starting position
            if (!string.IsNullOrEmpty(currentPattern))
            {
                ulong canonical = CanonicalizeBitboard(ConvertPatternToBitboard(currentPattern));
                if (!bitboards.Contains(canonical))
                {
                    bitboards.Add(canonical);
                    Positions.Add(currentPattern);
                }
            }

            // Try all possible moves
            if (x < N - 1 && !_board[x + 1, y])
            {
                Generate(x + 1, y, currentPattern + "R");
            }
            if (y < N - 1 && !_board[x, y + 1])
            {
                Generate(x, y + 1, currentPattern + "D");
            }
            if (x > 0 && !_board[x - 1, y])
            {
                Generate(x - 1, y, currentPattern + "L");
            }
            if (y > 0 && !_board[x, y - 1])
            {
                Generate(x, y - 1, currentPattern + "U");
            }

            _board[x, y] = false; // backtrack
        }
    }
}
