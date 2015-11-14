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
            Application.DoEvents();
            //initialize source image
            Source_Image_Path.ShowDialog();
            Bitmap source_Image = (Bitmap)Image.FromFile(Source_Image_Path.FileName);
            Source_Imagebox.BackgroundImage = source_Image;
            Application.DoEvents();

            //initialize Population            
            int numGenes = 20;
            int popSize = 20;
            Random rand = new Random();
            Individ[] population = new Individ[popSize];            

            for (int i = 0; i < popSize; i++)
            {
                population[i] = new Individ(source_Image, numGenes, rand);
            }
                       
            
            //Sorting initial Population            
            Array.Sort(population, delegate (Individ num1, Individ num2)
            {
                return num1.fitness.CompareTo(num2.fitness);
            });

            //Evolving population until best individ fitness meets some value
            while(population[0].fitness > 5500000)
            {
                //evolving Population
                population = mate(population, rand);

                //Sorting Population            
                Array.Sort(population, delegate (Individ num1, Individ num2)
                {
                    return num1.fitness.CompareTo(num2.fitness);
                });

                ////Print population
                //foreach (Individ i in population)
                //{
                //    Genes_Textbox.Text += (i.fitness + Environment.NewLine);
                //}

                //print best image of the generation

                Application.DoEvents();
                Genes_Textbox.Text += (population[0].fitness + Environment.NewLine);
                Evo_Imagebox.BackgroundImage = population[0].evoImage;
                Application.DoEvents();
            }
            Application.DoEvents();
            Genes_Textbox.Text += (population[0].fitness + Environment.NewLine);
            Evo_Imagebox.BackgroundImage = population[0].evoImage;
            Application.DoEvents();
        }

        public static int likeness(Bitmap source, Bitmap compare)
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

        public static Individ procreation(Individ Parent1, Individ Parent2, Random rand)
        {
            return Parent1;
        }

        public static Individ[] mate(Individ[] population , Random rand)
        {
            Individ[] temp_Population = new Individ[population.Length];
            //calculate parents
            int numParents = Convert.ToInt32((population.Length) / 2);
            int i = 0;
            for (i = 0; i < numParents; i = i + 2)
            {
                temp_Population[i] = procreation(population[i], population[i + 1], rand);
                temp_Population[i + 1] = procreation(population[i], population[i + 1], rand);
            }
            while (i < population.Length)
            {
                temp_Population[i] = new Individ(population[0].evoImage, population[0].genes.Length,rand);
                i++;
            }


            return temp_Population;
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
                    this.r = value;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public int g;
            public int G
            {
                get { return g; }
                set
                {
                    this.g = value;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public int b;
            public int B
            {
                get { return b; }
                set
                {
                    this.b = value;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public int a;
            public int A
            {
                get { return a; }
                set
                {
                    this.a = value;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public Color color { get; set; }

            public Triangle(Bitmap Source, Random rand)
            {                
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

        public class Individ
        {
            public Triangle[] genes { get; set; }

            public int fitness { get; set; }

            public Bitmap evoImage { get; set; }

            public Individ(Bitmap source, int numGenes, Random randomGen)
            {
                //this.evoImage = new Bitmap(source.Width, source.Height);

                Random rand = randomGen;

                this.genes = new Triangle[numGenes];

                Bitmap temp = new Bitmap(source.Width, source.Height);

                for (int i = 0; i < numGenes; i++)
                {
                    this.genes[i] = new Triangle(source, rand);

                    using (Graphics g = Graphics.FromImage(temp))
                    {
                        SolidBrush drawing_Evo_Brush = new SolidBrush(this.genes[i].color);
                        g.FillPolygon(drawing_Evo_Brush, this.genes[i].pos);                        
                    }                                        
                }
                this.evoImage = temp;
                this.fitness = likeness(source, this.evoImage);
            }

        }
    }
}
