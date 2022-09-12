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

namespace saimmod1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Alg alg;
        const int K = 20;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCalculate(object sender, RoutedEventArgs e)
        {
            ClearGrid();
            int R0 = int.Parse(R0Text.Text);
            int m = int.Parse(mText.Text);
            int a = int.Parse(aText.Text);

            alg = new Alg(R0, m, a);
            var exam = new Examiner(alg,20);

            DrawHist(exam.HistogramStr,m);
            SetCharacteristics(exam);
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
            var linesList = lines.Children.Cast<Line>().ToList();

            for (int i = 0; i < rects.Count; i++)
            {
                rects[i].Height = 0;
                linesList[i].Y1 = linesList[i].Y2 = 0;
                cNumList[i].Content = "";
            }
        }

        private void DrawHist(Histogram histogram, int m)
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
            var linesList = lines.Children.Cast<Line>().ToList();

            var max = (histogram.Max * 1.2);
            ;
            var revM = 1.0f / 20;
            var Y = height - revM / max * height;
            for (int i = 0; i < histogram.Length; i++)
            {
                rects[i].Height = histogram[i].c / max * height;
                linesList[i].Y1 = linesList[i].Y2 = Y;
                cNumList[i].Content = histogram[i].c.ToString("F7");
            }
        }

        private void SetCharacteristics(Examiner examiner)
        {
            mathExpect.Content = examiner.MathExpect.ToString("F5");
            dispersion.Content = examiner.Dispersion.ToString("F5");
            deviation.Content = examiner.Deviation.ToString("F5");
            N.Content = examiner.N.ToString();
            hits.Content=examiner.Hits.ToString();
            K2N.Content = examiner.K2N.ToString("F5");
            diff.Content = (Math.Abs(Math.PI / 4 - examiner.K2N)).ToString("F5");
            period.Content = examiner.Period.ToString();
            aperiod.Content = examiner.Aperiod.ToString();
        }
    }
}
