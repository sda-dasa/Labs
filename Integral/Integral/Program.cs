
Console.WriteLine("Вычисляем интеграл функции sin(ln(x)) на отрезке от [1; 2]");
Console.WriteLine("Введите точность (eps)");
double eps; double.TryParse(Console.ReadLine(), out eps);
if (eps <= 0) Console.WriteLine("Точность не может быть меньше или равна нулю");
else
{
    double f = func_i(2) - func_i(1); int steps1 = 3; int steps2 = 3;

    while (Math.Abs(get_integral(steps1) - f) > eps) steps1 *= 2;
    
    while (Math.Abs(get_integralS(steps2) - f) > eps) steps2 *= 2;


    Console.WriteLine($"Значение интеграла вычисленного методом левых прямоугольников {get_integral(steps1)}");

    Console.WriteLine($"Количество шагов вычисления интеграла методом левых прямоугольников {steps1}");

    Console.WriteLine($"Значение интеграла вычисленного методом Симпсона (парабол) {get_integralS(steps2)}");

    Console.WriteLine($"Количество шагов вычисления интеграла методом Симпсона (парабол) {steps2}");

}

double get_integral(int steps) 
{
    double h = 1.0 / steps; double integral = 0.0;
    for (int i = 0; i < steps - 1; i++)
    {
        integral+=func(1 + i * h);
    }
    
    return integral*h; 

}

double get_integralS(int steps)
{
    double h = 1.0 / (steps); double integral = 0.0;

    for (int i = 0; i < steps - 1; i++)
    {
        integral+=func(1.0 + i * h) + 4 * func(1.0 + i * h + h / 2) + func (1.0 + (i+1) * h);
        
    }
    
    return integral *  h / 6.0;
}

double func_i(double x)
{
    Console.WriteLine("1a11");
   return (Math.Sin(Math.Log(x)) * x - Math.Cos(Math.Log(x)) * x) / 2;
}
double func (double x) =>  Math.Sin(Math.Log(x));

