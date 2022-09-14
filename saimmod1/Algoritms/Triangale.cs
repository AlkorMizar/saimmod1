using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1.Algoritms
{
    class Triangale13 : Algorithm
    {
        Algorithm rand;

        double a, b;
        public Triangale13(double _a, double _b, Algorithm _rand)
        {
            (a, b) = _a < _b ? (_a, _b) : (_b, _a);
            rand = _rand;
        }

        public override Algorithm Clone()
        {
            return new Triangale13(a, b, rand.Clone());
        }

        public override double GetNext()
        {
            double R1 = rand.GetNext(), R2 = rand.GetNext();
            return a + (b - a) * Math.Max(R1, R2);
        }

        private double GetProbability(double x)
        {
            if (x < a || x > b)
            {
                return 0;
            }
            return 2 * (x - a) / ((b - a) * (b - a));
        }

        public override (double m, double d) GetStatistic(long N)
        {
            var clone = rand.Clone();
            double m = 0, d = 0, x = 0;

            for (int i = 0; i < N; i++)
            {
                x = clone.GetNext();
                m += x * GetProbability(x);
            }

            clone.Reset();
            for (int i = 0; i < N; i++)
            {
                x = clone.GetNext();
                d += (x - m) * (x - m) * GetProbability(x);
            }

            return (m, d);
        }

        public override void Reset()
        {
            rand.Reset();
        }
    }
    class Triangale14 : Algorithm
    {
        Algorithm rand;

        double a, b;
        public Triangale14(double _a, double _b, Algorithm _rand)
        {
            (a, b) = _a < _b ? (_a, _b) : (_b, _a);
            rand = _rand;
        }
        public override double GetNext()
        {
            double R1 = rand.GetNext(), R2 = rand.GetNext();
            return a + (b - a) * Math.Min(R1, R2);
        }
        public override void Reset()
        {
            rand.Reset();
        }

        private double GetProbability(double x)
        {
            if (x < a || x > b)
            {
                return 0;
            }
            return 2 * (b - x) / ((b - a) * (b - a));
        }

        public override (double m, double d) GetStatistic(long N)
        {
            var clone = rand.Clone();
            double m = 0, d = 0, x;

            for (int i = 0; i < N; i++)
            {
                x = clone.GetNext();
                m += x * GetProbability(x);
            }

            clone.Reset();
            for (int i = 0; i < N; i++)
            {
                x = clone.GetNext();
                d += (x - m) * (x - m) * GetProbability(x);
            }

            return (m, d);
        }

        public override Algorithm Clone()
        {
            return new Triangale14(a, b, rand.Clone());
        }

    }
}
