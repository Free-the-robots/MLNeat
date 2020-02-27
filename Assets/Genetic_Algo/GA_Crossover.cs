using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.Crossover
{
    public class Crossover<T> : GA_Crossover<T>
    {
        public override T crossover(T person1, T person2)
        {
            return base.crossover(person1, person2);
        }
    }
}
