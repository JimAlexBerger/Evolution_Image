using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Evolution_Image
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Source_Image_Path.ShowDialog();

            Bitmap source_Image = (Bitmap)Image.FromFile(Source_Image_Path.FileName);

            Source_Imagebox.BackgroundImage = source_Image;

            Bitmap evo_Image = new Bitmap(source_Image.Width, source_Image.Height);

            for (int i = 0; i < 10; i++)
            {
                Triangle triangle = new Triangle(evo_Image);

                triangle.pos[1] = new Point(12, 12);
                triangle.r = 0;
                triangle.g = 170;
                triangle.b = 0;
                triangle.a = 0;

                using (Graphics g = Graphics.FromImage(evo_Image))
                {
                    SolidBrush drawing_Evo_Brush = new SolidBrush(triangle.color);
                    g.FillPolygon(drawing_Evo_Brush, triangle.pos);
                    //g.FillRectangle(drawing_Evo_Brush, 5, i, 5, 1);
                }
                //Thread.Sleep(10);
                Evo_Imagebox.BackgroundImage = evo_Image;
                
            }
            for (int v = 0; v < 1; v++)
            {
                Genes_Textbox.Text += ("Likeness: " + likeness(source_Image, evo_Image) + Environment.NewLine);
            }
        }

        public int likeness(Bitmap source, Bitmap compare)
        {
            if ((source.Height == compare.Height) && (source.Width == compare.Width))
            {
                int fitness = 0;
                Color clr_Source = new Color();
                Color clr_Compare = new Color();
                for (int x = 0; x < source.Width; x++)
                {
                    for (int y = 0; y < source.Height; y++)
                    {
                        clr_Source = source.GetPixel(x, y);
                        clr_Compare = compare.GetPixel(x, y);
                        fitness += Math.Abs(clr_Source.R - clr_Compare.R);
                        fitness += Math.Abs(clr_Source.G - clr_Compare.G);
                        fitness += Math.Abs(clr_Source.B - clr_Compare.B);
                    }
                }
                return fitness;
            }
            else return 0;
        }

        public double likeness_percent(Bitmap source, Bitmap compare)
        {
            int like = likeness(source, compare);

            int worst = 0;

            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    worst += 3 * 255;

                }

            }

            double fitness_Percent = 100 - ((like + 0.0) / (worst + 0.0));
            return fitness_Percent;

        }

        public class Triangle
        {
            public Point[] pos { get; set; }

            public int r;
            public int R
            {
                get { return r; }
                set
                {
                    this.r = R;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public int g;
            public int G
            {
                get { return g; }
                set
                {
                    this.g = G;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public int b;
            public int B
            {
                get { return b; }
                set
                {
                    this.b = B;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public int a;
            public int A
            {
                get { return a; }
                set
                {
                    this.a = A;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public Color color { get; set; }

            public Triangle(Bitmap Source)
            {
                Random rand = new Random();
                this.pos = new Point[3];
                this.pos[0] = new Point(rand.Next(0, Source.Width), rand.Next(0, Source.Height));
                this.pos[1] = new Point(rand.Next(0, Source.Width), rand.Next(0, Source.Height));
                this.pos[2] = new Point(rand.Next(0, Source.Width), rand.Next(0, Source.Height));
                this.r = rand.Next(0, 256);
                this.g = rand.Next(0, 256);
                this.b = rand.Next(0, 256);
                this.a = rand.Next(0, 256);
                this.color = Color.FromArgb(a, r, g, b);
            }

        }

    }
}
