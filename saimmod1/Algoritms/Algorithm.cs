using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1.Algoritms
{
    public abstract class Algorithm
    {
        public abstract double GetNext();
        public abstract void Reset();
        public abstract (double m, double d) GetStatistic(long N);

        public (float min, float max) MinMaxFor(long N)
        {
            var max = float.MinValue;
            var min = float.MaxValue;
            var clone = this.Clone();

            for (long i = 0; i < N; i++)
            {
                float x = (float)clone.GetNext();
                max = Math.Max(max, x);
                min = Math.Min(min, x);
            }
            return (min, max);

        }

        public abstract Algorithm Clone();
        public Histogram GetHistogram(long K,long N) 
        {
            var (min, max) = MinMaxFor(N);

            Histogram histogram;
            if (min == max)
            {
                histogram = new Histogram(1);
                histogram.UpdateWInd(0, 1, max, 0);
                return histogram;

            }
            float r = max - min;
            float delt = r / K;

            var ms = new long[K];
            histogram = new Histogram(K);

            this.Reset();
            for (long j = 0; j < N; j++)
            {
                var x = this.GetNext();
                if (x == max)
                {
                    ms[K - 1]++;
                    continue;
                }
                var ind = (long)Math.Floor((x - min) / delt);
                ms[ind]++;
            }

            var from = min;
            var to = min + delt;
            for (long i = 0; i < K; i++)
            {
                histogram.UpdateWInd(from, ((float)ms[i]) / N, to, i);
                from = to;
                to = i != K - 1 ? from + delt : max;
            }

            return histogram;
        }
    }
}
