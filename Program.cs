using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static Tile_Slayer.Util;
using System.Runtime.Intrinsics.X86;

[assembly: InternalsVisibleTo("UnitTests")]

// Filepath for saving output
// C:\Users\rober\source\repos\Tile Slayer\PuzzleSolutions
// Solution from a 6x6 RRDLDDDLDRRURURDRUUL

namespace Tile_Slayer
{
    // I think v4 is working but I need to test and check
    // Unlike the others, I think it's checking against canoncial versions first which should reduce memory
    public class Program
    {
        public static void Main(string[] args)
        {
            HashSet<ulong> output = new HashSet<ulong>();
            DFS dfs = new DFS(5);
            output = dfs.RookSearch2();

            //foreach (ulong value in output)
            //{
            //    PrintBitboard(value);
            //}

            Console.WriteLine($"{output.Count}");
            
            //PrintBitboard(GetMask(0x1100000011, 5));

            Console.WriteLine();


            //Puzzle puzzle = new Puzzle();
            //puzzle.SetPuzzleData(578721382720276488UL);
            //PrintBitboard(puzzle.GetPuzzleData());

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