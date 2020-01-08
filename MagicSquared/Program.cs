using System;
using System.Linq;

namespace MagicSquared
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
        }

        internal void Run()
        {
            try
            {
                Process process = new Process();
                process.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception:\r\n{e}");
            }
            finally
            {
                Console.ReadLine();
            }            
        }
    }
}
