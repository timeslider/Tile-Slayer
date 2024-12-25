using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static Tile_Slayer.Util;

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

            //DFS dfs = new DFS(8);
            //HashSet<ulong> parent = new HashSet<ulong>();

            //Console.WriteLine(ReduceToCanonical(dfs.RookSearch(0UL, 0, 0)).Count);


            //ulong startingBoard = 2385729457UL;

            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        parent.UnionWith(dfs.RookSearch(startingBoard, i, j));
            //    }
            //}

            //Console.WriteLine($"Total: {parent.Count}");
            //Console.WriteLine($"Canonical: {ReduceToCanonical(parent).Count}");

            //dfs = new DFS(8);
            //parent.Clear();

            //int total = 0;
            //int totalCanonical = 0;
            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        total += dfs.RookSearch(0UL, i, j).Count;
            //        parent.UnionWith(dfs.RookSearch(startingBoard, i, j));
            //    }
            //}

            //totalCanonical = ReduceToCanonical(parent).Count;

            //Console.WriteLine($"Total: {total}");
            //Console.WriteLine($"Canonical: {ReduceToCanonical(parent).Count}");

            //dfs = new DFS(8);
            //parent.Clear();

            //// These are the bare minimum starting nodes
            //var burnsidePositions = new (int x, int y)[]
            //{
            //    (0, 0),
            //    (0, 1),
            //    (0, 2),
            //    (0, 3),
            //    (1, 1),
            //    (1, 2),
            //    (1, 3),
            //    (2, 2),
            //    (2, 3),
            //    (3, 3),
            //};

            //foreach ((int x, int y) position in burnsidePositions)
            //{
            //    parent.UnionWith(dfs.RookSearch(startingBoard, position.x, position.y));
            //}

            //Console.WriteLine($"Canonical: {ReduceToCanonical(parent).Count}");
            Action action = () => {
                Util.ShiftUpAndLeft(0x8000000000000000);
            };

            TimeAction(action, 1_000_000_000);


            Action action2 = () =>
            {
                Util.ShiftUpAndLeft2(0x8000000000000000);
            };

            TimeAction(action2, 1_000_000_000);




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