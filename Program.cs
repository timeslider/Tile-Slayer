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


            Bitboard bitboard = new Bitboard(0UL, 5, 7);
            bitboard.SetBitboardCell(2, 1);

            Console.WriteLine(bitboard.bitboardValue);

            bitboard.PrintBitboard();

            //bitboard.


            //bitboard.SetBitboardCell(0, 3, true);


            //HashSet<ulong> output = new HashSet<ulong>();
            //DFS dfs = new DFS(4);
            //output = dfs.RookSearch2();

            ////foreach (ulong value in output)
            ////{
            ////    PrintBitboard(value);
            ////}

            //Console.WriteLine(output.Count);
        }


    }
}