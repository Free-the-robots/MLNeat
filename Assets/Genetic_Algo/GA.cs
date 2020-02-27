using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

using GA.Selection;
using GA.Crossover;
using GA.Mutation;

namespace GA
{
    namespace Selection
    {
        public class GA_Selection<T>
        {
            public virtual void selection(List<T> population, List<float> fitnesses, List<Tuple<T, T>> crossoverPop, float crossovers, List<T> mutationPop, float mutation)
            {

            }
        }
    }
    namespace Crossover
    {
        public class GA_Crossover<T>
        {
            public virtual T crossover(T person1, T person2)
            {
                return default(T);
            }
        }
    }

    namespace Mutation
    {
        public class GA_Mutation<T>
        {
            public virtual T mutation(T person)
            {
                return default(T);
            }
        }
    }

    public class GA<T>
    {
        public List<T> results = new List<T>();
        public float elitesP = 0.05f;
        public float crossoversP = 0.8f;
        public List<T> population;
        public List<float> population_fit = null;

        protected GA_Selection<T> selectionObj = null;
        protected GA_Crossover<T> crossoverObj = null;
        protected GA_Mutation<T> mutationObj = null;

        protected int population_size = 0;
        protected IEnumerable<T> orderedPop = null;
        protected List<Tuple<T, T>> crossoverPop = null;
        protected List<T> mutationPop = null;

        public GA(List<T> init_population, float elites_count = 0.05f, float crossovers = 0.8f)
        {
            selectionObj = new Roulette<T>();
            crossoverObj = new GA_Crossover<T>();
            mutationObj = new GA_Mutation<T>();

            population = init_population;
            population_size = population.Count;
            elitesP = elites_count;
            crossoversP = crossovers;

            crossoverPop = new List<Tuple<T, T>>((int)(crossoversP * population_size));
            mutationPop = new List<T>((int)((1f - crossoversP) * population_size));

            population_fit = new List<float>(population_size);
        }

        public GA(List<T> init_population, GA_Selection<T> selectionType, GA_Crossover<T> crossoverType, GA_Mutation<T> mutationType, float elites_count = 0.05f, float crossovers = 0.8f)
        {
            selectionObj = selectionType;
            crossoverObj = crossoverType;
            mutationObj = mutationType;

            population = init_population;
            population_size = population.Count;
            elitesP = elites_count;
            crossoversP = crossovers;

            crossoverPop = new List<Tuple<T, T>>((int)(crossoversP * population_size));
            mutationPop = new List<T>((int)((1f-crossoversP) * population_size));
        }

        public virtual float fitness(T person)
        {
            return 0f;
        }

        public void crossover()
        {
            foreach(Tuple<T,T> parents in crossoverPop)
            {
                results.Add(crossover(parents.Item1, parents.Item2));
            }
        }

        public virtual T crossover(T person1, T person2)
        {
            return crossoverObj.crossover(person1, person2);
        }

        public void mutation()
        {
            foreach (T parent in mutationPop)
                results.Add(mutation(parent));
        }

        public virtual T mutation(T person)
        {
            return mutationObj.mutation(person);
        }

        public virtual void selection()
        {
            orderedPop = population.OrderByDescending(p => fitness(p));
            population_fit = orderedPop.Select(p => fitness(p)).ToList<float>();

            results.AddRange(orderedPop.Take<T>((int)(elitesP*population_size)));

            crossoverPop = new List<Tuple<T, T>>();
            mutationPop = new List<T>();
            selectionObj.selection(orderedPop.ToList<T>(), population_fit, crossoverPop, crossoversP, mutationPop, 1f - crossoversP);
        }

        public List<T> breed()
        {
            results.Clear();

            orderedPop = null;
            crossoverPop.Clear();
            mutationPop.Clear();

            selection();
            crossover();
            mutation();

            orderedPop = null;
            crossoverPop.Clear();
            mutationPop.Clear();

            return results;
        }
    }
}
