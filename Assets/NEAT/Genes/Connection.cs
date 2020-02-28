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

            [SerializeField]
            public bool enabled = true;

            public int innov = 0;

            public void instantiate()
            {
                innov = global_innov++;
            }

            public Connection()
            {

            }

            public void init(int inN, int outN, float w, bool enable)
            {
                innov = global_innov++;
                inNode = inN;
                outNode = outN;
                this.w = w;
                enabled = enable;
            }

            public Connection(int inN, int outN, float w, bool enable)
            {
                init(inN, outN, w, enable);
            }

            public Connection Clone()
            {
                Connection res = new Connection();
                res.inNode = inNode;
                res.outNode = outNode;
                res.w = w;
                res.innov = innov;
                res.enabled = enabled;
                return res;
            }
        }
    }
}
