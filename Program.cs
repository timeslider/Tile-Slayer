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
            //for (ulong i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(BitOperations.PopCount(i));
            //}
            int n = 5;
            DFS dfs = new DFS(n);

            HashSet<ulong> parent = new HashSet<ulong>();
            HashSet<ulong> grandparent = new HashSet<ulong>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    parent.UnionWith(dfs.RookSearch(0UL, i, j));

                }
            }


            // Print all the children in parents
            //foreach(var child in parent)
            //{
            //    PrintBitboard(child);
            //    Thread.Sleep(50);
            //}

            //grandparent.UnionWith(parent);
            for (int i = 0; i < n; i++)
            {
                foreach (ulong child in parent)
                {
                    grandparent.Add(child);
                    var xy_pairs = GetValues(child);

                    foreach (var pair in xy_pairs)
                    {
                        grandparent.UnionWith(ReduceToCanonical(dfs.RookSearch(child, pair.x, pair.y)));
                    }
                }
                parent.UnionWith(ReduceToCanonical(grandparent));

            }



            grandparent = ReduceToCanonical(grandparent);
            Console.WriteLine(grandparent.Max<ulong>());
            Console.WriteLine(grandparent.Count);
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