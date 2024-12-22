using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Tile_Slayer
{
    internal class ValidPositionGenerator
    {
        //private ulong board = 0UL;

        HashSet<ulong> boards = new HashSet<ulong>();

        public ValidPositionGenerator()
        {
            int i = 0;
            while(i < 64)
            {
                boards.Add((ulong)Math.Pow(2, i));
                i++;
            }
        }

        public void GenerateValid(int maxTiles = 1)
        {
            
        }

        public ulong TryMove(Direction direction)
        {

            return 1UL;
        }

        public int GetBitCount(ulong board)
        {
            return BitOperations.PopCount(board);
        }

    }
}
