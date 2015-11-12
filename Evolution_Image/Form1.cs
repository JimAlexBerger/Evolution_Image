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

            Bitmap evo_Image = new Bitmap(source_Image.Width,source_Image.Height);

            for (int i = 0; i < 10; i++)
            {
                Triangle triangle = new Triangle(evo_Image);

                using (Graphics g = Graphics.FromImage(evo_Image))
                {
                    SolidBrush drawing_Evo_Brush = new SolidBrush(triangle.color);
                    g.FillPolygon(drawing_Evo_Brush, triangle.pos);
                    //g.FillRectangle(drawing_Evo_Brush, 5, i, 5, 1);
                }
                Thread.Sleep(10);
                Evo_Imagebox.BackgroundImage = evo_Image;
                
            }                 
        }

        public class Triangle
        {
            public Point[] pos { get; set; }
            public int r { get; set; }
            public int g { get; set; }
            public int b { get; set; }
            public int a { get; set; }
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
