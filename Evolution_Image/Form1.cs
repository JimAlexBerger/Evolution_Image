using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GeneticEvolution.Handlers;

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
            Bitmap source_Image = new Bitmap(Source_Image_Path.FileName);
            Source_Imagebox.BackgroundImage = source_Image;
            Application.DoEvents();

            chart1.Series.Clear();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Fitness",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            chart1.Series.Add(series1);

            label1.Text = "";

            //initialize Population            
            var numGenes = 10;
            var popSize = 2048;
            var mutateChance = 0.000005D;

            var population = new Population(source_Image.Width,
                                            source_Image.Height,
                                            popSize,
                                            numGenes,
                                            mutateChance,
                                            new Random(), 
                                            source_Image);
            var generation = 0;
            //Evolving population until best individ fitness meets some value 5500000
            while (true)
            {
                var res = population.NextGeneration();

                Application.DoEvents();
                label1.Text = res.Item2.ToString();
                series1.Points.AddXY(generation++, res.Item2);
                Evo_Imagebox.BackgroundImage = res.Item1;
                Application.DoEvents();
            }
        }
    }
}
