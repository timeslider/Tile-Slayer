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
            HashSet<ulong> found = new HashSet<ulong>();
            HashSet<ulong> Marked = new HashSet<ulong>();

            ulong start = SetBitboardCell(inital, startX, startY, true);
            possible.Push(start);
            found.Add(start);
            Marked.Add(start);

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
                    if (!Marked.Contains(nextVertex))
                    {
                        possible.Push(nextVertex);
                        found.Add(nextVertex);
                        Marked.Add(nextVertex);
                    }
                }
            }
            return found;
        }

        /// <summary>
        /// Since RookSearch (the original) ended up being all the possible positions (2^x if there are x cells), then we don't need to do a depth-first search
        /// We can just enumerate over all the possible cells
        /// </summary>
        /// <param name="inital"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <returns></returns>
        public HashSet<ulong> RookSearch2(ulong inital, int startX, int startY)
        {
            Stack<ulong> possible = new Stack<ulong>();
            HashSet<ulong> marked = new HashSet<ulong>();

            ulong start = SetBitboardCell(inital, startX, startY, true);
            possible.Push(start);
            marked.Add(start);

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
                    if (!marked.Contains(nextVertex))
                    {
                        possible.Push(nextVertex);
                        marked.Add(nextVertex);
                    }
                }
            }
            return marked;
        }
    }
}
