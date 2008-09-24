﻿using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.ShortestPath
{
    [Serializable]
    public abstract class ShortestPathAlgorithmBase<TVertex, TEdge, TGraph> :
        RootedAlgorithmBase<TVertex,TGraph>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, GraphColor> vertexColors;
        private readonly IDictionary<TVertex, double> distances;
        private readonly Func<TEdge, double> weights;
        private readonly IDistanceRelaxer distanceRelaxer;

        protected ShortestPathAlgorithmBase(
            IAlgorithmComponent host,
            TGraph visitedGraph,
            Func<TEdge, double> weights
            )
            :this(host, visitedGraph, weights, ShortestDistanceRelaxer.Instance)
        {}

        protected ShortestPathAlgorithmBase(
            IAlgorithmComponent host,
            TGraph visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(host, visitedGraph)
        {
            if (weights == null)
                throw new ArgumentNullException("weights");
            if (distanceRelaxer == null)
                throw new ArgumentNullException("distanceRelaxer");

            this.vertexColors = new Dictionary<TVertex, GraphColor>();
            this.distances = new Dictionary<TVertex, double>();
            this.weights = weights;
            this.distanceRelaxer = distanceRelaxer;
        }

        public IDictionary<TVertex, GraphColor> VertexColors
        {
            get
            {
                return this.vertexColors;
            }
        }

        public GraphColor GetVertexColor(TVertex vertex)
        {
            return this.vertexColors[vertex];
        }

        public IDictionary<TVertex, double> Distances
        {
            get
            {
                return this.distances;
            }
        }

        public Func<TEdge, double> Weights
        {
            get { return this.weights; }
        }

        public IDistanceRelaxer DistanceRelaxer
        {
            get { return this.distanceRelaxer; }
        }

        protected bool Compare(double a, double b)
        {
            return this.distanceRelaxer.Compare(a, b);
        }

        protected double Combine(double distance, double weight)
        {
            return this.distanceRelaxer.Combine(distance, weight);
        }
    }
}