using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static Tile_Slayer.Util;

namespace Tile_Slayer
{
    internal class ValidPositionGenerator
    {
        private readonly Queue<ulong> bitBoards = new Queue<ulong>();
        
        private readonly HashSet<ulong> visited = [];

        public HashSet<ulong> GetVisited()
        {
            return visited;
        }

        public ValidPositionGenerator(int maxTiles)
        {
            if (maxTiles < 0 || maxTiles > 64)
            {
                throw new ArgumentOutOfRangeException($"maxTiles was {maxTiles}, but it should be between 0 and 63.");
            }

            int i = 0;
            // These 10 boards are the only canonical 8x8 bitboards with 1 bit in them
            //boards.Enqueue(0b0000000000000000000000000000000000000000000000000000000000000001);
            //boards.Enqueue(0b0000000000000000000000000000000000000000000000000000000000000010);
            //boards.Enqueue(0b0000000000000000000000000000000000000000000000000000000000000100);
            //boards.Enqueue(0b0000000000000000000000000000000000000000000000000000000000001000);
            //boards.Enqueue(0b0000000000000000000000000000000000000000000000000000001000000000);
            //boards.Enqueue(0b0000000000000000000000000000000000000000000000000000010000000000);
            //boards.Enqueue(0b0000000000000000000000000000000000000000000000000000100000000000);
            //boards.Enqueue(0b0000000000000000000000000000000000000000000001000000000000000000);
            //boards.Enqueue(0b0000000000000000000000000000000000000000000010000000000000000000);
            //boards.Enqueue(0b0000000000000000000000000000000000001000000000000000000000000000);

            //// We could have done it automatically... but the above saves a few steps
            while (i < 64)
            {
                bitBoards.Enqueue(CanonicalizeBitBoard((ulong)Math.Pow(2, i)));
                i++;
            }

            GenerateValid(bitBoards, maxTiles);
        }

        private void GenerateValid(Queue<ulong> bitBoards, int maxTiles)
        {
            int i = 0;
            while (bitBoards.Count != 0)
            {
                ulong board = bitBoards.Dequeue();

                if (visited.Contains(board))
                {
                    continue;
                }

                if (BitOperations.PopCount(board) > maxTiles)
                {
                    continue;
                }

                visited.Add(board);

                GetNewPositions(board, bitBoards, visited);
                
                i++;
                if(i > 100_000)
                {
                    GC.Collect();
                    i = 0;
                }
            }
        }


        private static void GetNewPositions(ulong board, Queue<ulong> bitBoards, HashSet<ulong> visited)
        {
            // Get the active bits from board
            List<int> indices = GetIndices(board);

            int i;
            ulong canonical;

            // Loop over each one and attempt to add each one to the output
            // Could probably optomize it a bit here
            // Like if we run into a cell with a bit in it, we can exit the while loop
            // Because it'll continue when it gets to that indice
            foreach (int index in indices)
            {
                (int, int) pair = GetXYPairFromIndex(index);
                // Up
                i = 1;
                while (pair.Item2 - i > -1)
                {
                    // Check if the next bit is already set
                    if (Util.GetBitboardCell(board, pair.Item1, pair.Item2 - i) == true)
                    {
                        break;
                    }
                    canonical = CanonicalizeBitBoard(SetBitboardBit(board, pair.Item1, pair.Item2 - i, true));
                    if (!visited.Contains(canonical))
                    {
                        bitBoards.Enqueue(canonical);
                    }
                    i++;
                }
                // Left
                i = 1;
                while (pair.Item1 - i > -1)
                {
                    // Check if the next bit is already set
                    if (Util.GetBitboardCell(board, pair.Item1 - i, pair.Item2) == true)
                    {
                        break;
                    }
                    canonical = CanonicalizeBitBoard(SetBitboardBit(board, pair.Item1 - i, pair.Item2, true));
                    if (!visited.Contains(canonical))
                    {
                        bitBoards.Enqueue(canonical);
                    }
                    i++;
                }
                // Right
                i = 1;
                while (pair.Item1 + i < 8)
                {
                    // Check if the next bit is already set
                    if (Util.GetBitboardCell(board, pair.Item1 + i, pair.Item2) == true)
                    {
                        break;
                    }
                    canonical = CanonicalizeBitBoard(SetBitboardBit(board, pair.Item1 + i, pair.Item2, true));
                    if (!visited.Contains(canonical))
                    {
                        bitBoards.Enqueue(canonical);
                    }
                    i++;
                }
                // Down
                i = 1;
                while (pair.Item2 + i < 8)
                {
                    // Check if the next bit is already set
                    if (Util.GetBitboardCell(board, pair.Item1, pair.Item2 + i) == true)
                    {
                        break;
                    }
                    canonical = CanonicalizeBitBoard(SetBitboardBit(board, pair.Item1, pair.Item2 + i, true));
                    if (!visited.Contains(canonical))
                    {
                        bitBoards.Enqueue(canonical);
                    }
                    i++;
                }
            }
        }

        private static List<int> GetIndices(ulong board)
        {
            ulong temp = board;
            List<int> indices = new List<int>();
            int index = 0;
            while (board > 0)
            {
                if ((board & 1) == 1)
                {
                    indices.Add(index);
                }
                board >>= 1;
                index++;
            }
            return indices;
        }

        // Convert index to an x, y pair
        private static (int, int) GetXYPairFromIndex(int index)
        {
            return (index % 8, index / 8);
        }
    }
}
