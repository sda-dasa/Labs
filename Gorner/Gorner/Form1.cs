﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Gorner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }


        private int[] coefficients = { 1, -18, 144, -672, 2016, -4032, 5376, -4608, 2304, -512 };

        private double Gorner (double arg)
        { 
            double p = (double) coefficients[0]; 
            for (int i = 1; i <= coefficients.Length - 1; i++) p = arg * p + coefficients[i]; 
            return p; 
        }

        private double Standart (double arg)
        {
            double res = 1.0; for (int i = 1; i <= 9; i++) res *= (arg - 2); return res;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphPane pane = Graphic.GraphPane;
            pane.Title.Text = "Значения многочлена (х - 2)^9, \n вычисленные с помощью схемы Горнера и  с помощью формулы";
            pane.XAxis.Title.Text = "Ось Х";
            pane.YAxis.Title.Text = "Ось Y";
            
            pane.CurveList.Clear();

            double x_min = 1.92, x_max = 2.08;

            PointPairList gorner_func = new PointPairList();
            for (double x = x_min; x <= x_max; x += 0.0001) gorner_func.Add(x, Gorner(x));

            PointPairList standart_func = new PointPairList();
            for (double x = x_min; x <= x_max; x += 0.0001) standart_func.Add(x, Standart(x));

            
            LineItem line_2 = pane.AddCurve(
                "Результат вычисления по формуле", standart_func, Color.Red, SymbolType.None);
            LineItem line_1 = pane.AddCurve(
                "Результат по схеме Горнера", gorner_func, Color.Blue, SymbolType.None);

            Graphic.AxisChange(); Graphic.Invalidate();

        }
    }
}
