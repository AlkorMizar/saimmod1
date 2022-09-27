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

        public override (double m, double d) GetStatistic(long N)
        {
            return ((a + 2 * b) / 3, (a * a +  b * b - 2 * a * b) / 18);
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

        public override (double m, double d) GetStatistic(long N)
        {
            return ((2 * a + b) / 3, ( a * a + b * b - 2 * a * b) / 18);
        }

        public override Algorithm Clone()
        {
            return new Triangale14(a, b, rand.Clone());
        }

    }
}
