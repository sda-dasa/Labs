using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Lagrange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = graph.GraphPane; pane.Title.Text = "Аппроксимация функции " +
               "\n полиномом Лагранжа";
            graph.Visible = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
        public void Generate_Graph(Function func) 
        {
            GraphPane pane = graph.GraphPane;
            graph.Visible = true;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            LineItem curve;

            int n = Convert.ToInt32(numericUpDown1.Value);
            double h = 2.0 / (n + 1);
            // заполняем список
            for (double x = -1; x <= 1; x+=h)
            {
                list.Add(x, Generate_Polinom.GetValue(x, n , func));
            }

            curve = pane.AddCurve($"Аппроксимированная функция",
                        list, Color.Red);
            curve.Line.Width = 2.5F;

            list = new PointPairList();

            for (double x = -1; x <= 1; x += h)
            {
                list.Add(x, func(x));
            }

            curve = pane.AddCurve($"Исходная функция",
                        list, Color.Blue);
            curve.Line.Width = 2.5F;

            list = new PointPairList();
            for (int i = 0; i < n + 1; i++ )
            {
                double x = Math.Cos((2 * i + 1) * Math.PI / (2 * n)); 
                list.Add(x, Generate_Polinom.GetValue(x, n + 1, func, true));
            }

            curve = pane.AddCurve($"Аппроксимированная функция (Узлы Чебышева)",
                        list, Color.Green);
            curve.Line.Width = 2.5F;


            graph.AxisChange();
            graph.Invalidate();


        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {

                switch(comboBox1.SelectedIndex) 
                {
                    case 0:
                        Generate_Graph(ln_func); break;
                    case 1:
                        Generate_Graph(module); break;
                    case 2:
                        Generate_Graph(pow_10); break;
                    case 3:
                        Generate_Graph(exp_func); break;
                    case 4:
                        Generate_Graph(devide_func); break;
                    default: break;
                }
            }
            else MessageBox.Show("Выберите функцию");


        }



        public delegate double Function(double x);




        internal class Generate_Polinom
        {
            
            public static double GetValue(double x, int n, Function func, bool mode = false) // usly - значения x_k 
                                                                   // [] values - значения функции в узлах
            {
                double[] usly = new double[n]; double[] values = new double[n];
                if (mode)
                {
                    for (int i = 0; i < n; i++)
                    {
                        usly[i] = Math.Cos((2 * i + 1) * Math.PI / (2*n));
                        values[i] = func(usly[i]);
                    }

                }
                else
                {
                    double h = 2.0 / (n); usly[0] = -1; values[0] = func(usly[0]);
                    for (int i = 1; i < n; i++)
                    {
                        usly[i] = usly[i - 1] + h; values[i] = func(usly[i]);
                    }
                }

                double[] p_k = new double[n]; // p_k - коэф 
                
                for (int i = 0; i < n; i++) p_k[i] = 1;
                for (int k = 0; k < n; k++)
                {
                    for (int i = 0; i < n; i++) if (i != k) p_k[k] *= x - usly[i];

                    for (int i = 0; i < n; i++) if (i != k) p_k[k] /= usly[k] - usly[i];
                }
                double val_Lagrange = 0;
                for (int k = 0; k < n; k++)
                {
                    val_Lagrange += p_k[k] * values[k];
                }

                return val_Lagrange; // для одной точки вычисляется значение полинома Лагранжа

            }

            //        x^4+21x^3-2x^2-6x+11
            //ln20x^2+14x+2, 
            //|x|
            //12^x
            //exp^(-kx^2)
            //1/(1+25x^2)

            

        }

        public double ln_func(double x) => Math.Log(20 * (x+1.1)) + 14 * x + 2;

        public double devide_func(double x) => 1.0 / (1.0 + 25 * x * x);

        public double exp_func(double x) => Math.Exp(-2 * x*x);

        public double module(double x) => x < 0 ? -x : x;

        public double pow_10(double x) => Math.Pow(12, x);


    }
}
