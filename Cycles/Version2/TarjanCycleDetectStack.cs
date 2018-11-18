using System;
using System.Collections.Generic;

namespace Cycles.Version2
{
    public class TarjanCycleDetectStack
    {
        protected List<List<Vertex>> _StronglyConnectedComponents;
        protected Stack<Vertex> _Stack;
        protected int _Index;

        public List<List<Vertex>> DetectCycle(List<Vertex> graph_nodes)
        {
            _StronglyConnectedComponents = new List<List<Vertex>>();

            _Index = 0;
            _Stack = new Stack<Vertex>();

            foreach (Vertex v in graph_nodes)
            {
                if (v.Index < 0)
                {
                    StronglyConnect(v);
                }
            }

            return _StronglyConnectedComponents;
        }

        private void StronglyConnect(Vertex v)
        {
            v.Index = _Index;
            v.Lowlink = _Index;

            _Index++;
            _Stack.Push(v);

            foreach (Vertex w in v.Dependencies)
            {
                if (w.Index < 0)
                {
                    StronglyConnect(w);
                    v.Lowlink = Math.Min(v.Lowlink, w.Lowlink);
                }
                else if (_Stack.Contains(w))
                {
                    v.Lowlink = Math.Min(v.Lowlink, w.Index);
                }
            }

            if (v.Lowlink == v.Index)
            {
                List<Vertex> cycle = new List<Vertex>();
                Vertex w;

                do
                {
                    w = _Stack.Pop();
                    cycle.Add(w);
                } while (v != w);

                _StronglyConnectedComponents.Add(cycle);
            }
        }
    }
}