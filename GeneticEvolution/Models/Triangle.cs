using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GeneticEvolution.Models
{
    public class Triangle
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Random _r;
        public Color Color;

        public List<Point> Points;

        public Triangle(int width, int height, Random r)
        {
            _width = width;
            _height = height;
            _r = r;
            Randomize();
        }

        public void Randomize()
        {
            Points = Enumerable.Range(0, 3).Select(_ => new Point(_r.Next(0, _width), _r.Next(0, _height))).ToList();

            Color = Color.FromArgb(255, _r.Next(0, 256), _r.Next(0, 256), _r.Next(0, 256));
        }
    }
}