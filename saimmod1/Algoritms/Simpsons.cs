using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1.Algoritms
{
    internal class Simpsons : Algorithm
    {
        Algorithm rand;

        double  a,b;
        public Simpsons(double _a, double _b, Algorithm _rand)
        {
            (a,b) = _a < _b ? (_a, _b) : (_b, _a );
            rand = _rand;
        }
        private double GetNextUniform()
        {
            return a/2 + ((b-a)/2) * rand.GetNext();
        }

        public override double GetNext()
        {
            return GetNextUniform() + GetNextUniform();
        }
        public override void Reset()
        {
            rand.Reset();
        }


        private double GetProbability(double x)
        {
            if (x >= a && x <= (a+b)/2)
            {
                return 4 * (x - a) / ((b - a) * (b - a));
            }
            if (x >= (a + b) / 2 && x <= b)
            {
                return 4 * (b - x) / ((b - a) * (b - a));
            }
            return 0;
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
            return new Simpsons(a,b,rand.Clone());
        }
    }
}
