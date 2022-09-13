using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1
{
    public class Alg
    {
        long R0;
        double previousR;
        double m, a;
        public long M { get => (long)m; }

        public Alg(long R0,long m,long a) {
            if(m<a)
                throw new ArgumentException($"a>m : {a}>{m}");
            previousR= this.R0 = R0;
            this.a = a;
            this.m = m;
        }

        private Alg(long R0, double prev, long m, long a):this(R0,m,a)
        {
            previousR = prev;
        }

        public float GetNext() {
            previousR = (previousR * a) % m;
            var res = (float)(previousR / m) ;
            return res;
        }

        public void Reset() {
            previousR = R0;
        }

        public Alg CloneWithState()
        {
            return new Alg(R0,previousR, M, (long)a);
        }

        public void ToState(long i)
        {
            while (i!=0)
            {
                GetNext();
                i--;
            }
        }
    }
}
