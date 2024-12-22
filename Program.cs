
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

[assembly: InternalsVisibleTo("UnitTests")]

namespace Tile_Slayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Puzzle puzzle = new Puzzle((14, 7), (14, 7));

            //puzzle.PuzzleData.Add((3, 2), Puzzle.Tiles.Down);

            //puzzle.PrintPuzzle();
            //Puzzle puzzle = new Puzzle();


            ValidPositionGenerator VPG = new ValidPositionGenerator();


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