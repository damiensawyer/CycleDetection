using System.Collections.Generic;
using System.Linq;
using Cycles.Version1;
using Cycles.Version2;
using Xunit;

namespace Cycles.Test.Version2
{
    public class StronglyConnectedComponentTests
    {
        [Fact()]
        public void TarjanStackTest()
        {
            // tests simple model presented on https://en.wikipedia.org/wiki/Tarjan%27s_strongly_connected_components_algorithm
            var graph_nodes = new List<Vertex>();

            var v1 = new Vertex() { Id = 1 };
            var v2 = new Vertex() { Id = 2 };
            var v3 = new Vertex() { Id = 3 };
            var v4 = new Vertex() { Id = 4 };
            var v5 = new Vertex() { Id = 5 };
            var v6 = new Vertex() { Id = 6 };
            var v7 = new Vertex() { Id = 7 };
            var v8 = new Vertex() { Id = 8 };

            v1.Dependencies.Add(v2);
            v2.Dependencies.Add(v3);
            v3.Dependencies.Add(v1);
            v4.Dependencies.Add(v3);
            v4.Dependencies.Add(v5);
            v5.Dependencies.Add(v4);
            v5.Dependencies.Add(v6);
            v6.Dependencies.Add(v3);
            v6.Dependencies.Add(v7);
            v7.Dependencies.Add(v6);
            v8.Dependencies.Add(v7);
            v8.Dependencies.Add(v5);
            v8.Dependencies.Add(v8);

            graph_nodes.Add(v1);
            graph_nodes.Add(v2);
            graph_nodes.Add(v3);
            graph_nodes.Add(v4);
            graph_nodes.Add(v5);
            graph_nodes.Add(v6);
            graph_nodes.Add(v7);
            graph_nodes.Add(v8);

            var tcd = new TarjanCycleDetectStack();
            var cycle_list = tcd.DetectCycle(graph_nodes);

            Assert.True(cycle_list.Count == 4);
        }
    }
}