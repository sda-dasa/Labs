
// ввод матрицы и значения точности

Console.WriteLine("Введите размерность матрицы (n)");

int n; int.TryParse(Console.ReadLine(), out n);

Console.WriteLine("Введите матрицу А");

double[,] matrA = new double[n, n];

for (int k = 0; k < n; k++)
    for (int h = 0; h < n; h++) matrA[k, h] = double.Parse(Console.ReadLine());


Console.WriteLine("Введите значение точности");

double eps = double.Parse(Console.ReadLine());

int i;


// метод прямой итерации

double[] vector = new double[n];
for (i = 0; i < n; i++) vector[i] = i;

double[] iter_x = MultiplyProcedure(vector);

double[] iter_x2; double eigen1 = 0; double eigen2 = 0;

if (!Check()) Console.WriteLine("Данная матрица не является симметричной");

else
{
    iter_x = MultiplyProcedure(vector);

    double eigen = 1000;

    i = 1;


    while (Math.Abs(GetEigen(iter_x) - eigen) > eps)
    {
        eigen = GetEigen(iter_x);
        iter_x = MultiplyProcedure(iter_x);
        i++;

    }

    Console.WriteLine($"Собственное значение матрицы А:  {eigen} ");

    Console.WriteLine($"Номер итерации {i}");


}



static double Norma (double[] x)
{
    double sum = 0;
    foreach (double el in x) sum += el * el;

    return Math.Sqrt(sum);


}

double[] MultiplyProcedure(double[] vector1, bool withnorma = true)
{
    double[] res = new double[vector1.Length];
    double norma;

    for (int k = 0; k < n; k++)
        for (int j = 0; j < n; j++) res[k] += matrA[k, j] * vector1[j];

    if (withnorma)
    {
        norma = Norma(vector1);

        for (int k = 0; k < n; k++) res[k] /= norma;

    }

    return res;

}


bool Check()
{
    for (int k = 0; k < matrA.GetLength(0) - 1; k++) 
        for (int j = k + 1; j < matrA.GetLength(1); j++)
            if (matrA[k, j] != matrA[j, k]) return false;
    
    return true;

}




double GetEigen(double[] x1)
{
    double sum1 = 0;

    double[] Ax = MultiplyProcedure(x1, false);

    for (int e = 0; e < Ax.Length; e++) sum1 += Ax[e] * x1[e];

    return Math.Pow(sum1, 1.0 / 3);

}







