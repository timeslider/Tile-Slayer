using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("UtilTests")]

namespace Tile_Slayer
{
    internal static class Util
    {
        // Sets a new bit in a bitboard
        public static ulong SetBitboardCell(ulong bitBoard, int x, int y, bool value)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentOutOfRangeException("The x or y was too small");
            }
            if (x > 7 || y > 7)
            {
                throw new ArgumentOutOfRangeException("The x or y was too large");
            }

            int bitPosition = y * 8 + x;  // The math stays the same, just parameter names change

            if (value == true)
            {
                return bitBoard |= (1UL << bitPosition);
            }
            else
            {
                return bitBoard &= ~(1UL << bitPosition);
            }
        }

        public static bool GetBitboardCell(ulong bitBoard, int x, int y)
        {

            int bitPosition = x * 8 + y;
            return (bitBoard & (1UL << bitPosition)) != 0;
        }

        public static void PrintBitboard(ulong bitBoard, int size = 8, bool invert = false)
        {
            StringBuilder sb = new StringBuilder();

            // Prints the puzzle ID so we always know which puzzle we are displaying
            sb.Append(bitBoard + "\n");

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (invert == true)
                    {
                        if (GetBitboardCell(bitBoard, row, col) == true)
                        {
                            sb.Append("0 ");
                        }
                        else
                        {
                            sb.Append("1 ");
                        }
                    }
                    else
                    {
                        if (GetBitboardCell(bitBoard, row, col) == true)
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

        // Rotates an 8x8 bitBoard 90 degrees counter clockwise
        public static ulong Rotate90CCFast_8x8(ulong bitBoard)
        {
            bitBoard =
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0000000100000001000000010000000100000001000000010000000100000001) << 56) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0000001000000010000000100000001000000010000000100000001000000010) << 48) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0000010000000100000001000000010000000100000001000000010000000100) << 40) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0000100000001000000010000000100000001000000010000000100000001000) << 32) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0001000000010000000100000001000000010000000100000001000000010000) << 24) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0010000000100000001000000010000000100000001000000010000000100000) << 16) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0100000001000000010000000100000001000000010000000100000001000000) << 8) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b1000000010000000100000001000000010000000100000001000000010000000));

            return bitBoard;
        }

        public static ulong Rotate90CCFast_7x7(ulong bitBoard)
        {
            bitBoard =
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0000000100000001000000010000000100000001000000010000000100000001) << 42) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0000001000000010000000100000001000000010000000100000001000000010) << 35) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0000010000000100000001000000010000000100000001000000010000000100) << 28) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0000100000001000000010000000100000001000000010000000100000001000) << 21) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0001000000010000000100000001000000010000000100000001000000010000) << 14) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0010000000100000001000000010000000100000001000000010000000100000) << 7) |
                 (Bmi2.X64.ParallelBitExtract(bitBoard, 0b0100000001000000010000000100000001000000010000000100000001000000));

            return bitBoard;
        }



        public static ulong FlipHorizontally(ulong bitBoard)
        {
            bitBoard = (bitBoard << 56) |
            ((bitBoard << 40) & 0b0000000011111111000000000000000000000000000000000000000000000000) |
            ((bitBoard << 24) & 0b0000000000000000111111110000000000000000000000000000000000000000) |
            ((bitBoard << 8) & 0b0000000000000000000000001111111100000000000000000000000000000000) |
            ((bitBoard >> 8) & 0b0000000000000000000000000000000011111111000000000000000000000000) |
            ((bitBoard >> 24) & 0b0000000000000000000000000000000000000000111111110000000000000000) |
            ((bitBoard >> 40) & 0b0000000000000000000000000000000000000000000000001111111100000000) |
            ((bitBoard >> 56));

            return bitBoard;
        }

        // ########
        // # Rows #
        // ########
        private readonly static ulong row_0 =       0b0000000000000000000000000000000000000000000000000000000011111111;
        private readonly static ulong row_0_below = 0b1111111111111111111111111111111111111111111111111111111100000000;


        private readonly static ulong row_1 =       0b0000000000000000000000000000000000000000000000001111111100000000;
        private readonly static ulong row_1_below = 0b1111111111111111111111111111111111111111111111110000000000000000;


        private readonly static ulong row_2 =       0b0000000000000000000000000000000000000000111111110000000000000000;
        private readonly static ulong row_2_below = 0b1111111111111111111111111111111111111111000000000000000000000000;



        private readonly static ulong row_3 =       0b0000000000000000000000000000000011111111000000000000000000000000;
        private readonly static ulong row_3_below = 0b1111111111111111111111111111111100000000000000000000000000000000;


        private readonly static ulong row_4 =       0b0000000000000000000000001111111100000000000000000000000000000000;
        private readonly static ulong row_4_below = 0b1111111111111111111111110000000000000000000000000000000000000000;


        private readonly static ulong row_5 =       0b0000000000000000111111110000000000000000000000000000000000000000;
        private readonly static ulong row_5_below = 0b1111111111111111000000000000000000000000000000000000000000000000;

        private readonly static ulong row_6 =       0b0000000011111111000000000000000000000000000000000000000000000000;
        private readonly static ulong row_6_below = 0b1111111100000000000000000000000000000000000000000000000000000000;


        // ###########
        // # Columns #
        // ###########
        private readonly static ulong col_0 = 0b0000000100000001000000010000000100000001000000010000000100000001;
        private readonly static ulong col_0_right = 0b1111111011111110111111101111111011111110111111101111111011111110;

        private readonly static ulong col_1 = 0b0000001000000010000000100000001000000010000000100000001000000010;
        private readonly static ulong col_1_right = 0b1111110011111100111111001111110011111100111111001111110011111100;

        private readonly static ulong col_2 = 0b0000010000000100000001000000010000000100000001000000010000000100;
        private readonly static ulong col_2_right = 0b1111100011111000111110001111100011111000111110001111100011111000;

        private readonly static ulong col_3 = 0b0000100000001000000010000000100000001000000010000000100000001000;
        private readonly static ulong col_3_right = 0b1111000011110000111100001111000011110000111100001111000011110000;

        private readonly static ulong col_4 = 0b0001000000010000000100000001000000010000000100000001000000010000;
        private readonly static ulong col_4_right = 0b1110000011100000111000001110000011100000111000001110000011100000;

        private readonly static ulong col_5 = 0b0010000000100000001000000010000000100000001000000010000000100000;
        private readonly static ulong col_5_right = 0b1100000011000000110000001100000011000000110000001100000011000000;

        private readonly static ulong col_6 = 0b0100000001000000010000000100000001000000010000000100000001000000;
        private readonly static ulong col_6_right = 0b1000000010000000100000001000000010000000100000001000000010000000;

        private static readonly ulong[] rows =      [row_0, row_1, row_2, row_3, row_4, row_5, row_6];
        private static readonly ulong[] rowsBelow = [row_0_below, row_1_below, row_2_below, row_3_below, row_4_below, row_5_below, row_6_below];

        private static readonly ulong[] cols = [col_0, col_1, col_2, col_3, col_4, col_5, col_6];
        private static readonly ulong[] colsRight = [col_0_right, col_1_right, col_2_right, col_3_right, col_4_right, col_5_right, col_6_right];

        // Deletes empty rows and columns and shifts the remaining up and left
        // Note: This removes empty rows and columns BETWEEN the board too
        public static ulong ShiftAndRemoveEmpty(ulong bitBoard)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                while ((bitBoard & rows[i]) == 0 && (bitBoard & rowsBelow[i]) != 0)
                {
                    bitBoard = (bitBoard & rowsBelow[i]) >> 8 | (bitBoard & ~rowsBelow[i]);
                }
            }

            for (int i = 0; i < cols.Length; i++)
            {
                while ((bitBoard & cols[i]) == 0 && (bitBoard & colsRight[i]) != 0)
                {
                    bitBoard = (bitBoard & colsRight[i]) >> 1 | (bitBoard & ~colsRight[i]);
                }
            }

            return bitBoard;
        }

        
        // Deletes empty rows and columns and shifts the remaining up and to the left
        // Note: This only shifts the bitboard to the top left corner
        public static ulong ShiftUpAndLeft(ulong bitBoard)
        {
            while((bitBoard & 0x1010101010101ff) == 0)
            {
                bitBoard = (bitBoard & 0xffffffffffffff00) >> 9;
            }
            while ((bitBoard & 0xff) == 0)
            {
                bitBoard = (bitBoard & 0xffffffffffffff00) >> 8;
            }

            while ((bitBoard & 0x101010101010101) == 0)
            {
                bitBoard = (bitBoard & 0xfefefefefefefefe) >> 1;
            }

            return bitBoard;
        }

        /// <summary>
        /// Takes in a bitBoard and checks it against all 8 symmetries (4 rotations and their mirrors)<br></br>
        /// Outputs the smallest one
        /// </summary>
        /// <param name="bitBoard">The bitBoard to canonicalize</param>
        /// <param name="verboseLogging">If true, output inforamtion about the process to the console for debugging purposes</param>
        /// <returns>Returns the min hash from the puzzle</returns>
        public static ulong CanonicalizeBitboard(ulong bitBoard, bool verboseLogging = false)
        {
            //// This is only temporary
            //return bitBoard;
            
            ulong original = bitBoard;
            HashSet<ulong> transformations =
            [
                ShiftUpAndLeft(bitBoard)
            ];

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = FlipHorizontally(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            ulong minHash = ulong.MaxValue;
            foreach (ulong data in transformations)
            {
                minHash = Math.Min(minHash, data);
            }

            if (verboseLogging == true)
            {
                if (minHash != original)
                {
                    Console.WriteLine($"This puzzle wasn't in it's Canonical form.");
                    Console.WriteLine($"Original:");
                    PrintBitboard(original);

                    Console.WriteLine();
                    Console.WriteLine($"Canonical form found:");
                    PrintBitboard(minHash);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("This puzzle was already in its canonical form.");
                }
            }
            return minHash;
        }



        public static ulong CanonicalizeBitboard2(ulong bitBoard, bool verboseLogging = false)
        {
            HashSet<ulong> transformations =
            [
                ShiftUpAndLeft(bitBoard)
            ];

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = FlipHorizontally(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            bitBoard = Rotate90CCFast_8x8(bitBoard);
            transformations.Add(ShiftUpAndLeft(bitBoard));

            ulong minHash = ulong.MaxValue;
            foreach (ulong data in transformations)
            {
                minHash = Math.Min(minHash, data);
            }

            return minHash;
        }

        public static void TimeAction(Action action, ulong iterations)
        {
            Stopwatch sw = Stopwatch.StartNew();
            ulong i = 0UL;
            while (i < iterations)
            {
                action.Invoke();
                i++;
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }

        public static ulong ConvertPatternToBitboard(string pattern)
        {
            var bitboard = 0UL;
            var position = (x: 0, y: 0);

            // Set initial position
            bitboard = SetBitboardCell(bitboard, position.x, position.y, true);

            for (int i = 0; i < pattern.Length; i++)
            {
                position = GetNextPosition(position, pattern[i]);

                // Set bit if it's the last character or different from next character
                if (i == pattern.Length - 1 || pattern[i] != pattern[i + 1])
                {
                    bitboard = SetBitboardCell(bitboard, position.x, position.y, true);
                }
            }

            return bitboard;
        }

        private static (int x, int y) GetNextPosition((int x, int y) current, char direction)
        {
            return direction switch
            {
                'R' => (current.x + 1, current.y),
                'L' => (current.x - 1, current.y),
                'U' => (current.x, current.y - 1),
                'D' => (current.x, current.y + 1),
                _ => current
            };
        }

        public static void SavePuzzlesToBinaryFile(List<ulong> puzzles, string filePath)
        {
            if (filePath.EndsWith(".bin") == false)
            {
                filePath += ".bin";
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] sbytes = new byte[8];
                foreach (ulong puzzle in puzzles)
                {
                    sbytes = BitConverter.GetBytes(puzzle);
                    fs.Write(sbytes, 0, sbytes.Length);
                }
            }
        }


        /// <summary>
        /// If you don't include .bin, it'll add it automatically.
        /// </summary>
        /// <param name="puzzles"></param>
        /// <param name="filePath"></param>
        /// 
        public static bool CompareCanonical(HashSet<ulong> setA, HashSet<ulong> setB)
        {
            foreach(ulong a in setA)
            {
                if(setB.Contains(a)) return true;
            }
            throw new NotImplementedException();
        }

        public static HashSet<ulong> ReduceToCanonical(HashSet<ulong> set)
        {
            HashSet<ulong> result = new HashSet<ulong>();

            foreach(ulong a in set)
            {
                result.Add(CanonicalizeBitboard(a));
            }

            return result;
        }

        public static HashSet<ulong> ReduceToCanonical(HashSet<string> set, bool verboseLogging = false)
        {
            HashSet<ulong> result = new HashSet<ulong>();

            foreach (string a in set)
            {
                result.Add(CanonicalizeBitboard(ConvertPatternToBitboard(a)));

            }

            return result;
        }
    }
}
