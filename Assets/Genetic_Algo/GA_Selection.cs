using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GA.Selection
{
    public class Roulette<T> : GA_Selection<T>
    {
        public override void selection(List<T> population, List<float> fitnesses, List<Tuple<T, T>> crossoverPop, float crossovers, List<T> mutationPop, float mutation)
        {
            float sumFit = fitnesses.Sum();
            int crossoverCount = (int)(crossovers * population.Count);
            for(int i = 0; i < crossoverCount; ++i)
            {
                int index = fitnesses.FindIndex(f => (f / sumFit) >= UnityEngine.Random.value);
                if (index == -1)
                    index = 0;

                crossoverPop.Add(new Tuple<T,T>(population[index], population[(index+1)%population.Count]));
            }
            int mutationCount = (int)(mutation * population.Count);
            for (int i = 0; i < mutationCount; ++i)
            {
                int index = fitnesses.FindIndex(f => (f / sumFit) >= UnityEngine.Random.value);
                if (index == -1)
                    index = 0;

                mutationPop.Add(population[index]);
            }
        }
    }

    public class NEAT_Selection : GA_Selection<NEAT.Person>
    {
        public int IndexOfClosestSpeciation(int person, List<NEAT.Person> population)
        {
            var rest_population = population;
            NEAT.Person best_match = population.Where<NEAT.Person>((p, i) => i != person).OrderByDescending(p => population[person].similarities(p)).FirstOrDefault<NEAT.Person>();
            
            return population.IndexOf(best_match); ;
        }

        public override void selection(List<NEAT.Person> population, List<float> fitnesses, List<Tuple<NEAT.Person, NEAT.Person>> crossoverPop, float crossovers, List<NEAT.Person> mutationPop, float mutation)
        {
            float sumFit = fitnesses.Sum();
            int crossoverCount = (int)(crossovers * population.Count);
            for (int i = 0; i < crossoverCount; ++i)
            {
                int index = fitnesses.FindIndex(f => (f / sumFit) >= UnityEngine.Random.value);
                if (index == -1)
                    index = 0;

                int index2 = IndexOfClosestSpeciation(index, population);

                crossoverPop.Add(new Tuple<NEAT.Person, NEAT.Person>(population[index], population[(index2)]));
            }
            int mutationCount = (int)(mutation * population.Count);
            for (int i = 0; i < mutationCount; ++i)
            {
                int index = fitnesses.FindIndex(f => (f / sumFit) >= UnityEngine.Random.value);
                if (index == -1)
                    index = 0;

                mutationPop.Add(population[index]);
            }
        }
    }
}
