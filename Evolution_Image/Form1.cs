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
            int numGenes = 10;
            int popSize = 32;
            int mutateChance = 5;
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

            //Evolving population until best individ fitness meets some value 5500000
            while(population[0].fitness > 55000)
            {
                //evolving Population
                population = mate(population, rand, 4);

                //Mutate population
                population = mutate_Population(population, rand, mutateChance);

                //Sorting Population            
                Array.Sort(population, delegate (Individ num1, Individ num2)
                {
                    return num1.fitness.CompareTo(num2.fitness);
                });                

                //print best image of the generation

                Application.DoEvents();
                Genes_Textbox.Text = (population[0].fitness + Environment.NewLine) + Genes_Textbox.Text;
                Evo_Imagebox.BackgroundImage = population[0].evoImage;
                Application.DoEvents();
            }
            Application.DoEvents();
            Genes_Textbox.Text = (population[0].fitness + Environment.NewLine) + Genes_Textbox.Text;
            Evo_Imagebox.BackgroundImage = population[0].evoImage;
            Application.DoEvents();
        }

        public static int likeness(Bitmap source, Bitmap compare, int skip)
        {
            if ((source.Height == compare.Height) && (source.Width == compare.Width))
            {
                int fitness = 0;
                Color clr_Source = new Color();
                Color clr_Compare = new Color();
                for (int x = 0; x < source.Width; x = x + skip)
                {
                    for (int y = 0; y < source.Height; y = y + skip)
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
            //initialize a cutoff point where all the genes of parent 1 will be transferred from before the point
            //and all the genes from parent 2 will be transferred to the kid from after the point
            int cutoff = rand.Next(0, Parent1.genes.Length);

            //Initialize kid
            Individ kid = new Individ(Parent1.source, Parent1.genes.Length, rand);
            Triangle[] kid_Genes = new Triangle[kid.genes.Length];

            for (int i = 0; i < kid.genes.Length; i++)
            {
                if (i < cutoff)
                {
                    kid_Genes[i] = Parent1.genes[i];
                }
                else kid_Genes[i] = Parent2.genes[i];
            }

            kid.Genes = kid_Genes;

            return kid;
        }

        public static Individ[] mate(Individ[] population , Random rand, int numParents)
        {
            Individ[] temp_Population = new Individ[population.Length];
            //calculate parents            
            int parent1;
            int parent2; 


            for (int i = 0; i < population.Length; i++)
            {
                parent1 = rand.Next(0, numParents);
                parent2 = parent1;

                //parent2 should not be like parent1
                while(parent2 == parent1)
                {
                    parent2 = rand.Next(0, numParents);
                }

                //if (i == 0)
                //{
                //    temp_Population[i] = population[0];
                //}
                //else
                //{
                    temp_Population[i] = procreation(population[parent1], population[parent2], rand);
                //}
            }

            return temp_Population;
        }        

        public static Individ[] mutate_Population(Individ[] population, Random rand, double chance)
        {
            //initialize temporary population
            Individ[] temp_Population = population;           

                           

            //Mutate every individual of the population
            for (int i = 0; i < temp_Population.Length; i++)
            {
                for (int j = 0; j < temp_Population[i].genes.Length; j++)
                {
                    if (rand.Next(0, 101) < chance)
                    {
                        //random R,G,B,A,X,Y picker
                        int rand_int = rand.Next(1, 7);

                        switch (rand_int)
                        {
                            case 1:
                                temp_Population[i].Genes[j].R = rand.Next(0, 256);
                                break;
                            case 2:
                                temp_Population[i].Genes[j].G = rand.Next(0, 256);
                                break;
                            case 3:
                                temp_Population[i].Genes[j].B = rand.Next(0, 256);
                                break;
                            case 4:
                                temp_Population[i].Genes[j].A = rand.Next(0, 256);
                                break;
                            case 5:
                                temp_Population[i].Genes[j].pos[rand.Next(0, 3)].X = rand.Next(0, population[0].source.Width);
                                break;
                            case 6:
                                temp_Population[i].Genes[j].pos[rand.Next(0, 3)].Y = rand.Next(0, population[0].source.Height);
                                break;

                        }
                    }

                }
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

            public int a = 255;
            public int A
            {
                get { return a; }
                set
                {
                    this.a = value;
                    this.color = Color.FromArgb(this.a, this.r, this.g, this.b);
                }
            }

            public Color color;          


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
                this.a = 255;
                this.color = Color.FromArgb(a, r, g, b);
            }

        }
        
        public class Individ
        {
            public Triangle[] genes;
            public Triangle[] Genes
            {
                get { return genes; }
                set
                {                    
                    Bitmap temp = new Bitmap(this.evoImage.Width, this.evoImage.Height);

                    this.genes = value;

                    for (int i = 0; i < this.genes.Length; i++)
                    {                       
                        using (Graphics g = Graphics.FromImage(temp))
                        {
                            SolidBrush drawing_Evo_Brush = new SolidBrush(this.genes[i].color);
                            g.FillPolygon(drawing_Evo_Brush, this.genes[i].pos);
                        }
                    }
                    this.evoImage = temp;
                    this.fitness = likeness(this.source, this.evoImage, 4);
                }
            }

            public int fitness { get; set; }

            public Bitmap evoImage { get; set; }
            public Bitmap source { get; set; }

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
                this.fitness = likeness(source, this.evoImage, 4);
                this.source = source;
            }

        }
    }
}
