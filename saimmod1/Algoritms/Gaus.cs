using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1.Algoritms
{
    class Gaus : Algorithm
    {
        Algorithm rand;
        double Two { get => Math.Sqrt(2); }

        double m, sigma;
        public Gaus(double _m, double _sigma, Algorithm _rand)
        {
            (m, sigma) = (_m, _sigma);
            rand = _rand;
        }
        public override double GetNext()
        {
            double res = 0;
            for (int i = 0; i < 6; i++)
            {
                res = +rand.GetNext();
            }
            return m + sigma * Two * (res - 3);
        }
        public override void Reset()
        {
            rand.Reset();
        }

        public override (double m, double d) GetStatistic(long N)
        {
            return (m,sigma*sigma);
        }

        public override Algorithm Clone()
        {
            return new Gaus(m,sigma,rand.Clone());
        }
    }
}
