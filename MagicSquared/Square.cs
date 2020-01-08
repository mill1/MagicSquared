using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MagicSquared
{
    public class Square
    {
        public long[,] Values;

        public bool IsCandidate(List<Cell> cells)
        {
            DetermineValues(cells);

            // Compare sum values first row to second row
            bool result = Sum(Values[0, 0], Values[0, 1], Values[0, 2]) ==
                          Sum(Values[1, 0], Values[1, 1], Values[1, 2]);

            return result;
        }

        private void DetermineValues(List<Cell> cells)
        {
            Values = new long[3, 3]
                     {
                        {cells[0].Value, cells[1].Value, cells[2].Value},
                        {cells[3].Value, cells[4].Value, cells[5].Value},
                        {-1, -1, -1}
                     };

            SquareValues();
        }

        public int CheckCandidate(List<Cell> cells) 
        {
            DetermineValues(cells);

            long sum = Sum(Values[0, 0], Values[0, 1], Values[0, 2]);

            // Calculate sum values in row 3
            Values[2, 0] = sum - Values[0, 2] - Values[1, 1]; // DIAGONALS
            Values[2, 1] = sum - Values[0, 1] - Values[1, 1];
            Values[2, 2] = sum - Values[0, 0] - Values[1, 1]; // DIAGONALS

            int count = 5; // correct sums: 2 rows + 2 diagonals + 1 (middle) column

            // Check sum of row 3
            if (Sum(Values[2, 0], Values[2, 1], Values[2, 2]) == sum)
                count++;

            // Check sum of values in Values[2, 0] and Values[2, 2] 
            if (Sum(Values[0, 0], Values[1, 0], Values[2, 0]) == sum)
                count++;
            if (Sum(Values[0, 2], Values[1, 2], Values[2, 2]) == sum)
                count++;

            if (count < 6) // was 8 (=alle sums OK)
                return count;

            // Check duplicates
            List<long> list = cells.Select(c => c.Value * c.Value).ToList();
            list.Add(Values[2, 0]);
            list.Add(Values[2, 1]);
            list.Add(Values[2, 2]);

            if (list.Count != list.Distinct().Count()) // Solution contains duplicates
                return 0;

            // Check if the values in row 3 are squares
            if (IsSquare(Values[2, 0]))
                count++;
            if (IsSquare(Values[2, 1]))
                count++;
            if (IsSquare(Values[2, 2]))
                count++;

            return count;
        }

        private bool IsSquare(long value)
        {
            if (value < 0)
                return false;
            else
            {
                double result = Math.Sqrt(value);
                return result % 1 == 0;
            }
        }

        private void SquareValues()
        {
            for (byte y = 0; y < 2; y++)  // 2!
                for (byte x = 0; x < 3; x++)
                    Values[y, x] = Values[y, x] * Values[y, x];
        }

        private long Sum(long value1, long value2, long value3)
        {
            return value1 + value2 + value3;
        }

        public void Print()
        {
            Console.WriteLine($"{Values[0,0]} {Values[0, 1]} {Values[0,2]}");
            Console.WriteLine($"{Values[1,0]} {Values[1, 1]} {Values[1,2]}");
            Console.WriteLine($"{Values[2,0]} {Values[2, 1]} {Values[2,2]}");
        }
    }
}
