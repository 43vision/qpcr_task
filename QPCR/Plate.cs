namespace QPCR
{
    public class Plate
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Plate(int width, int height)
        {
            Height = height;
            Width = width;
        }
    }
}
