using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

// Important information about how puzzles are stored and how the index file works

// -- How puzzles are stored --
// The first 3 bits are the x position of a tile
// The next 3 bits are the y position of a tile
// The next 4 bits are the tile type (see Tiles enum)
// 10 bits total
// There can be a variable number of tiles in a puzzle
// To know how far to offset when reading the bits and how many bits to read we use an index file

// -- How the index file works --
// The first 6 bits tells you how many pieces are in the first puzzle
// So multiplying this by 10 gives you the total number of bits in the puzzle
// So read that many bits in 10 bit chunks

namespace Tile_Slayer
{
    public enum Tiles
    {
        None = 0,
        Hero = 1,
        One = 2,
        Two = 4,
        Three = 8,
        Up = 16,
        Down = 32,
        Left = 64,
        Right = 128,
        Spring = 256,
        Bomb = 512
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
    internal class Puzzle
    {
        #region Fields
        private int _sizeX;
        private int _sizeY;
        private HashSet<ulong> _puzzleDataMoves = new HashSet<ulong>();

        // Possible locations of where tiles might be
        private ulong PuzzleDataLocations = 0UL;
        #endregion

        #region Properties
        #endregion


        #region Constructors
        /// <summary>
        /// Empty contructor
        /// </summary>
        public Puzzle()
        {
            
        }

        /// <summary>
        /// Mostly for quick testing
        /// </summary>
        /// <param name="setPuzzleDataLocations"></param>
        public Puzzle(ulong setPuzzleDataLocations)
        {
            PuzzleDataLocations = setPuzzleDataLocations;
        }

        #endregion

        #region Data Access Methods
        public bool GetPuzzleCell(int row, int col)
        {

            int bitPosition = row * 8 + col;
            return (PuzzleDataLocations & (1UL << bitPosition)) != 0;
        }


        public void SetPuzzleCell(int row, int col, bool value)
        {
            if (row < 0 || col < 0)
            {
                throw new ArgumentOutOfRangeException("The row or col was too small");
            }
            if (row > 7 || col > 7)
            {
                throw new ArgumentOutOfRangeException("The row or colo was too large");
            }

            int bitPosition = row * 8 + col;
            if (value)
            {
                PuzzleDataLocations |= (1UL << bitPosition);
            }
            else
            {
                PuzzleDataLocations &= ~(1UL << bitPosition);
            }
        }




        public ulong GetPuzzleData()
        {
            return PuzzleDataLocations;
        }


        public void SetPuzzleData(ulong newData)
        {
            PuzzleDataLocations = newData;
        }

        #endregion

        #region Utility Methods
        public void PrintUlong(bool invert = false)
        {
            StringBuilder sb = new StringBuilder();

            // Prints the puzzle ID so we always know which puzzle we are displaying
            sb.Append(PuzzleDataLocations.ToString() + "\n");

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (invert == true)
                    {

                    }
                    else
                    {
                        if (GetPuzzleCell(row, col) == true)
                        {
                            sb.Append("1 ");
                        }
                        else
                        {
                            sb.Append("0 ");
                        }
                    }
                }
                sb.Append('\n');
            }
            Console.WriteLine(sb.ToString());
        }
        public ulong Move(Direction direction)
        {
            Puzzle newPuzzle = new Puzzle();
            newPuzzle.SetPuzzleData(PuzzleDataLocations);

            //if (direction == Direction.Up)
            //{ 
            //    newPuzzle.puzzleDataLocations
            //}

            return 1UL;
        }
        public void LoadPuzzle(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    //reader.Read
                }
            }
        }

        public void SavePuzzle(string filePath, Dictionary<(int, int), Tiles> puzzleData)
        {

        }
        #endregion












        //public void PrintPuzzle()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    for (int y= 0; y < SizeY; y++)
        //    {
        //        for (int x = 0; x < SizeX; x++)
        //        {
        //            if (PuzzleData.TryGetValue((x, y), out Tiles tile))
        //            {
        //                string symbol = tile switch
        //                {
        //                    Tiles.None => "- ",
        //                    Tiles.Hero => "H ",
        //                    Tiles.One => "1 ",
        //                    Tiles.Two => "2 ",
        //                    Tiles.Three => "3 ",
        //                    Tiles.Up => "U ",
        //                    Tiles.Down => "D ",
        //                    Tiles.Left => "L ",
        //                    Tiles.Right => "R ",
        //                    Tiles.Spring => "S ",
        //                    Tiles.Bomb => "B ",
        //                    _ => "? "
        //                };
        //                if (symbol == "? ")
        //                {
        //                    throw new Exception("Shits fucked up");
        //                }
        //                stringBuilder.Append(symbol);
        //            }
        //            else
        //            {
        //                stringBuilder.Append("- ");
        //            }
        //        }
        //        stringBuilder.Append('\n');
        //    }
        //    Console.WriteLine(stringBuilder);
        //}
    }

}
