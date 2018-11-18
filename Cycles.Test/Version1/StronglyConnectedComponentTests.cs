using System.Collections.Generic;
using System.Linq;
using Cycles.Version1;
using Xunit;

namespace Cycles.Test.Version1
{
    public class StronglyConnectedComponentTests
    {
        [Fact]
        public void EmptyGraph()
        {
            var graph = new List<Vertex<int>>();
            var detector = new StronglyConnectedComponentFinder<int>();
            var cycles = detector.DetectCycle(graph);
            Assert.Equal(0, cycles.Count);
        }

        // A
        [Fact]
        public void SingleVertex()
        {
            var graph = new List<Vertex<int>>();
            graph.Add(new Vertex<int>(1));
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.Equal(1, components.Count);
            Assert.Equal(1, components.IndependentComponents().Count());
            Assert.Equal(0, components.Cycles().Count());
        }

        // A→B
        [Fact]
        public void Linear2()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            vA.Dependencies.Add(vB);
            graph.Add(vA);
            graph.Add(vB);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.Equal(2, components.Count);
            Assert.Equal(2, components.IndependentComponents().Count());
            Assert.Equal(0, components.Cycles().Count());
        }

        // A→B→C
        [Fact]
        public void Linear3()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            var vC = new Vertex<int>(3);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.Equal(3, components.Count);
            Assert.Equal(3, components.IndependentComponents().Count());
            Assert.Equal(0, components.Cycles().Count());
        }

        // A↔B
        [Fact]
        public void Cycle2()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vA);
            graph.Add(vA);
            graph.Add(vB);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.Equal(1, components.Count);
            Assert.Equal(0, components.IndependentComponents().Count());
            Assert.Equal(1, components.Cycles().Count());
            Assert.Equal(2, components.First().Count);
        }

        // A→B
        // ↑ ↓
        // └─C
        [Fact]
        public void Cycle3()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            var vC = new Vertex<int>(3);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            vC.Dependencies.Add(vA);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.Equal(1, components.Count);
            Assert.Equal(0, components.IndependentComponents().Count());
            Assert.Equal(1, components.Cycles().Count());
            Assert.Equal(3, components.Single().Count);
        }

        // A→B   D→E
        // ↑ ↓   ↑ ↓
        // └─C   └─F
        [Fact]
        public void TwoIsolated3Cycles()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            var vC = new Vertex<int>(3);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            vC.Dependencies.Add(vA);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);

            var vD = new Vertex<int>(4);
            var vE = new Vertex<int>(5);
            var vF = new Vertex<int>(6);
            vD.Dependencies.Add(vE);
            vE.Dependencies.Add(vF);
            vF.Dependencies.Add(vD);
            graph.Add(vD);
            graph.Add(vE);
            graph.Add(vF);

            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.Equal(2, components.Count);
            Assert.Equal(0, components.IndependentComponents().Count());
            Assert.Equal(2, components.Cycles().Count());
            Assert.True(components.All(c => c.Count == 3));
        }

        // A→B
        // ↑ ↓
        // └─C-→D
        [Fact]
        public void Cycle3WithStub()
        {
            var graph = new List<Vertex<int>>();
            var vA = new Vertex<int>(1);
            var vB = new Vertex<int>(2);
            var vC = new Vertex<int>(3);
            var vD = new Vertex<int>(4);
            vA.Dependencies.Add(vB);
            vB.Dependencies.Add(vC);
            vC.Dependencies.Add(vA);
            vC.Dependencies.Add(vD);
            graph.Add(vA);
            graph.Add(vB);
            graph.Add(vC);
            graph.Add(vD);
            var detector = new StronglyConnectedComponentFinder<int>();
            var components = detector.DetectCycle(graph);
            Assert.Equal(2, components.Count);
            Assert.Equal(1, components.IndependentComponents().Count());
            Assert.Equal(1, components.Cycles().Count());
            Assert.Equal(1, components.Count(c => c.Count == 3));
            Assert.Equal(1, components.Count(c => c.Count == 1));
            Assert.True(components.Single(c => c.Count == 1).Single() == vD);
        }
    }
}
