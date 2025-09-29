using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SolveSLAU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GraphPane pane = graph.GraphPane; pane.Title.Text = "Зависимость величины нормы невязки " +
               "\n от номера итерации";
            pane.XAxis.Title.Text = "Номер итерации";
            pane.YAxis.Title.Text = "Значение нормы невязки";




        }

        static bool CheckDescent(double[,] matr) // проверка на симметричность матрицы
        {
            for (int i = 0; i < matr.GetLength(0) - 1; i++)
                for (int j = i + 1;  j < matr.GetLength(1) - 1; j++) if (matr[i, j] != matr[j, i]) return false;
            // проверка на неотрицательность угловых элементов 
            if (matr[0, 0] < 0 || matr[0, matr.GetLength(1) - 1] < 0 ||
                matr[matr.GetLength(0) - 1, 0] < 0 || matr[matr.GetLength(0) - 1, matr.GetLength(1) - 1] < 0) 
                return false;

            return true;

        }


        static void DescentMethod(double[] x, double[,] matr, double eps, ref ZedGraph.ZedGraphControl graphic)
        {

            if (!CheckDescent(matr)) { MessageBox.Show("Матрица не удовлетворяет условиям\n для метода МН");  return; }

            double[] iter_x = new double[matr.GetLength(0)];

            double[] r = new double[matr.GetLength(0)]; r =  GetR_Procedure(x); // вычислили значение погрешности для исходного приближения
                        
            int iter_number = 1;

            GraphPane pane = graphic.GraphPane; 
            PointPairList list = new PointPairList();
            LineItem curve;


            while (Norma(r) > eps) 
            {
                double tau = GetTau();
                for (int i = 0; i < x.Length; i++)
                {
                    iter_x[i] = x[i] -  tau * r[i];
                }

                Array.Copy(iter_x, x, x.Length);

                r = GetR_Procedure(x);

                PointPair p = new PointPair(iter_number, Norma(r));

                list.Add(p);



                iter_number++;


            }

            curve = pane.AddCurve($"ММН",
            list, Color.DarkBlue, SymbolType.None);
            curve.Line.Width = 2.5F;

            graphic.AxisChange();
            graphic.Invalidate();


            if (iter_x.Length == 2) MessageBox.Show($"ММН: {iter_x[0]}, {iter_x[1]}");

            if (iter_x.Length == 3) MessageBox.Show($"ММН: {iter_x[0]}, {iter_x[1]}, {iter_x[2]}");

            if (iter_x.Length == 4) MessageBox.Show($"ММН: {iter_x[0]}, {iter_x[1]}, {iter_x[2]} {iter_x[3]} ");


            double[] GetR_Procedure(double[] vector, bool withf = true) 
                // процедура умножения матрицы системы на заданный вектор
            {

                double [] result = new double[matr.GetLength(0)];

                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    result[i] = 0;
                    
                    for (int j = 0; j < matr.GetLength(1) - 1; j++) result[i] += matr[i, j] * vector[j];

                    if (withf) result[i] -= matr[i, matr.GetLength(1) - 1];


                }

                return result;


            }



            double GetTau()
            {
                double[] Ar = GetR_Procedure(r, false); // находим вектор - произведение матр системы на вектор r

                // считаем скалярное произведение вектора Ar на r
                double t = 0; double Ar_ = 0;
                
                for (int i = 0; i <  Ar.Length; i++)
                {
                    t += Ar[i] * r[i];   
                }

                for(int i = 0; i < Ar.Length; i++)
                {
                    Ar_ += Ar[i] * Ar[i];
                }

                return t / Ar_; 

            }









        }



        // метод Якоби

        static void MethodYakoby(double[] approximation, double[,] matr, double eps, ref ZedGraph.ZedGraphControl graphic) //  изначальный x - приближенный вектор
        {

            // ПРОВЕРКА НА СХОДИМОСТЬ К РЕШЕНИЮ (ДОСТАТОЧНОЕ УСЛОВИЕ)

            if (!Check(matr))
            {
                MessageBox.Show("Матрица не удовлетворяет \n условию сходимости - преобладанию диагональных элементов"); return;
            }

            // строим матрицу B и вектор F

            double[,] newmatr = new double[matr.GetLength(0), matr.GetLength(1)]; // матрица В и вектор F

            for (int i = 0; i < matr.GetLength(0); i++) // для всех строк
            {
                for (int j = 0; j < matr.GetLength(1) - 1; j++) // для всех элементов в строке
                {
                    if (i != j) newmatr[i, j] = -matr[i, j] / matr[i, i];

                }

                newmatr[i, matr.GetLength(1) - 1] = matr[i, matr.GetLength(1) - 1] / matr[i, i];

                newmatr[i, i] = 0;


            }

            //метод простой итерации

            double[] x = new double[matr.GetLength(0)]; // первое приближение

            double[] iter_x = new double[x.Length]; // 2...k-тое приближение

            double[] residual = new double[x.Length]; // вектор xk+1 - xk

            for (int i = 0; i < x.Length; i++)
            { 
                // строим первое приближение

                double sum = 0;
                for (int j = 0; j < newmatr.GetLength(1) - 1; j++)
                    sum += approximation[i] * newmatr[i, j];

                x[i] = newmatr[i, newmatr.GetLength(1) - 1] + sum;

                residual[i] = x[i] - approximation[i];

            }

            GraphPane pane = graphic.GraphPane;
            PointPairList list = new PointPairList();
            LineItem curve;




            int iter_number = 1;

            while (Norma(residual) > eps)
            {
                for (int i = 0; i < newmatr.GetLength(0); i++)
                {
                    for (int j = 0; j < newmatr.GetLength(1) - 1; j++) iter_x[i] += newmatr[i, j] * x[j];

                    iter_x[i] += newmatr[i, matr.GetLength(1) - 1];

                }
                for (int i = 0; i < x.Length; i++) residual[i] = iter_x[i] - x[i];

                // подготовка значений для отрисовки графика

                PointPair p = new PointPair (iter_number, Norma(residual));

                list.Add(p);

                // подготовка к след итерации

                for (int i = 0; i < x.Length; i++) { x[i] = iter_x[i]; iter_x[i] = 0; }

                iter_number++;

            }
            // отрисовка графика

            curve = pane.AddCurve($"Метод Якоби",
                        list, Color.DarkRed, SymbolType.None);
            curve.Line.Width = 2.5F;

            graphic.AxisChange();
            graphic.Invalidate();




            if (iter_x.Length == 2) MessageBox.Show($"МЯ: {x[0]}, {x[1]}");

            if (iter_x.Length == 3) MessageBox.Show($"МЯ: {x[0]}, {x[1]}, {x[2]}");

            if (iter_x.Length == 4) MessageBox.Show($"МЯ: {x[0]}, {x[1]}, {x[2]} {x[3]} ");



        }


        // Проверка на сходимость систем уравнений - проверка достаточного условия сходимости - преобладания диагональных элементов

        static bool Check(double[,] matr)
        {
            for (int i = 0; i < matr.GetLength(0); i++)
            {

                double sum = 0;

                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    if (i != j) sum += Math.Abs(matr[i, j]);
                }

                if (Math.Abs(matr[i, i]) < sum) return false;

            }

            return true;


        }

        public static double Norma(double[] vector)
        {
            double sum = 0;
            foreach (double el in vector) sum += el * el;

            return Math.Sqrt(sum);

        }

        private void start_button_Click(object sender, EventArgs e)
        {
            GraphPane pane = graph.GraphPane;
            pane.CurveList.Clear();

            // берем данные из текстовых вставок

            double[] approxY = new double[(int)countx.Value]; 
            double[] approxD = new double[(int)countx.Value]; 
            int i = 0;
            double[,] matr = new double[(int)countx.Value, (int)countx.Value + 1];

            string d = matrixBox.Text.ToString();

            foreach (string line in d.Split('\n'))
            {
                int j = 0;
                foreach (string item in line.Split(' '))
                {
                    matr[i, j] = Convert.ToDouble(item); j++;
                }
                i++;

            }

            d = vectorsBox.Text.ToString(); i = 0;

            foreach (string line in d.Split('\n'))
            {
                int j = 0;
                foreach (string item in line.Split(' '))
                {
                    if (i == 0) approxY[j]= Convert.ToDouble(item);
                    else approxD[j] = Convert.ToDouble(item);
                    j++;
                }
                i++;

            }

            MethodYakoby(approxY, matr, Convert.ToDouble(valueEps.Text), ref graph);


            DescentMethod(approxD, matr, Convert.ToDouble(valueEps.Text), ref graph);

                         

        }




    }



}
