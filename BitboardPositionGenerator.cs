using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tile_Slayer.Util;

namespace Tile_Slayer
{
    internal class BitboardPositionGenerator
    {
        private int N;
        private readonly bool[,] _board;
        private ulong _currentBitboard;
        public HashSet<ulong> Positions { get; } = new();

        public BitboardPositionGenerator(int n)
        {
            N = n;
            _board = new bool[n, n];
        }

        public void Generate(int x, int y, Direction? lastDirection = null)
        {
            _board[x, y] = true;

            // Mark the current position if it's the start or if we changed direction
            if (lastDirection == null)
            {
                _currentBitboard = SetBitboardCell(_currentBitboard, x, y, true);
            }

            // Move right
            if (x < N - 1 && !_board[x + 1, y])
            {
                if (lastDirection != Direction.Right)
                    _currentBitboard = SetBitboardCell(_currentBitboard, x, y, true);

                Generate(x + 1, y, Direction.Right);

                if (lastDirection != Direction.Right)
                    _currentBitboard = SetBitboardCell(_currentBitboard, x, y, false);
            }

            // Move down
            if (y < N - 1 && !_board[x, y + 1])
            {
                if (lastDirection != Direction.Down)
                    _currentBitboard = SetBitboardCell(_currentBitboard, x, y, true);

                Generate(x, y + 1, Direction.Down);

                if (lastDirection != Direction.Down)
                    _currentBitboard = SetBitboardCell(_currentBitboard, x, y, false);
            }

            // Move left
            if (x > 0 && !_board[x - 1, y])
            {
                if (lastDirection != Direction.Left)
                    _currentBitboard = SetBitboardCell(_currentBitboard, x, y, true);

                Generate(x - 1, y, Direction.Left);

                if (lastDirection != Direction.Left)
                    _currentBitboard = SetBitboardCell(_currentBitboard, x, y, false);
            }

            // Move up
            if (y > 0 && !_board[x, y - 1])
            {
                if (lastDirection != Direction.Up)
                    _currentBitboard = SetBitboardCell(_currentBitboard, x, y, true);

                Generate(x, y - 1, Direction.Up);

                if (lastDirection != Direction.Up)
                    _currentBitboard = SetBitboardCell(_currentBitboard, x, y, false);
            }

            // Always mark the endpoint
            if (_board[x - 1, y] && _board[x + 1, y] && _board[x, y - 1] && _board[x, y + 1])
            {
                _currentBitboard = SetBitboardCell(_currentBitboard, x, y, true);
                Positions.Add(_currentBitboard);
                _currentBitboard = SetBitboardCell(_currentBitboard, x, y, false);
            }

            _board[x, y] = false; // back-track
        }

        public enum Direction
        {
            Right,
            Down,
            Left,
            Up
        }
    }
}
