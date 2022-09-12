using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1
{
    struct Histogram
    {
        List<(float from, float c, float to)> intervals;
        float max = float.MinValue;
        public (float from, float c, float to) this[int ind] {
            get=>intervals[ind];
        }

        public int Length { get => intervals.Count; }

        public float Max
        {
            get=>max;
        }

        public Histogram() { 
            intervals = new List<(float from, float c, float to)>();
        }

        public void Add(float from, float c, float to) {
            intervals.Add((from, c, to));
            max = Math.Max(max, intervals.Last().c);
        }

        public (float from, float c, float to)[] ToArray()
        {
            return intervals.ToArray();
        }
    }
}
