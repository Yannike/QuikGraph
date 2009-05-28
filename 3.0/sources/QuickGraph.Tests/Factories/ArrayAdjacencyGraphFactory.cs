// <copyright file="ArrayAdjacencyGraphFactory.cs" company="MSIT">Copyright © MSIT 2007</copyright>

using System;
using Microsoft.Pex.Framework;
using QuickGraph;

namespace QuickGraph
{
    public static partial class ArrayAdjacencyGraphFactory
    {
        [PexFactoryMethod(typeof(ArrayAdjacencyGraph<int, SEdge<int>>))]
        public static ArrayAdjacencyGraph<int, SEdge<int>> Create(
            int vertexCount,
            SEdge<int>[] edges
        )
        {
            PexAssume.IsTrue(0 <= vertexCount);
            PexSymbolicValue.Minimize(vertexCount);
            PexAssume.TrueForAll(edges, e => 
                0 <= e.Source && e.Source < vertexCount && 
                0 <= e.Target && e.Target < vertexCount
                );
            var arrayAdjacencyGraph
               = new ArrayAdjacencyGraph<int, SEdge<int>>
                  (vertexCount, edges, v => v);
            return arrayAdjacencyGraph;
        }
    }
}
