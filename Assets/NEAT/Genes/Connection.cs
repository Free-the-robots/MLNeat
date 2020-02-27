using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NEAT
{
    namespace GENES
    {
        [System.Serializable]
        public class Connection
        {
            public static int global_innov = 0;

            [SerializeField]
            public int inNode = 0;
            [SerializeField]
            public int outNode = 0;

            [SerializeField]
            public float w = 0.0f;

            public int innov { get; set; }

            public void instantiate()
            {
                innov = global_innov++;
            }

            public void init(int inN, int outN, float w)
            {
                innov = global_innov++;
                inNode = inN;
                outNode = outN;
                this.w = w;
            }

            public Connection(int inN, int outN, float w)
            {
                init(inN, outN, w);
            }
        }
    }
}
