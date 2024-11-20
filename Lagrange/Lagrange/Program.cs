using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lagrange
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            // ввод n
            // выбор функции (полином 4 степени, ln20x^2+14x+2, |x|, 12^x*10^lnx, exp^(-kx^2), 1/(1+25x^2))





        }
    }
}
