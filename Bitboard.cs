using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private ulong BitboardValue;
        public ulong bitboardValue { get { return BitboardValue; } }

        private int SizeX;
        public int sizeX { get { return SizeX; } }
        private int SizeY;
        public int sizeY { get { return SizeY; } }



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
            
            this.BitboardValue = bitboard;
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }


        public ulong SetBitboardCell(int x, int y, bool value = true)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException($"Can't set bitboard of sizeX {SizeX} with value {x} because {x} is too small.");
            }
            if (y < 0)
            {
                throw new ArgumentOutOfRangeException($"Can't set bitboard of sizeY {SizeY} with value {y} because {y} is too small.");
            }
            if (x >= SizeX)
            {
                throw new ArgumentOutOfRangeException($"Can't set bitboard of sizeX {SizeX} with value {x} because {x} is too large");
            }
            if (y >= SizeY)
            {
                throw new ArgumentOutOfRangeException($"Can't set bitboard of sizeY {SizeY} with value {y} because {y} is too large");
            }

            int bitPosition = y * SizeX + x;

            if (value == true)
            {
                return BitboardValue |= (1UL << bitPosition);
            }
            else
            {
                return BitboardValue &= ~(1UL << bitPosition);
            }
        }

        public bool GetBitboardCell(int x, int y)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException($"Can't get bitboard of sizeX {SizeX} with value {x} because {x} is too small.");
            }
            if (y < 0)
            {
                throw new ArgumentOutOfRangeException($"Can't get bitboard of sizeY {SizeY} with value {y} because {y} is too small.");
            }
            if (x > SizeX)
            {
                throw new ArgumentOutOfRangeException($"Can't get bitboard of sizeX {SizeX} with value {x} because {x} is too large");
            }
            if (y > SizeY)
            {
                throw new ArgumentOutOfRangeException($"Can't get bitboard of sizeY {SizeY} with value {y} because {y} is too large");
            }
            int bitPosition = y * SizeX + x;
            return (BitboardValue & (1UL << bitPosition)) != 0;
        }

        public static void PrintBitboard(bool invert = false)
        {
            StringBuilder sb = new StringBuilder();

            // Prints the puzzle ID so we always know which puzzle we are displaying
            sb.Append(BitboardValue + "\n");

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
    }
}
