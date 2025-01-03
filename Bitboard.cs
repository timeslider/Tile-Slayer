using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("UtilTests")]

namespace Tile_Slayer
{
    /// <summary>
    /// Represents a bitboard of size x, y.
    /// </summary>
    internal class Bitboard
    {
        private ulong bitboardValue;
        public ulong BitboardValue { get { return bitboardValue; } }

        private readonly int sizeX;
        public int SizeX { get { return sizeX; } }
        
        private readonly int sizeY;
        public int SizeY { get { return sizeY; } }
        
        private bool IsSquare = false;



        public Bitboard(ulong bitboard, int sizeX)
            : this(bitboard, sizeX, sizeX)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitboard"></param>
        /// <param name="size"></param>
        /// <exception cref="ArgumentOutOfRangeException">size must be greater than zero and less than 9</exception>
        public Bitboard(ulong bitboard, int sizeX, int sizeY)
        {
            CheckBounds(bitboard, sizeX, sizeY);

            this.bitboardValue = bitboard;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            if(this.SizeX == this.SizeY)
            {
                this.IsSquare = true;
            }
        }


        public ulong SetBitboardCell(int x, int y, bool value = true)
        {
            CheckBounds(x, y);
            int bitPosition = y * SizeX + x;

            if (value == true)
            {
                return bitboardValue |= (1UL << bitPosition);
            }
            else
            {
                return bitboardValue &= ~(1UL << bitPosition);
            }
        }

        public bool GetBitboardCell(int x, int y)
        {
            CheckBounds(x, y);
            int bitPosition = y * SizeX + x;
            return (bitboardValue & (1UL << bitPosition)) != 0;
        }

        public void PrintBitboard(bool invert = false)
        {
            StringBuilder sb = new StringBuilder();

            // Prints the puzzle ID so we always know which puzzle we are displaying
            sb.Append(bitboardValue + "\n");

            for (int row = 0; row < SizeY; row++)
            {
                for (int col = 0; col < SizeX; col++)
                {
                    if (invert == true)
                    {
                        sb.Append(GetBitboardCell(col, row) ? "- " : "1 ");
                    }
                    else
                    {
                        sb.Append(GetBitboardCell(col, row) ? "1 " : "- ");
                    }
                }
                sb.Append('\n');
            }
            Console.WriteLine(sb.ToString());
        }


        public void Rotate90CCSquare()
        {
            ulong result = 0;
            for (int i = 0; i < SizeX; i++)
            {
                ulong mask = GenerateMask(SizeX, i);
                result |= Bmi2.X64.ParallelBitExtract(bitboardValue, mask) << (SizeX - 1 - i) * SizeX;
            }
            bitboardValue = result;
        }

        private ulong GenerateMask(int size, int row)
        {
            ulong mask = 0;
            for (int i = 0; i < size; i++)
            {
                mask |= 1UL << (i * size + row); }
            return mask;
        }

        public void Rotate180()
        {
            ulong result = 0;
            for (int i = 0; i < SizeX * SizeY; i++)
            {
                if ((bitboardValue & (1UL << i)) != 0)
                {
                    result |= 1UL << (SizeX * SizeY - 1 - i);
                }
            }

            bitboardValue = result;
        }

        private void CheckBounds(ulong bitboard, int sizeX, int sizeY)
        {
            if (sizeX < 1)
            {
                throw new ArgumentOutOfRangeException("sizeX was too small.");
            }
            if (sizeY < 1)
            {
                throw new ArgumentOutOfRangeException("sizeY was too small.");
            }
            if (sizeX > 8)
            {
                throw new ArgumentOutOfRangeException("sizeX was too large");
            }
            if (sizeY > 8)
            {
                throw new ArgumentOutOfRangeException("sizeY was too large.");
            }
        }

        private void CheckBounds(int x, int y)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException($"Can't get bitboard of sizeX {SizeX} with value {x} because {x} is too small.");
            }
            if (y < 0)
            {
                throw new ArgumentOutOfRangeException($"Can't get bitboard of sizeY {SizeY} with value {y} because {y} is too small.");
            }
            if (x >= SizeX)
            {
                throw new ArgumentOutOfRangeException($"Can't get bitboard of sizeX {SizeX} with value {x} because {x} is too large");
            }
            if (y >= SizeY)
            {
                throw new ArgumentOutOfRangeException($"Can't get bitboard of sizeY {SizeY} with value {y} because {y} is too large");
            }
        }
    }
}
