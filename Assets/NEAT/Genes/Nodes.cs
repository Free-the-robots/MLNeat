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

            public enum ACTIVATION
            {
                NORMAL,
                SIN,
                COS,
                TANH,
                RECT
            }

            [SerializeField]
            public NODE property = NODE.IN;


            [SerializeField]
            public ACTIVATION activation = ACTIVATION.SIN;

            [SerializeField]
            public int nb = 0;

            public void init(NODE mode, ACTIVATION act, int n)
            {
                property = mode;
                activation = act;
                nb = n;
            }

            public Node()
            {
                property = NODE.IN;
                activation = ACTIVATION.SIN;
                nb = 0;
            }

            public Node(NODE mode, ACTIVATION act, int n)
            {
                init(mode, act, n);
            }

            public Node Clone()
            {
                Node res = new Node();
                res.property = property;
                res.activation = activation;
                res.nb = nb;
                return res;
            }
        }
    }
}
