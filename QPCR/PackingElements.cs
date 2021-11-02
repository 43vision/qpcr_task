
namespace QPCR
{
    public class PackingElements
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Area { get { return Height * Width; } }
        public int FitFactor { get; set; }
        public int sortOrder { get; set; }
        public int Index { get; set; }
        public string[] Samples { get; set; }
        public string Reagent { get; set; }
    }
}
