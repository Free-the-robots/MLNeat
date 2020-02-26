using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NEAT
{
    namespace GENES
    {
        public class Connection
        {
            public static int global_innov = 0;

            public Node inNode = null;
            public Node outNode = null;
            public float w = 0.0f;
            public int innov = 0;

            public Connection()
            {
                innov = global_innov++;
            }

            public Connection(Node inN, Node outN, float w)
            {
                innov = global_innov++;
                inNode = inN;
                outNode = outN;
                this.w = w;
            }
        }
    }
}
