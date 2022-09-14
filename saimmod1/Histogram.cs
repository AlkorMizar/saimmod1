using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1
{
    public class Histogram
    {
        (float from, float c, float to)[] intervals;
        float max = float.MinValue;
        public (float from, float c, float to) this[int ind] {
            get=>intervals[ind];
        }

        public int Length { get => intervals.Length; }

        public float Max
        {
            get=>max;
        }

        public Histogram(long K) { 
            intervals = new (float from, float c, float to)[K];
        }

        public void UpdateWInd(float from, float c, float to,long ind) {
            intervals[ind]=(from, c, to);
            max = Math.Max(max, intervals[ind].c);
        }

        public (float from, float c, float to)[] ToArray()
        {
            return intervals.ToArray();
        }
    }
}
