using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1
{
    class Examiner
    {
        Alg alg;
        const float EPS = 0.00000001f;
        float max, min;

        public float MathExpect { get; }
        public float Dispersion { get; }
        public float Deviation { get; }
        public float Hits { get; }
        public float K2N { get; }
        public long N { get; }
        public long Period { get; }
        public long Aperiod { get; }

        public Histogram HistogramStr { get; }

        public Examiner(Alg alg, long K,long N)
        {
            this.alg = alg.CloneWithState();
            this.alg.Reset();

            this.N = N;

            (Hits, K2N, MathExpect,max,min) = InderecctProperties();
            ( Dispersion, Deviation) = StatisticProperties(MathExpect);
            (Period, Aperiod) = PeriodicProperties();

            HistogramStr = GenerateHistogeram(K);
        }

        private Histogram GenerateHistogeram(long K)
        {
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

            alg.Reset();
            for (long j = 0; j < N;j++)
            {
                var x = alg.GetNext();
                if (x==max)
                {
                    ms[K - 1]++;
                    continue;
                }
                var ind = (long)Math.Floor((x - min ) / delt);
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

        private (float d, float dev) StatisticProperties(float mathExp)
        {
            alg.Reset();
            float d = 0, dev = 0;
            
            for (long i = 0; i < N; i++)
            {
                var x =(float) alg.GetNext();
                d += (x - mathExp) * (x - mathExp);
            }

            d /= (N - 1);

            dev = (float)Math.Sqrt(d);
            return (d, dev);
        }

        private (long K, float K2N, float MathExp,float max,float min) InderecctProperties()
        {
            var (K, m,max,min) = CountHits();
            var K2N = K * 2.0f / N;
            return (K, K2N, m,max,min);
        }

        private (long Period, long Aperiod) PeriodicProperties()
        {

            var p = CalculatePeriod(N-1, EPS);

            if (p < 0)
            {
                return (-1, -1);
            }

            return (p, CalculateAperiod(p, EPS));
        }

        private (long Hits, float MathExp,float max,float min) CountHits()
        {
            alg.Reset();
            
            var max = float.MinValue;
            var min = float.MaxValue;

            var h = 0;
            var m = 0f;
            for (long i = 0; i < N - 2; i += 2)
            {
                var x2i_1 = (float)alg.GetNext();
                var x2i = (float)alg.GetNext();
                if (x2i * x2i + x2i_1 * x2i_1 < 1)
                {
                    h++;
                }
                m += x2i + x2i_1;

                var (maxX, minX) = x2i > x2i_1 ? (x2i, x2i_1) : (x2i_1, x2i);
                max = Math.Max(max, maxX);
                min = Math.Min(min, minX);
            }
            return (h, m / N,max,min);
        }

        private long CalculateAperiod(long period, float eps)
        {
            var clone0 = alg.CloneWithState();
            clone0.Reset();
            var cloneP = clone0.CloneWithState();
            cloneP.ToState(period);
            long i3 = 0;
            float xi3 = (float)clone0.GetNext(), xi3P = (float)cloneP.GetNext();
            /*while ((Math.Abs(xi3 - xi3P) > eps) && i3 < N)
            {
                xi3 = clone0.GetNext();
                xi3P = cloneP.GetNext();
                i3++;
            }*/
            while (xi3.CompareTo(xi3P)!=0 && i3 < N)
            {
                xi3 = (float)clone0.GetNext();
                xi3P = (float)cloneP.GetNext();
                i3++;
            }
            if ((Math.Abs(xi3 - xi3P) > eps))
                return -1;
            return period + i3;
        }

        private long CalculatePeriod(long v, float eps)
        {
            var x_v = GetAt(v);

            var clone = alg.CloneWithState();
            clone.Reset();

            long i1, i2;
            
            i1 = FindClone(x_v, eps, N, clone);
            if (i1 < 0)
                return -1;
            i2 = FindClone(x_v, eps, N, clone) + i1;
            if (i2 < 0)
                return -1;

            return i2 - i1;
        }

        private float GetAt(long v)
        {
            var clone = alg.CloneWithState();
            alg.Reset();
            float x_v = -1;
            for (long i = 0; i < v; i++)
            {
                x_v = (float)alg.GetNext();
            }

            return x_v;
        }

        public long FindClone(float x_v, float eps, long upperBorder, Alg alg)
        {
            long i = 0;
            while (i < upperBorder)
            {
                float x = (float)alg.GetNext();
                i++;
                /*if ( Math.Abs(x - x_v) < eps)
                {
                    return i;
                }*/

                if (x.CompareTo(x_v) == 0)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
