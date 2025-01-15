using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Match3Linked
{
    /// <summary>
    /// Provides extension methods for the <see cref="List{T}"/> class.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Retrieves a random element from the provided list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to retrieve the random element from.</param>
        /// <returns>A random element from the list.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the list is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the list is empty.</exception>
        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "The list cannot be null.");
            }

            if (list.Count == 0)
            {
                throw new InvalidOperationException("Cannot retrieve a random element from an empty list.");
            }

            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}