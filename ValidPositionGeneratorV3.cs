internal class ValidPositionGeneratorV3
{
    private int N;
    private readonly bool[,] _board;
    private int _patternLength = 0;  // Track actual number of directions
    private readonly List<byte> _pattern = new();
    public HashSet<DirectionSequence> Positions { get; } = new(new DirectionSequenceComparer());

    public ValidPositionGeneratorV3(int n)
    {
        N = n;
        _board = new bool[n, n];
    }

    public class DirectionSequence
    {
        public readonly byte[] _data;
        public readonly int _length;

        public DirectionSequence(byte[] data, int length)
        {
            _data = (byte[])data.Clone();
            _length = length;
        }

        public override string ToString()
        {
            var result = new char[_length];
            for (int i = 0; i < _length; i++)
            {
                int byteIndex = i / 4;
                int bitPosition = (i % 4) * 2;
                byte direction = (byte)((_data[byteIndex] >> bitPosition) & 0b11);

                result[i] = direction switch
                {
                    0b00 => 'R',
                    0b01 => 'D',
                    0b10 => 'L',
                    0b11 => 'U',
                    _ => 'X'  // Should never happen
                };
            }
            return new string(result);
        }

        public DirectionSequence Clone() => new(_data, _length);
    }

    private class DirectionSequenceComparer : IEqualityComparer<DirectionSequence>
    {
        public bool Equals(DirectionSequence x, DirectionSequence y)
        {
            if (x._length != y._length) return false;
            for (int i = 0; i < (x._length + 3) / 4; i++)
            {
                if (x._data[i] != y._data[i]) return false;
            }
            return true;
        }

        public int GetHashCode(DirectionSequence obj)
        {
            int hash = 17;
            for (int i = 0; i < (obj._length + 3) / 4; i++)
            {
                hash = hash * 31 + obj._data[i];
            }
            return hash;
        }
    }

    private void AddDirection(Direction dir)
    {
        int byteIndex = _patternLength / 4;
        int bitPosition = (_patternLength % 4) * 2;

        if (bitPosition == 0)
        {
            _pattern.Add(0);
        }

        _pattern[byteIndex] |= (byte)((byte)dir << bitPosition);
        _patternLength++;
    }

    private void RemoveLastDirection()
    {
        _patternLength--;
        int byteIndex = _patternLength / 4;
        int bitPosition = (_patternLength % 4) * 2;

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
            Positions.Add(new DirectionSequence(_pattern.ToArray(), _patternLength));
            Generate(x + 1, y);
            RemoveLastDirection();
        }
        // Move down
        if (y < N - 1 && !_board[x, y + 1])
        {
            AddDirection(Direction.Down);
            Positions.Add(new DirectionSequence(_pattern.ToArray(), _patternLength));
            Generate(x, y + 1);
            RemoveLastDirection();
        }
        // Move left
        if (x > 0 && !_board[x - 1, y])
        {
            AddDirection(Direction.Left);
            Positions.Add(new DirectionSequence(_pattern.ToArray(), _patternLength));
            Generate(x - 1, y);
            RemoveLastDirection();
        }
        // Move up
        if (y > 0 && !_board[x, y - 1])
        {
            AddDirection(Direction.Up);
            Positions.Add(new DirectionSequence(_pattern.ToArray(), _patternLength));
            Generate(x, y - 1);
            RemoveLastDirection();
        }
        _board[x, y] = false; // back-track
    }
}