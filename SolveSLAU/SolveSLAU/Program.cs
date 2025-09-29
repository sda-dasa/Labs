using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolveSLAU
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        // 10   13  1 | 2
        // 4    9   4 | 2
        // 3    8   3 | 3



        // метод Якоби

        static void  MethodYakoby(double[] x, double [,] matr) //  изначальный x - приближенный вектор
        {

            // ПРОВЕРКА НА СХОДИМОСТЬ К РЕШЕНИЮ (ДОСТАТОЧНОЕ УСЛОВИЕ)

            if (Check(matr))


            // меняем матрицу 

            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    matr[i, j] = -matr[i, j] / matr[i, i];
                }




            }











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

                if (Math.Abs(matr[i, i]) <= sum) return false;

            }

            return true;


        }

         









    }



}
