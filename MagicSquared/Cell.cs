using System;
using System.Collections.Generic;
using System.Text;

namespace MagicSquared
{
    public class Cell
    {
        public Cell(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
        public long Value { get; set; }
    }
}
