using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1
{
    class Examiner
    {
        float[] X;
        float max, min;
        Alg alg;
        const float EPS = 0.00001f;

        public float MathExpect { get; }
        public float Dispersion { get; }
        public float Deviation { get; }
        public float Hits { get; }
        public float K2N { get; }
        public int N { get; }
        public int Period { get; }
        public int Aperiod { get; }

        public Histogram HistogramStr { get; }

        public Examiner(Alg alg, int K)
        {
            this.alg = alg;
            N = alg.M * 100;
            X = new float[N];

            alg.Reset();

            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < N; i++)
            {
                X[i] = alg.GetNext();
                min = Math.Min(min, X[i]);
                max = Math.Max(max, X[i]);
            }
        
            (MathExpect, Dispersion, Deviation) = StatisticProperties();
            (Hits, K2N) = InderecctProperties();
            (Period, Aperiod) = PeriodicProperties();

            Array.Sort(X);
            HistogramStr = GenerateHistogeram(K);
        }

        private Histogram GenerateHistogeram(int K)
        {
            float r = max - min;
            float delt = r / K;

            var histogram = new Histogram();

            var from = min;
            var to = from + delt;
            for (int j = 0; j < X.Length;)
            {
                var m = 0f;
                while (j < X.Length && X[j] <= to)
                {
                    m++;
                    j++;
                }
                histogram.Add(from, m / N, to);
                from = to;
                to = histogram.Length==K-1 ?max : from + delt;
            }

            return histogram;
        }

        private (float m, float d, float dev) StatisticProperties()
        {
            float m = 0, d = 0, dev = 0;
            foreach (var x in X)
            {
                m += x;
            }

            m /= N;

            foreach (var x in X)
            {
                d += (x - MathExpect) * (x - MathExpect);
            }

            d /= (N - 1);

            dev = (float)Math.Sqrt(d);
            return (m, d, dev);
        }

        private (int K, float K2N) InderecctProperties()
        {
            var K = CountHits();
            var K2N = K * 2.0f / N;
            return (K, K2N);
        }

        private(int Period,int Aperiod) PeriodicProperties()
        {

            var p=CalculatePeriod(N*10, EPS);

            if (p < 0)
            {
                return(-1,-1);
            }

            return (p,CalculateAperiod(p, EPS));
        }

        private int CountHits()
        {
            var h = 0;
            for (int i = 0; i < X.Length - 2; i += 2)
            {
                if (X[i] * X[i] + X[i + 1] * X[i + 1] < 1)
                {
                    h++;
                }
            }
            return h;
        }

        private int CalculateAperiod(int period,float eps)
        {
            var clone0 = alg.CloneWithState();
            clone0.Reset();
            var cloneP = clone0.CloneWithState();
            cloneP.ToState(period);
            int i3 = 1;
            float xi3 = clone0.GetNext(), xi3P = cloneP.GetNext();
            while ((Math.Abs(xi3 - xi3P) > eps) && i3 < 1000_000_000)
            {
                xi3 = clone0.GetNext();
                xi3P = cloneP.GetNext();
                i3++;
            }
            if ((Math.Abs(xi3 - xi3P) > eps))
                return -1;
            return period + i3;
        }

        private int CalculatePeriod(int v, float eps)
        {
            var x_v = GetAt(v);

            var clone = alg.CloneWithState();
            clone.Reset();

            int i1, i2;
            i1 = FindClone(x_v, eps, 1000_0000, clone);
            if (i1 < 0)
                return -1;
            i2 = FindClone(x_v, eps, 1000_0000, clone)+i1;
            if (i2 < 0)
                return -1;

            return i2 - i1;
        }

        private float GetAt(int v)
        {
            var clone = alg.CloneWithState();
            float x_v = 0;
            if (N < v)
            {
                for (int i = N; i < v; i++)
                {
                    x_v = alg.GetNext();
                }
            }
            else x_v = X[v];

            return x_v;
        }

        public int FindClone(float x_v, float eps, int upperBorder, Alg alg)
        {
            int i = 0;
            while (i < upperBorder)
            {
                float x = alg.GetNext();
                i++;
                if (Math.Abs(x - x_v) < eps)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
