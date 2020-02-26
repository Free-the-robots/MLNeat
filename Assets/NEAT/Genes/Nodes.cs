using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NEAT
{

    namespace GENES
    {
        public class Node
        {
            public enum NODE{
                IN,
                OUT,
                HIDDEN
            }

            public NODE property;
            public int nb = 0;

            public Node(NODE mode, int n)
            {
                property = mode;
                nb = n;
            }
        }
    }
}
