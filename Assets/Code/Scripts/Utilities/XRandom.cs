using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public delegate float ItemWeight<T>(T d);

    public delegate float ItemFilter<T>(T d);

    public class XRandom
    {
        public int Seed
        {
            get { return seed; }
            set
            {
                seed = value;
                random = new Random(seed);
            }
        }

        private int seed = 0;
        private Random random;

        public XRandom()
        {
            seed = UnityEngine.Random.Range(0, 99999);
            random = new Random(seed);
        }

        public XRandom(int seed)
        {
            this.seed = seed;
            random = new Random(seed);
        }

        public float Float()
        {
            return (float)random.NextDouble();
        }

        public int Int()
        {
            return random.Next();
        }

        public float Range(float start, float end)
        {
            return (end - start) * Float() + start;
        }

        public int Range(int start, int end)
        {
            if (start == end)
                return start;

            return start + random.Next() % (end - start);
        }

        public T Item<T>(IList<T> array, T def = default, Func<T, bool> filter = null)
        {
            if (array == null)
                return def;

            if (filter == null)
            {
                return array.Count == 0 ? def : array[random.Next() % array.Count];
            }

            var filtered = array.Where(filter).ToList();

            return filtered.Count == 0 ? def : filtered[random.Next() % filtered.Count];
        }

        public List<T> ItemTake<T>(IList<T> array, int count = 1, Func<T, bool> filter = null)
        {
            if (array == null)
                return new List<T>();

            var candidates = filter != null ? array.Where(filter) : array.ToArray();

            var result = (
                from c in candidates
                orderby Float()
                select c
            ).Take(count);

            return result.ToList();
        }

        public T ItemWeighted<T>(IList<T> sortedList, ItemWeight<T> weight)
        {
            var items = sortedList.ToList();

            var totalWeight = items.Sum(x => weight(x));
            var randomWeightedIndex = Range(0, totalWeight);
            var itemWeightedIndex = 0f;

            foreach (var item in items)
            {
                itemWeightedIndex += weight(item);

                if (randomWeightedIndex < itemWeightedIndex)
                {
                    return item;
                }
            }

            return default;
        }

        public float BottomHeavy
        {
            get
            {
                var v = Float();
                return v * v;
            }
        }

        public float TopHeavy
        {
            get
            {
                var v = Float();
                return 1f - (v * v);
            }
        }
    }
}