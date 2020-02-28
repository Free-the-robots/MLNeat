using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NN
{

    public class Node
    {
        public List<Node> inNodes = null;
        public List<Node> outNodes = null;
        public List<float> w = new List<float>();
        public int nb = 0;

        public virtual float activation(float x)
        {
            //return 1f/(1f+Mathf.Exp(-x));
            //return (1f - Mathf.Exp(-2f*x)) / (1f + Mathf.Exp(-2f*x));
            if (x > 1f)
                x = 1f;
            if (x < -1f)
                x = -1f;
            return x;
            //return (float)System.Math.Tanh(x);
        }

        public float evaluateBack(List<float> inputs)
        {
            float sum = 0f;
            if (inNodes == null)
            {
                if (inputs.Count > nb)
                    return activation(inputs[nb]);
                else
                    return 0f;
            }

            for(int i = 0; i < inNodes.Count; ++i)
            {
                sum += w[i] * inNodes[i].evaluateBack(inputs);
            }
            return activation(sum);
        }
    }
    public class Rectifier : Node
    {
        public override float activation(float x)
        {
            if (x >= 0f)
                x = 1f;
            if (x < 0f)
                x = -1f;
            return x;
        }
    }
    public class SinNode : Node
    {
        public override float activation(float x)
        {
            return Mathf.Sin(x);
        }
    }
    public class CosNode : Node
    {
        public override float activation(float x)
        {
            return Mathf.Cos(x);
        }
    }
    public class TanhNode : Node
    {
        public override float activation(float x)
        {
            return (float)System.Math.Tanh(x);
        }
    }
    public class GaussianNode : Node
    {
        public override float activation(float x)
        {
            return 1f/Mathf.Sqrt(0.5f*Mathf.PI)*Mathf.Exp(-(x*x));
        }
    }
    public class BipolarNode : Node
    {
        public override float activation(float x)
        {
            return (1f - Mathf.Exp(-x))/ (1f + Mathf.Exp(-x));
        }
    }
    public class TanNode : Node
    {
        public override float activation(float x)
        {
            return Mathf.Tan(x);
        }
    }

    public class Net
    {
        public List<Node> inNodes = new List<Node>();
        public List<Node> outNodes = new List<Node>();
        public List<Node> hidNodes = new List<Node>();

        /*public Net(List<List<float>> layersW)
        {
            if (layersW.Count < 2)
                Debug.LogError("Not enough layers");

            for(int i = 0; i < layersW[layersW.Count- 1].Count; ++i)
            {
                Node outNode = new Node();
                outNode.w.Add(layersW[layersW.Count - 2][i]);
                outNode.nb = layersW.Count ;
                outNodes.Add(outNode);
            }
            for (int i = layersW.Count - 2; i > 1; ++i)
            {
                for (int j = 0; j > 1; ++i)
                {
                    Node hidNode = new Node();
                hidNode.w.Add(layersW[layersW.Count - 2][i]);
            }
        }*/

        public Net(List<NEAT.GENES.Node> nodes, List<NEAT.GENES.Connection> connections)
        {
            Dictionary<int,Node> nodes_NN = new Dictionary<int,Node>();
            //int nIndex = 0;
            foreach(NEAT.GENES.Node node in nodes)
            {
                Node a = null;
                switch (node.activation)
                {
                    case NEAT.GENES.Node.ACTIVATION.RAMP:
                        a = new Node();
                        break;
                    case NEAT.GENES.Node.ACTIVATION.COS:
                        a = new CosNode();
                        break;
                    case NEAT.GENES.Node.ACTIVATION.SIN:
                        a = new SinNode();
                        break;
                    case NEAT.GENES.Node.ACTIVATION.TANH:
                        a = new TanhNode();
                        break;
                    case NEAT.GENES.Node.ACTIVATION.RECT:
                        a = new Rectifier();
                        break;
                    case NEAT.GENES.Node.ACTIVATION.TAN:
                        a = new TanNode();
                        break;
                    case NEAT.GENES.Node.ACTIVATION.BIPOLAR:
                        a = new BipolarNode();
                        break;
                    case NEAT.GENES.Node.ACTIVATION.GAUSSIAN:
                        a = new GaussianNode();
                        break;
                }
                a.nb = node.nb;
                nodes_NN[node.nb] = a;
            }

            foreach (NEAT.GENES.Connection connection in connections)
            {
                if (connection.enabled)
                {
                    if (nodes_NN[connection.outNode].inNodes == null)
                        nodes_NN[connection.outNode].inNodes = new List<Node>();
                    nodes_NN[connection.outNode].inNodes.Add(nodes_NN[connection.inNode]);

                    if (nodes_NN[connection.inNode].outNodes == null)
                        nodes_NN[connection.inNode].outNodes = new List<Node>();
                    nodes_NN[connection.inNode].outNodes.Add(nodes_NN[connection.outNode]);
                    nodes_NN[connection.outNode].w.Add(connection.w);
                }
            }

            foreach(NEAT.GENES.Node node in nodes)
            {
                if(node.property == NEAT.GENES.Node.NODE.IN)
                {
                    inNodes.Add(nodes_NN[node.nb]);
                }
                if (node.property == NEAT.GENES.Node.NODE.OUT)
                {
                    outNodes.Add(nodes_NN[node.nb]);
                }
                if (node.property == NEAT.GENES.Node.NODE.HIDDEN)
                {
                    hidNodes.Add(nodes_NN[node.nb]);
                }

            }
        }

        public List<float> evaluate(List<float> inputs)
        {
            List<float> res = new List<float>();

            for(int i = 0; i < outNodes.Count; ++i)
            {
                res.Add(outNodes[i].evaluateBack(inputs));
            }

            return res;
        }

        public void randomizeWeight()
        {
            foreach (Node inN in inNodes)
            {
                for (int i = 0; i < inN.w.Count; ++i)
                {
                    inN.w[i] = Random.Range(-0.5f, 0.5f);
                }
            }
            foreach (Node outN in outNodes)
            {
                for (int i = 0; i < outN.w.Count; ++i)
                {
                    outN.w[i] = Random.Range(-0.5f, 0.5f);
                }
            }
            foreach (Node hidN in hidNodes)
            {
                for (int i = 0; i < hidN.w.Count; ++i)
                {
                    hidN.w[i] = Random.Range(-0.5f, 0.5f);
                }
            }
        }
    }
}
