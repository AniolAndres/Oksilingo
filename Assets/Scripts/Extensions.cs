using System;
using System.Collections.Generic;

namespace Scripts
{
    public static class Extensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1); // 0 ≤ j ≤ i
                // Swap
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}