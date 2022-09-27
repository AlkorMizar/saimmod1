using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1.Algoritms
{
    class Uniform : Algorithm
    {
        Algorithm rand;

        double a, b;
        public Uniform(double _a, double _b,Algorithm _rand) {
            (a,b) =_a<_b?(_a,_b):(_b,_a);
            rand = _rand;
        }

        public override double GetNext()
        {
            return a + (b-a)*rand.GetNext();
        }


        public override (double m, double d) GetStatistic(long N)
        {
            var clone = this.Clone();
            
            var m = 0d;
            for (long i = 0; i < N ; i ++)
            {
                m += clone.GetNext();
            }

            m /= N;

            clone.Reset();
            double d = 0d;

            for (long i = 0; i < N; i++)
            {
                var x = clone.GetNext();
                d += (x - m) * (x - m);
            }

            d /= (N - 1);

            return (m, d);
        }

        public override void Reset()
        {
            rand.Reset();
        }

        public override Algorithm Clone()
        {
            return new Uniform(a, b, rand.Clone());
        }
    }
}
