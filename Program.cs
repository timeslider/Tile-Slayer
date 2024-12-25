
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static Tile_Slayer.Util;

[assembly: InternalsVisibleTo("UnitTests")]

namespace Tile_Slayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Puzzle puzzle = new Puzzle((14, 7), (14, 7));

            //puzzle.PuzzleData.Add((3, 2), Puzzle.Tiles.Down);d

            //puzzle.PrintPuzzle();
            //ulong testBoard = 0b0000000010000000000000100000100001000000000100000000010000000000;
            //PrintBitboard(testBoard);
            //PrintBitboard(CanonicalizeBitBoard(testBoard));
            //int maxTiles = 6;
            //ValidPositionGenerator VPG = new ValidPositionGenerator(maxTiles);



            //foreach (ulong x in VPG.GetVisited())
            //{
            //    if (BitOperations.PopCount(x) == maxTiles)
            //    {
            //        PrintBitboard(x);
            //    }
            //}

            //Console.WriteLine(VPG.GetVisited().Count);

            var generator = new ValidPositionGenerator2();
            generator.Generate(0, 0);
            Console.WriteLine(generator.Positions.Count);
            //foreach (string position in generator.Positions)
            //{
            //    Console.WriteLine(position);
            //}



            // The ouput should be
            // 526336
            // 524296

            //34078720
            //17301504
            //Puzzle puzzle = new Puzzle();
            //puzzle.SetPuzzleData(34078720);
            //puzzle.PrintUlong();
        }




        #region Next lexigraphical bit
        //        int i = 15;
        //        int count = 10;
        //        count += i;
        //            int v = 15;
        //        int w = 15;
        //        int t = 0;
        //            while(true)
        //            {
        //                t = (v | (v - 1)) + 1;
        //                w = t | ((((t & -t) / (v & -v)) >> 1) - 1);
        //                Console.WriteLine(Convert.ToString(w, 2).PadLeft(32, '0') + "  <--");
        //                v = w;
        //                i++;
        //            }


        //}

        //public int BSF(int x)
        //{
        //    return 0;
        //}
        #endregion
    }
}