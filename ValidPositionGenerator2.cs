using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Slayer
{
    internal class ValidPositionGenerator2
    {
        const int N = 8;

        private readonly bool[,] _board = new bool[N, N];
        private readonly StringBuilder _pattern = new();

        public HashSet<string> Positions { get; } = new();

        public void Generate(int x, int y)
        {
            _board[x, y] = true;

            // Move right
            if (x < N - 1 && !_board[x + 1, y])
            {
                if (_pattern.Length == 0 || _pattern[^1] != 'R')
                {
                    _pattern.Append('R');
                    Positions.Add(_pattern.ToString());
                    Generate(x + 1, y);
                    _pattern.Length--;
                }
                else
                {
                    Generate(x + 1, y);
                }
            }

            // Move down
            if (y < N - 1 && !_board[x, y + 1])
            {
                if (_pattern.Length == 0 || _pattern[^1] != 'D')
                {
                    _pattern.Append('D');
                    Positions.Add(_pattern.ToString());
                    Generate(x, y + 1);
                    _pattern.Length--;
                }
                else
                {
                    Generate(x, y + 1);
                }
            }

            // Move left
            if (x > 0 && !_board[x - 1, y])
            {
                if (_pattern.Length == 0 || _pattern[^1] != 'L')
                {
                    _pattern.Append('L');
                    Positions.Add(_pattern.ToString());
                    Generate(x - 1, y);
                    _pattern.Length--;
                }
                else
                {
                    Generate(x - 1, y);
                }
            }

            // Move up
            if (y > 0 && !_board[x, y - 1])
            {
                if (_pattern.Length == 0 || _pattern[^1] != 'U')
                {
                    _pattern.Append('U');
                    Positions.Add(_pattern.ToString());
                    Generate(x, y - 1);
                    _pattern.Length--;
                }
                else
                {
                    Generate(x, y - 1);
                }
            }

            _board[x, y] = false; // back-track
        }
    }
}
