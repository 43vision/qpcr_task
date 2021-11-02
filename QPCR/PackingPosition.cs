namespace QPCR
{
    public class PackingPosition
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Area { get {  return Height * Width; } }
    }
}
