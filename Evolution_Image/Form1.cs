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
            //initialize source image
            Source_Image_Path.ShowDialog();
            Bitmap source_Image = (Bitmap)Image.FromFile(Source_Image_Path.FileName);
            Source_Imagebox.BackgroundImage = source_Image;

            //initialize Population            
            int numGenes = 20;
            int popSize = 5;
            Individ[] population = new Individ[popSize];

            for (int i = 0; i < popSize; i++)
            {
                population[i] = new Individ(source_Image, numGenes);
            }

            //test testing of Population
            Graph populationGraph = new Graph(population);
            while (populationGraph.history[249] > 9000000)
            {
                for (int i = 0; i < popSize; i++)
                {
                    population[i] = new Individ(source_Image, numGenes);
                }
                populationGraph.CurrPopulation = population;
                Evo_Imagebox.BackgroundImage = population[populationGraph.currMaxIndex].evoImage;
                Genes_Textbox.Text += ("Likeness: " + likeness(source_Image, population[populationGraph.currMaxIndex].evoImage) + Environment.NewLine);
            }
            Evo_Imagebox.BackgroundImage = population[populationGraph.currMaxIndex].evoImage;
            Genes_Textbox.Text += ("Likeness Final: " + likeness(source_Image, population[populationGraph.currMaxIndex].evoImage) + Environment.NewLine);


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

        public class Graph
        {
            public Bitmap graph { get; set; }

            public int[] history { get; set; }

            public Individ[] currPopulation;
            public Individ[] CurrPopulation
            {                
                set
                {
                    //Find best fitness of the population
                    int bestFitness = 0;
                    int bestIndex = 0;

                    for (int i = 0; i < value.Length; i++)
                    {
                        if ((value[i].fitness < bestFitness) || (i == 0))
                        {
                            bestFitness = value[i].fitness;
                            bestIndex = i;
                        }
                    }
                    //append best fitness to the history array
                    for (int i = 0; i < 249; i++)
                    {
                        int tempIndex = 249 - i;
                        history[tempIndex - 1] = history[tempIndex];
                    }
                    history[249] = bestFitness;

                    //graph the history array
                    Bitmap temp = new Bitmap(this.graph);
                    Point[] graphPoints = new Point[252];
                    graphPoints[250] = new Point(this.graph.Width, this.graph.Height);
                    graphPoints[251] = new Point(0, this.graph.Height);

                    for (int i = 0; i < 250; i++)
                    {
                        graphPoints[i] = new Point((45 * history[i]) / (history[0] + 1000), i);
                        using (Graphics g = Graphics.FromImage(temp))
                        {
                            SolidBrush drawing_Evo_Brush = new SolidBrush(Color.Red);
                            g.FillPolygon(drawing_Evo_Brush, graphPoints);
                        }
                    }
                    this.graph = temp;
                    this.currPopulation = value;
                    this.currMaxIndex = bestIndex;
                }
            }

            public int currMaxIndex { get; set; }

            public Graph(Individ[] population)
            {
                //Initialize the history array
                this.history = new int[250];
                //Initialize graph bitmap
                this.graph = new Bitmap(250, 45);
                //Find best fitness of the population
                int bestFitness = 0;
                int bestIndex = 0;

                for (int i = 0; i < population.Length; i++)
                {
                    if ((population[i].fitness < bestFitness) || (i == 0))
                    {
                        bestFitness = population[i].fitness;
                        bestIndex = i;
                    }
                }
                //append best fitness to the history array
                for(int i = 0; i < 249; i++)
                {
                    int tempIndex = 249 - i;
                    history[tempIndex - 1] = history[tempIndex];
                }
                history[249] = bestFitness;

                //graph the history array
                Bitmap temp = new Bitmap(this.graph);
                Point[] graphPoints = new Point[252];
                graphPoints[250] = new Point(this.graph.Width, this.graph.Height);
                graphPoints[251] = new Point(0, this.graph.Height);

                for (int i = 0; i < 250; i++)
                {
                    graphPoints[i] = new Point((45*history[i])/(history[0]+1000),i);
                    using (Graphics g = Graphics.FromImage(temp))
                    {
                        SolidBrush drawing_Evo_Brush = new SolidBrush(Color.Red);
                        g.FillPolygon(drawing_Evo_Brush, graphPoints);
                    }
                }
                this.graph = temp;
                this.currPopulation = population;
                this.currMaxIndex = bestIndex;
            }
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

            public Individ(Bitmap source, int numGenes)
            {
                //this.evoImage = new Bitmap(source.Width, source.Height);

                Random rand = new Random();

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
