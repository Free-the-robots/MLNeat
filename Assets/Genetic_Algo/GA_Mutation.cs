using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.Mutation
{
    public class Mutation<T> : GA_Mutation<T>
    {
        public override T mutation(T person)
        {
            return base.mutation(person);
        }
    }
}
