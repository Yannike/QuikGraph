﻿using System;
using System.IO;
using System.Collections.Generic;

using QuickGraph.Unit;

using QuickGraph.Algorithms.Condensation;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms.Condensation
{
    [PexClass]
    [TypeFixture(typeof(IMutableVertexAndEdgeListGraph<string, Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    public partial class StronglyConnectedCondensationGraphAlgorithmTest
    {
        private CondensationGraphAlgorithm<string, Edge<string>,AdjacencyGraph<string,Edge<string>>> algo;
        [Test, PexTest]
        public void CondensateAndCheckVertexCount(
            [PexAssumeIsNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            this.algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            algo.Compute();
            CheckVertexCount(g);
        }

        [Test, PexTest]
        public void CondensateAndCheckEdgeCount(
            [PexAssumeIsNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            this.algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            algo.Compute();
            CheckEdgeCount(g);
        }
        [Test, PexTest]
        public void CondensateAndCheckComponentCount(
            [PexAssumeIsNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            this.algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            algo.Compute();
            CheckComponentCount(g);
        }
        [Test, PexTest]
        public void CondensateAndCheckDAG(
            [PexAssumeIsNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            this.algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            algo.Compute();
            CheckDAG(g);
        }

        private void CheckVertexCount(
            IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            int count = 0;
            foreach (AdjacencyGraph<string,Edge<string>> vertices in this.algo.CondensatedGraph.Vertices)
                count += vertices.VertexCount;
            Assert.AreEqual(g.VertexCount, count, "VertexCount does not match");
        }

        private void CheckEdgeCount(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            // check edge count
            int count = 0;
            foreach (CondensatedEdge<string, Edge<string>, AdjacencyGraph<string, Edge<string>>> edges in this.algo.CondensatedGraph.Edges)
                count += edges.Edges.Count;
            foreach (AdjacencyGraph<string, Edge<string>> vertices in this.algo.CondensatedGraph.Vertices)
                count += vertices.EdgeCount;
            Assert.AreEqual(g.EdgeCount, count, "EdgeCount does not match");
        }


        private void CheckComponentCount(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            // check number of vertices = number of storngly connected components
            int components = AlgoUtility.StronglyConnectedComponents<string, Edge<string>>(g, new Dictionary<string, int>());
            Assert.AreEqual(components, algo.CondensatedGraph.VertexCount, "ComponentCount does not match");
        }

        private void CheckDAG(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            // check it's a dag
            try
            {
                AlgoUtility.TopologicalSort(this.algo.CondensatedGraph);
            }
            catch (NonAcyclicGraphException)
            {
                Assert.Fail("Graph is not a DAG.");
            }

        }
    }
}