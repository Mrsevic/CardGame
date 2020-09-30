using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Framework
{
    public static class Extensions
    {
        static readonly Random Generator = new Random();
        public static IEnumerable<T> Shuffle<T>(this IList<T> sequence)
        {
            for (var i = sequence.Count - 1; i >= 0; i--)
            {
                // based on comment from J.Skeet
                // Swap element "i" with a random earlier element it (or itself)
                // ... except we don't really need to swap it fully, as we can
                // return it immediately, and afterwards it's irrelevant.
                var swapIndex = Generator.Next(i + 1);
                if (swapIndex == i) // don't replace with itself
                {
                    yield return sequence[i];
                    continue;
                }
                yield return sequence[swapIndex];
                sequence[swapIndex] = sequence[i];
            }
        }
        public static List<List<T>> Split<T>(this List<T> collection, int size)
        {
            var chunks = new List<List<T>>();
            var chunkCount = collection.Count / size;

            if (collection.Count % size > 0)
                chunkCount++;

            for (var i = 0; i < chunkCount; i++)
                chunks.Add(collection.Skip(i * size).Take(size).ToList());

            return chunks;
        }
    }
}
