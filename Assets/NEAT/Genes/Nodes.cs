using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NEAT
{

    namespace GENES
    {
        [System.Serializable]
        public class Node
        {
            public enum NODE{
                IN,
                OUT,
                HIDDEN
            }

            [SerializeField]
            public NODE property = NODE.IN;

            [SerializeField]
            public int nb = 0;

            public void init(NODE mode, int n)
            {
                property = mode;
                nb = n;
            }

            public Node()
            {
                property = NODE.IN;
                nb = 0;
            }

            public Node(NODE mode, int n)
            {
                init(mode, n);
            }
        }
    }
}
