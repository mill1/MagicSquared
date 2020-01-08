using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MagicSquared
{
    public class Process
    {
        private Square square;
        private List<List<Cell>> combinations;
        private List<List<Cell>> variations;
        private List<List<Cell>> variationsRow1;
        private List<List<Cell>> variationsRow2;
        private Cell a = new Cell("a");
        private Cell b = new Cell("b");
        private Cell c = new Cell("c");
        private Cell d = new Cell("d");
        private Cell e = new Cell("e");
        private Cell f = new Cell("f");
        private Cell k = new Cell("k");
        private Cell l = new Cell("l");
        private Cell m = new Cell("m");
        private Cell n = new Cell("n");
        private Cell o = new Cell("o");
        private Cell p = new Cell("p");
        private BigInteger combinationsTotal = 0;
        private long combinationsCount = 0;

        public Process()
        {
            square = new Square();

            InitializeCombinations();
            InitializeVariations();
        }

        public void Run()
        {
            Console.WriteLine("Start (int):");
            int start = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Count (int):");
            int count = Int32.Parse(Console.ReadLine());

            int[] arr = Enumerable.Range(start, count).ToArray(); // start = 101:  { 101, 102, 103.., count };

            int r = 6;
            CheckCombinations(arr, arr.Length, r);

            Console.WriteLine("done");
        }

        // The main function that handles all combinations of size r in arr[] of size n.
        private void CheckCombinations(int[] arr, int n, int r)
        {
            // A temporary array to store all combinations one by one 
            int[] data = new int[r];

            combinationsTotal = Factorial(arr.Length) / (Factorial(6) * Factorial(arr.Length - 6));

            Console.WriteLine($"{combinationsTotal.ToString("N0")} combinations will be checked. Continue (y/n)?");
            string response = Console.ReadLine();

            if (response.ToLower() == "y")
                CheckCombination(arr, data, 0, n - 1, 0, r);
        }

        /* arr[]        ---> Input Array 
           data[]       ---> Temporary array to store current combination 
           start & end  ---> Starting and Ending indexes in arr[] 
           index        ---> Current index in data[] 
           r            ---> Size of a combination to be printed       */
        private void CheckCombination(int[] arr, int[] data, int start, int end, int index, int r)
        {
            if (index == r)
            {
                //for (int j = 0; j < r; j++)
                //    Console.Write(data[j] + " ");
                //Console.WriteLine();

                combinationsCount++;
                if (combinationsCount % 1000000 == 0)
                    Console.WriteLine($"Checking {combinationsCount.ToString("N0")} of {combinationsTotal.ToString("N0")}...");  

                SetValues(data);

                foreach (List<Cell> combination in combinations)
                    if (square.IsCandidate(combination))
                        CheckCandidate(combination);

                return;
            }

            // Replace index with all possible elements. The condition "end-i+1 >=  r-index" makes sure that  
            // including one element at index will make a combination with remaining elements at remaining positions 
            for (int i = start; i <= end && end - i + 1 >= r - index; i++)
            {
                data[index] = arr[i];
                CheckCombination(arr, data, i + 1, end, index + 1, r);
            }
        }

        private BigInteger Factorial(BigInteger number)
        {
            if (number == 1)
                return 1;
            else
                return number * Factorial(number - 1);
        }

        private void CheckCandidate(List<Cell> combination)
        {
            k.Value = combination[0].Value;
            l.Value = combination[1].Value;
            m.Value = combination[2].Value;
            n.Value = combination[3].Value;
            o.Value = combination[4].Value;
            p.Value = combination[5].Value;

            // Check all variations of this combination
            foreach (var variation in variations)
            {
                int checkCount = square.CheckCandidate(variation);

                if (checkCount >= 8)
                {
                    Console.WriteLine($"checkCount: {checkCount}");
                    square.Print();
                }

                if (checkCount == 9)
                {
                    Console.WriteLine("WINNER!");                    
                    Console.ReadLine();
                }
            }
        }

        private void SetValues(int[] data)
        {
            a.Value = data[0];
            b.Value = data[1];
            c.Value = data[2];
            d.Value = data[3];
            e.Value = data[4];
            f.Value = data[5];
        }

        private void InitializeCombinations()
        {
            combinations = new List<List<Cell>>
            {
                new List<Cell>{a, b, c, d, e, f},
                new List<Cell>{a, b, d, c, e, f},
                new List<Cell>{a, b, e, c, d, f},
                new List<Cell>{a, b, f, c, d, e},
                new List<Cell>{a, c, d, b, e, f},
                new List<Cell>{a, c, e, b, d, f},
                new List<Cell>{a, c, f, b, d, e},
                new List<Cell>{a, d, e, b, c, f},
                new List<Cell>{a, d, f, b, c, e},
                new List<Cell>{a, e, f, b, c, d},
            };
        }

        private void InitializeVariations()
        {
            variations = new List<List<Cell>>();

            variationsRow1 = new List<List<Cell>>
            {
                new List<Cell>{k, l, m },
                new List<Cell>{k, m, l },
                new List<Cell>{l, k, m },
                new List<Cell>{l, m, k },
                new List<Cell>{m, k, l },
                new List<Cell>{m, l, k },
            };

            variationsRow2 = new List<List<Cell>>
            {
                new List<Cell>{n, o, p },
                new List<Cell>{n, p, o },
                new List<Cell>{o, n, p },
                new List<Cell>{o, p, n },
                new List<Cell>{p, n, o },
                new List<Cell>{p, o, n },
            };

            foreach (var VariationRow1 in variationsRow1)
                foreach (var VariationRow2 in variationsRow2)
                {
                    variations.Add(
                    new List<Cell>
                    {
                        VariationRow1[0], VariationRow1[1], VariationRow1[2],
                        VariationRow2[0], VariationRow2[1], VariationRow2[2],
                    });
                }
        }
    }
}
