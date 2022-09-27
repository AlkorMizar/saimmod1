using saimmod1.Algoritms;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

// 15 1643 12031278
namespace saimmod1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        delegate Algorithm CreateAlg(Algorithm rand);
        Alg alg;
        Algorithm currAlg;
        const int K = 20;
        CreateAlg func;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCalculate(object sender, RoutedEventArgs e)
        {
            int R0, m, a;
            ClearGrid();
            Algorithm distr;
            try
            {
                R0 = int.Parse(R0Text.Text);
                m = int.Parse(mText.Text);
                a = int.Parse(aText.Text);
                alg = new Alg(R0, m, a);
                distr = func(alg);

                if (m <= 0)
                {
                    MessageBox.Show("Введенные заначениe m должны быть больше >0");
                    return;
                }

                if (R0 < 0 || a < 0)
                {
                    MessageBox.Show("Введенные заначения R0 и a должны быть больше >=0");
                    return;
                }


            }
            catch(Exception err) 
            {
                MessageBox.Show("Ошибка в вводе");
                return;
            }
            var ex = new Examiner(alg.Clone(),20,2_000_000);
            DrawHist(distr.GetHistogram(20, ex.Aperiod));
            SetCharacteristics(distr.GetRealStatistic(ex.Aperiod));
        }

        private void ClearGrid()
        {
            var list = vals.Children.Cast<Label>().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Content = "";
            }

            var rects = hist.Children.Cast<Rectangle>().ToList();
            var cNumList = numC.Children.Cast<Label>().ToList();

            for (int i = 0; i < rects.Count; i++)
            {
                rects[i].Height = 0;
                cNumList[i].Content = "";
            }
        }

        private void DrawHist(Histogram histogram)
        {
            var list = vals.Children.Cast<Label>().ToList();
            for (int i = 0; i < histogram.Length; i++)
            {
                list[i].Content = histogram[i].from.ToString("F4");
            };
            list[histogram.Length].Content = histogram[histogram.Length-1].to.ToString("F4");
            //            list[^1].Content = "1";
            //            list[1].Content = "0";
            var height = hist.Height;
            var rects = hist.Children.Cast<Rectangle>().ToList();
            var cNumList = numC.Children.Cast<Label>().ToList();

            var max = (histogram.Max * 1.2);
            ;
            var revM = 1.0f / 20;
            var Y = height - revM / max * height;
            for (int i = 0; i < histogram.Length; i++)
            {
                rects[i].Height = histogram[i].c / max * height;
                cNumList[i].Content = histogram[i].c.ToString("F7");
            }
        }

        private void SetCharacteristics((double m,double d) val)
        {
            mathExpect.Content = val.m.ToString("F4");
            dispersion.Content = val.d.ToString("F4");
            deviation.Content = Math.Sqrt(val.d).ToString("F4");
        }

        private void UneditableTextInp()
        {
            a.IsReadOnly = true;
            b.IsReadOnly = true;
            a.Clear();
            b.Clear();
            
            m.IsReadOnly = true;
            sigm.IsReadOnly = true;
            m.Clear();
            sigm.Clear();

            lambda.IsReadOnly = true;
            n.IsReadOnly = true;
            lambda.Clear();
            n.Clear();
        }

        public void CheckUniform(object sender, RoutedEventArgs e)
        {
            UneditableTextInp();
            a.IsReadOnly = false;
            b.IsReadOnly = false;
            func = (rand) => {
                var aPar = double.Parse(a.Text);
                var bPar = double.Parse(b.Text);
                if (aPar > bPar)
                {
                    throw new ArgumentException("b should be bigger than a");
                }
                return new Uniform(aPar, bPar, rand);
            };
        }
        public void CheckExponential(object sender, RoutedEventArgs e)
        {
            UneditableTextInp();
            lambda.IsReadOnly = false;
            func = (rand) => {
                var lambdaPar = double.Parse(lambda.Text);
                return new Exponential(lambdaPar, rand);
            };

        }
        public void CheckedTriangle1(object sender, RoutedEventArgs e)
        {
            UneditableTextInp();
            a.IsReadOnly = false;
            b.IsReadOnly = false;
            func = (rand) => {
                var aPar = double.Parse(a.Text);
                var bPar = double.Parse(b.Text);
                if (aPar > bPar)
                {
                    throw new ArgumentException("b should be bigger than a");
                }
                return new Triangale13(aPar, bPar, rand);
            };

        }
        public void CheckedTriangle2(object sender, RoutedEventArgs e)
        {
            UneditableTextInp();
            a.IsReadOnly = false;
            b.IsReadOnly = false;
            func = (rand) => {
                var aPar = double.Parse(a.Text);
                var bPar = double.Parse(b.Text);
                if (aPar > bPar)
                {
                    throw new ArgumentException("b should be bigger than a");
                }
                return new Triangale14(aPar, bPar, rand);
            };

        }
        public void CheckedGauss(object sender, RoutedEventArgs e)
        {
            UneditableTextInp();

            m.IsReadOnly = false;
            sigm.IsReadOnly = false;
            func = (rand) => {
                var mPar = double.Parse(m.Text);
                var sigmaPar = double.Parse(sigm.Text);
                
                return new Gaus(mPar, sigmaPar, rand);
            };

        }
        public void CheckedGamma(object sender, RoutedEventArgs e)
        {
            UneditableTextInp();
            lambda.IsReadOnly = false;
            n.IsReadOnly = false;
            func = (rand) => {
                var lambdaPar = double.Parse(lambda.Text);
                var nPar = double.Parse(n.Text);
                return new Gamma(lambdaPar,nPar, rand);
            };

        }
        public void CheckedSimpson(object sender, RoutedEventArgs e)
        {
            UneditableTextInp();
            a.IsReadOnly = false;
            b.IsReadOnly = false;
            func = (rand) => {
                var aPar = double.Parse(a.Text);
                var bPar = double.Parse(b.Text);
                if (aPar > bPar)
                {
                    throw new ArgumentException("b should be bigger than a");
                }
                return new Simpsons(aPar, bPar, rand);
            };

        }
    }
}
