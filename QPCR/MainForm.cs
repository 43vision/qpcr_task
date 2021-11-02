using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QPCR
{
    public partial class MainForm : Form
    {
        Graphics _plateGraphics;
        int _scaleMultiplier = 50;

        string[][] wellCunks;
        string[][] arranged;
        
        public MainForm()
        {
            Plate plate = new Plate(12, 8);

            InitializeComponent();

            InitializePlate(plate);

            CreateWellChunks();
        }

        private void CreateWellChunks()
        {
            wellCunks = new string[][]
            {
                new string[] { "Sam 1, Sam2, Sam3", "Reg X, Reg Y", "1" },
                new string[] { "Sam 1, Sam3, Sam4", "Reg Y, Reg Z", "3" },
                new string[] { "Sam 1, Sam3, Sam4, Sam5, Sam6", "Reg Y, Reg Z", "3" }
            };

            ReplicatesView.Rows.Clear();

            var i = 0;
            foreach(var c in wellCunks)
            {
                var prependIndex = new string[] { i.ToString(), c[0], c[1], c[2] };
                ReplicatesView.Rows.Add(prependIndex);
                i++;
            }
        }

        private void buttonPack_Click(object sender, EventArgs e)
        {
            SquarePacking sp = new SquarePacking(wellCunks);
            var el = sp.PackElements();
            arranged = sp.ArrangePlateElements();

            Draw_Graphics(el);

            Refresh();
        }

        void InitializePlate(Plate plate)
        {
            int graphHeight = plate.Height * _scaleMultiplier;
            int graphWidth = plate.Width * _scaleMultiplier;
            // increase length and width by one
            graphHeight++;
            graphWidth++;

            // plate length and width
            Plate.Width = graphWidth;
            Plate.Height = graphHeight;

            // set new bitmap to image
            Plate.Image = new Bitmap(graphWidth, graphHeight);
            _plateGraphics = Graphics.FromImage(Plate.Image);
            _plateGraphics.TranslateTransform(0, --graphHeight);

            // clear drawing table
            _plateGraphics.Clear(Color.White);
        }

        void Draw_Graphics(List<PackedElements> elements)
        {
            Pen pencil = new Pen(Color.Black, 1);
            Brush brush = new SolidBrush(Color.Black);

            Brush brushWell = new SolidBrush(Color.Red);

            Font font = new Font("Console", 8, FontStyle.Regular);

            Rectangle element = new Rectangle();

            _plateGraphics.Clear(Color.White);

            foreach(var el in elements.Where(x => x.PlateNumber == int.Parse(PlateText.Text)))
            {
                // set element width and heigth
                element.Width = el.Width * _scaleMultiplier;
                element.Height = el.Height * _scaleMultiplier;

                // set element lower left corner coordinates
                element.X = el.X * _scaleMultiplier;
                element.Y = - (el.Y * _scaleMultiplier) - element.Height;

                // draw element at drawing_table
                _plateGraphics.DrawRectangle(pencil, element);

                // draw index value
                _plateGraphics.DrawString($"{el.Index.ToString()} {el.Reagent.ToString()} ", font, brush, new Point(element.X, element.Y));

                // richTextBox1.Text += $"Plate {el.PlateNumber}, Height: {el.Height}, Width: {el.Width}, X: { el.X}, Y: { el.Y}, Reagent: {el.Reagent}, Samples: {String.Join(",", el.Samples)}" + Environment.NewLine;
                foreach(var a in arranged)
                {
                    richTextBox1.Text += String.Join(",", a) + Environment.NewLine;
                }

                Refresh();
            }

            // release resources
            pencil.Dispose();
            brush.Dispose();
            font.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        }
    }
}
