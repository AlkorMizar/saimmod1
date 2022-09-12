using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1
{
    public class Alg
    {
        long R0;
        long previousR;
        long m, a;
        public long M { get => m; }

        float expr_m, expr_d;//math and disp
        long step;//n
        public Alg(long R0,long m,long a) {
            if(m<a)
                throw new ArgumentException($"a>m : {a}>{m}");
            previousR= this.R0 = R0;
            this.a = a;
            this.m = m;
        }

        private Alg(long R0, long prev, long m, long a):this(R0,m,a)
        {
            previousR = prev;
        }

        public float GetNext() {
            previousR = (previousR * a) % m;
            var res = ((float)previousR) / m;
            step++;
            if (step > 2)
            {
                expr_d = expr_d * (step - 2) / (step - 1)+(res-expr_m)* (res - expr_m)/step;
            }
            expr_m = expr_m * (step - 1) / step + res / step;
            return res;
        }

        public void Reset() {
            expr_m = expr_d = 0;
            step = 0;
            previousR = R0;
        }

        public Alg CloneWithState()
        {
            return new Alg(R0,previousR, M, a);
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
