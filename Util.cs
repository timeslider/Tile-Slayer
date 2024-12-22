using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Slayer
{
    internal class Util
    {
        public ulong SetBitboardBit(ulong bitBoard, int row, int col, bool value)
        {
            int bitPosition = row * 8 + col;
            if (value == true)
            {
                return bitBoard |= (1UL << bitPosition);
            }
            else
            {
                return bitBoard &= ~(1UL << bitPosition);
            }
        }
    }
}
