using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saimmod1
{
    public class Alg
    {
        int R0;
        int previousR;
        int m, a;
        public int M { get => m; }

        float expr_m, expr_d;//math and disp
        int step;//n
        public Alg(int R0,int m,int a) {
            if(m<a)
                throw new ArgumentException($"a>m : {a}>{m}");
            previousR= this.R0 = R0;
            this.a = a;
            this.m = m;
        }

        private Alg(int R0, int prev, int m, int a):this(R0,m,a)
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

        public void ToState(int i)
        {
            while (i!=0)
            {
                GetNext();
                i--;
            }
        }
    }
}
