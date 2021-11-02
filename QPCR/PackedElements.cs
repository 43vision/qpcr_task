using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPCR
{
    public class PackedElements
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Index { get; set; }
        public int PlateNumber { get; set; }
        public string[] Samples { get; set; }
        public string Reagent { get; set; }
    }
}
