using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1.Algoritms
{
    internal class Gamma : Algorithm
    {
        Algorithm rand;

        double lambdaRev,n,lambda;
        public Gamma(double _lambda,double _n, Algorithm _rand)
        {
            lambdaRev = -1 / _lambda;
            lambda = _lambda;
            n = _n;
            rand = _rand;
        }

        public override Algorithm Clone()
        {
            return new Gamma(lambda,n,rand.Clone());
        }

        public override double GetNext()
        {
            double res=0;
            for (int i = 0; i < n; i++)
            {
                res += Math.Log(rand.GetNext());
            }
            return lambdaRev*res;
        }

        public override (double m, double d) GetStatistic(long N)
        {
            return (lambdaRev*n, lambdaRev*lambdaRev*n);
        }

        public override void Reset()
        {
            rand.Reset();
        }
    }
}
