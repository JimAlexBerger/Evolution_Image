using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace GeneticEvolution.Models
{
    public class Individual : IComparable<Individual>
    {
        public List<Triangle> Genes;

        private readonly int _width, _height;
        private readonly int _numGenes;
        private readonly Random _r;
        private readonly Bitmap _targetImage;

        private Bitmap _image;
        public Bitmap Image => _image ?? GetImage();

        private double _fitness;
        private bool _fitnessCalculated;
        public double Fitness => _fitnessCalculated ? _fitness : FastLikeness();

        public Individual(int width, int height, int numGenes, Random r, Bitmap targetImage)
        {
            _fitnessCalculated = false;
            _image = null;

            Genes = Enumerable.Range(0, numGenes).Select(_ => new Triangle(width, height, r)).ToList();
            _width = width;
            _height = height;
            _numGenes = numGenes;
            _r = r;
            _targetImage = targetImage.Clone(new Rectangle(Point.Empty, targetImage.Size),targetImage.PixelFormat);
        }

        private Bitmap GetImage()
        {
            var temp = new Bitmap(_width, _height, _targetImage.PixelFormat);

            using (var g = Graphics.FromImage(temp))
            {
                foreach (var triangle in Genes)
                {
                    var brush = new SolidBrush(triangle.Color);
                    g.FillPolygon(brush, triangle.Points.ToArray());
                }
            }

            _image = temp;

            return temp;
        }

        public int CompareTo(Individual other)
        {
            return other.Fitness.CompareTo(Fitness);
        }

        public Individual Procreate(Individual other, Random r)
        {
            //initialize a cutoff point where all the genes of parent 1 will be transferred from before the point
            //and all the genes from parent 2 will be transferred to the kid from after the point
            var cutoff = r.Next(0, Genes.Count);

            //Initialize kid
            var kid = new Individual(_width, _height, _numGenes,_r,_targetImage);

            for (var i = 0; i < Genes.Count; i++)
            {
                kid.Genes[i] = (i < cutoff) ? Genes[i] : other.Genes[i];
            }

            return kid;
        }

        public void Mutate(double chance)
        {
            foreach (var triangle in Genes)
            {
                if (_r.NextDouble() <= chance)
                {
                    triangle.Randomize();
                }
            }
        }

        private double CalculateLikeness(Bitmap source, Bitmap compare, int skip = 4)
        {
            if ((source.Height != compare.Height) || (source.Width != compare.Width)) return 0;

            var totalError = 0D;
            for (var x = 0; x < source.Width; x += skip)
            {
                for (var y = 0; y < source.Height; y += skip)
                {
                    var clrSource = source.GetPixel(x, y);
                    var clrCompare = compare.GetPixel(x, y);
                    totalError += CompareColors(clrSource, clrCompare) / 198608D;
                }
            }

            var avgError = totalError / (source.Width * source.Height);

            _fitness = avgError;
            _fitnessCalculated = true;

            return avgError;
        }

        public double FastLikeness(int skip = 1)
        {

            var sourceData = Image.LockBits(new Rectangle(Point.Empty, Image.Size), ImageLockMode.ReadOnly, Image.PixelFormat);
            int pixelSize = sourceData.PixelFormat == PixelFormat.Format32bppArgb ? 4 : 3;
            var padding = sourceData.Stride - (sourceData.Width * pixelSize);
            var sourceBytes = new byte[sourceData.Height * sourceData.Stride];

            var targetData = _targetImage.LockBits(new Rectangle(Point.Empty, _targetImage.Size), ImageLockMode.ReadOnly, _targetImage.PixelFormat);
            var targetBytes = new byte[targetData.Height * targetData.Stride];

            Marshal.Copy(sourceData.Scan0, sourceBytes, 0, sourceBytes.Length);
            Marshal.Copy(targetData.Scan0, targetBytes, 0, targetBytes.Length);

            var index = 0;
            var totalError = 0D;

            for (var y = 0; y < sourceData.Height; y += skip)
            {
                for (var x = 0; x < sourceData.Width; x += skip)
                {
                    var sourcePixelColor = Color.FromArgb(
                        pixelSize == 3 ? 255 : sourceBytes[index + 3],
                        sourceBytes[index + 2],
                        sourceBytes[index + 1],
                        sourceBytes[index]
                    );

                    var targetPixelColor = Color.FromArgb(
                        pixelSize == 3 ? 255 : targetBytes[index + 3],
                        targetBytes[index + 2],
                        targetBytes[index + 1],
                        targetBytes[index]
                    );

                    totalError += CompareColors(sourcePixelColor, targetPixelColor);

                    index += pixelSize*skip;
                }

                index += padding*skip;
            }

            Image.UnlockBits(sourceData);
            _targetImage.UnlockBits(targetData);

            var avgError = totalError / (Image.Width * Image.Height);

            _fitness = 1/(avgError*0.1+0.01);
            _fitnessCalculated = true;

            return _fitness;
        }

        private static double CompareColors(Color x, Color y)
        {
            return Math.Abs((x.R - y.R) + (x.B - y.B) + (x.G - y.G)) / 765D;
        }
    }
}