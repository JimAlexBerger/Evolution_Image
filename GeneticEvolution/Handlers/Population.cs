using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GeneticEvolution.Models;

namespace GeneticEvolution.Handlers
{
    public class Population
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _numIndividuals;
        private readonly int _numTriangles;
        private readonly double _mutationChance;
        private readonly Random _r;
        private readonly Bitmap _target;
        private List<Individual> _individuals;
        
        public Population(int width, int height, int numIndividuals, int numTriangles, double mutationChance, Random r, Bitmap target)
        {
            _width = width;
            _height = height;
            _numIndividuals = numIndividuals;
            _numTriangles = numTriangles;
            _mutationChance = mutationChance;
            _r = r;
            _target = target;

            _individuals = Enumerable
                .Range(0, _numIndividuals)
                .Select(_ => new Individual(_width, _height, _numTriangles, _r, _target))
                .ToList();

            NextGeneration();
        }

        private void CreateNextGeneration()
        {
            var fitnessSum = _individuals.Sum(individual => individual.Fitness);

            var tempPopulation = Enumerable
                .Range(0, _numIndividuals)
                .Select(_ =>
                {
                    var p1Cutoff = _r.NextDouble() * fitnessSum;
                    var p2Cutoff = _r.NextDouble() * fitnessSum;

                    var parent1 = _individuals.First();
                    var parent2 = _individuals.First();

                    var runningSum = 0D;
                    var p1Found = false;
                    var p2Found = false;

                    foreach (var individual in _individuals)
                    {
                        runningSum += individual.Fitness;

                        if (runningSum >= p1Cutoff && !p1Found)
                        {
                            parent1 = individual;
                            p1Found = true;
                        }

                        if (runningSum >= p2Cutoff && !p2Found)
                        {
                            parent2 = individual;
                            p2Found = true;
                        }

                        if (p1Found && p2Found) break;
                    }

                    return parent1.Procreate(parent2, _r);
                });

            _individuals = tempPopulation
                    .AsParallel()
                    .ToList();
        }

        private void MutatePopulation()
        {
            foreach (var individual in _individuals)
            {
                individual.Mutate(_mutationChance);
            }
        }

        private void SortGeneration()
        {
            _individuals.Sort();
        }

        private void CalculateFitness()
        {
            Parallel.ForEach(_individuals, individual => individual.FastLikeness());
        }

        public Tuple<Bitmap,double> NextGeneration()
        {
            CreateNextGeneration();
            MutatePopulation();
            CalculateFitness();
            SortGeneration();

            var best = _individuals.First();

            return new Tuple<Bitmap, double>(best.Image, best.Fitness);
        }
    }
}