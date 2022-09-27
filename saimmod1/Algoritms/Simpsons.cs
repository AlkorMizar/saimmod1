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


        public override (double m, double d) GetStatistic(long N)
        {

            return ((a + b) / 2, (a- b)* (a - b) / 24);
        }

        public override Algorithm Clone()
        {
            return new Simpsons(a,b,rand.Clone());
        }
    }
}
