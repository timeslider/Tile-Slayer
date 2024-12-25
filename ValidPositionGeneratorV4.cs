using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Slayer
{
    internal class ValidPositionGeneratorV4
    {
        private int N;  // 8x8 grid
        private readonly bool[,] _board;
        private int _patternLength = 0;
        private readonly List<byte> _pattern = new();

        public HashSet<DirectionSequence> Positions { get; } = new(new DirectionSequenceComparer());
        public ValidPositionGeneratorV4(int n)
        {
            N = n;
            _board = new bool[n, n];
        }

        public class DirectionSequence
        {
            public readonly byte[] _data;

            public DirectionSequence(byte[] data)
            {
                _data = (byte[])data.Clone();
            }

            public int GetLength()
            {
                // Length is stored in the first 6 bits
                return _data[0] >> 2;
            }

            public override string ToString()
            {
                int length = GetLength();
                var result = new char[length];

                // Handle first byte specially (contains length and potentially 1 direction)
                if (length > 0)
                {
                    byte firstDirection = (byte)(_data[0] & 0b11);
                    result[0] = firstDirection switch
                    {
                        0b00 => 'R',
                        0b01 => 'D',
                        0b10 => 'L',
                        0b11 => 'U',
                        _ => 'X'
                    };
                }

                // Handle remaining directions
                for (int i = 1; i < length; i++)
                {
                    int byteIndex = (i + 3) / 4;  // +3 accounts for length bits
                    int bitPosition = ((i + 3) % 4) * 2;  // +3 accounts for length bits
                    byte direction = (byte)((_data[byteIndex] >> bitPosition) & 0b11);

                    result[i] = direction switch
                    {
                        0b00 => 'R',
                        0b01 => 'D',
                        0b10 => 'L',
                        0b11 => 'U',
                        _ => 'X'
                    };
                }
                return new string(result);
            }

            public DirectionSequence Clone() => new(_data);
        }

        private class DirectionSequenceComparer : IEqualityComparer<DirectionSequence>
        {
            public bool Equals(DirectionSequence x, DirectionSequence y)
            {
                int length = x.GetLength();
                if (length != y.GetLength()) return false;

                for (int i = 0; i < (length + 3) / 4; i++)
                {
                    if (x._data[i] != y._data[i]) return false;
                }
                return true;
            }

            public int GetHashCode(DirectionSequence obj)
            {
                int length = obj.GetLength();
                int hash = 17;
                for (int i = 0; i < (length + 3) / 4; i++)
                {
                    hash = hash * 31 + obj._data[i];
                }
                return hash;
            }
        }

        private void AddDirection(Direction dir)
        {
            if (_patternLength == 0)
            {
                // First direction - initialize first byte with length (0) and direction
                _pattern.Add((byte)dir);
            }
            else
            {
                int byteIndex = (_patternLength + 3) / 4;  // +3 accounts for length bits
                int bitPosition = ((_patternLength + 3) % 4) * 2;  // +3 accounts for length bits

                if (bitPosition == 0)
                {
                    _pattern.Add(0);
                }

                _pattern[byteIndex] |= (byte)((byte)dir << bitPosition);
            }

            // Update length in first byte (stored in upper 6 bits)
            _patternLength++;
            _pattern[0] = (byte)((_patternLength << 2) | (_pattern[0] & 0b11));
        }

        private void RemoveLastDirection()
        {
            if (_patternLength <= 0) return;

            _patternLength--;

            if (_patternLength == 0)
            {
                _pattern[0] = 0;
                while (_pattern.Count > 1)
                {
                    _pattern.RemoveAt(_pattern.Count - 1);
                }
                return;
            }

            // Update length in first byte
            _pattern[0] = (byte)((_patternLength << 2) | (_pattern[0] & 0b11));

            // Clear the bits of the removed direction
            int byteIndex = (_patternLength + 3) / 4;
            int bitPosition = ((_patternLength + 3) % 4) * 2;

            _pattern[byteIndex] &= (byte)~(0b11 << bitPosition);

            if (bitPosition == 0 && byteIndex < _pattern.Count - 1)
            {
                _pattern.RemoveAt(_pattern.Count - 1);
            }
        }

        private enum Direction : byte
        {
            Right = 0b00,
            Down = 0b01,
            Left = 0b10,
            Up = 0b11
        }

        public void Generate(int x, int y)
        {
            _board[x, y] = true;

            // Move right
            if (x < N - 1 && !_board[x + 1, y])
            {
                AddDirection(Direction.Right);
                Positions.Add(new DirectionSequence(_pattern.ToArray()));
                Generate(x + 1, y);
                RemoveLastDirection();
            }
            // Move down
            if (y < N - 1 && !_board[x, y + 1])
            {
                AddDirection(Direction.Down);
                Positions.Add(new DirectionSequence(_pattern.ToArray()));
                Generate(x, y + 1);
                RemoveLastDirection();
            }
            // Move left
            if (x > 0 && !_board[x - 1, y])
            {
                AddDirection(Direction.Left);
                Positions.Add(new DirectionSequence(_pattern.ToArray()));
                Generate(x - 1, y);
                RemoveLastDirection();
            }
            // Move up
            if (y > 0 && !_board[x, y - 1])
            {
                AddDirection(Direction.Up);
                Positions.Add(new DirectionSequence(_pattern.ToArray()));
                Generate(x, y - 1);
                RemoveLastDirection();
            }
            _board[x, y] = false; // back-track
        }
    }
}
