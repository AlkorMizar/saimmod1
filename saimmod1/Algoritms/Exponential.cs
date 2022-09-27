using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1.Algoritms
{
    class Exponential : Algorithm
    {
        Algorithm rand;

        double lambdaRev,lambda;
        public Exponential(double _lambda, Algorithm _rand)
        {
            lambdaRev = -1/_lambda;
            lambda = _lambda;
            rand = _rand;
        }

        public override Algorithm Clone()
        {
            return new Exponential(lambda,rand.Clone());
        }

        public override double GetNext()
        {
            return lambdaRev*Math.Log(rand.GetNext());
        }


        public override (double m, double d) GetStatistic(long N)
        {
            return (-lambdaRev, lambdaRev * lambdaRev);
        }

        public override void Reset()
        {
            rand.Reset();
        }
    }
}
