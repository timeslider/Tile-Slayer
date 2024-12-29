using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Tile_Slayer.Util;

namespace Tile_Slayer
{
    internal class DFS
    {
        int N;

        public DFS(int n)
        {
            N = n;
        }

        /// <summary>
        /// Seaches a bitboard for unique combinations from position startX and startY
        /// </summary>
        /// <param name="inital"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <returns></returns>
        public HashSet<ulong> RookSearch(ulong inital, int startX, int startY)
        {
            Stack<ulong> possible = new Stack<ulong>();
            HashSet<ulong> visited = new HashSet<ulong>();

            ulong start = SetBitboardCell(inital, startX, startY, true);
            possible.Push(start);
            visited.Add(start);

            // Generate directions for the specific row and column
            var directions = new List<(int dx, int dy)>();
            // Add horizontal moves in startY row
            for (int d = 1; d <= 7; d++)
            {
                if (startX + d < N) directions.Add((d, 0));   // right
                if (startX - d >= 0) directions.Add((-d, 0)); // left
            }
            // Add vertical moves in startX column
            for (int d = 1; d <= 7; d++)
            {
                if (startY + d < N) directions.Add((0, d));   // down
                if (startY - d >= 0) directions.Add((0, -d)); // up
            }

            while (possible.Count > 0)
            {
                ulong vertex = possible.Pop();

                foreach (var (dx, dy) in directions)
                {
                    int newX = startX + dx;
                    int newY = startY + dy;

                    ulong nextVertex = SetBitboardCell(vertex, newX, newY, true);
                    if (!visited.Contains(nextVertex))
                    {
                        possible.Push(nextVertex);
                        visited.Add(nextVertex);
                    }
                }
            }
            return visited;
        }

        /// <summary>
        /// Since RookSearch (the original) ended up being all the possible positions (2^x if there are x cells), then we don't need to do a depth-first search
        /// We can just enumerate over all the possible cells
        /// </summary>
        /// <param name="inital"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <returns></returns>
        public HashSet<ulong> RookSearch2()
        {
            Stack<ulong> possible = new Stack<ulong>();
            HashSet<ulong> visited = new HashSet<ulong>();

            // Start will everything. If doing canonical, can probably be simplified
            for(int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    possible.Push(SetBitboardCell(0UL, i, j, true));
                    visited.Add(SetBitboardCell(0UL, i, j, true));
                }
            }

            visited = ReduceToCanonical(visited);

            float total = 0;
            while (possible.Count > 0)
            {
                ulong vertex = possible.Pop();

                foreach (var x in GetPermutations(GetMask(vertex, N), vertex, visited))
                {
                    
                    if (!visited.Contains(x))
                    {
                        possible.Push(x);
                        visited.Add(x);
                    }
                }

                Console.WriteLine($"{++total / Math.Pow(2, N * N)}\t{(float)possible.Count / ((float)N * (float)N)}");
                
            }
            return visited;
        }


    }
}
