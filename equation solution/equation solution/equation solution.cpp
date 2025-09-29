// equation solution.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <Windows.h>
#include <omp.h>
#include <cmath>
#include <vector>
#include <math.h>
#include <chrono>



#include <iostream>
#include <stack>
#include <string>
#include <cmath>
#include <cctype>

std::string expression;

class RPN {
private:
    // Метод возвращает true, если проверяемый символ - разделитель
    static bool IsDelimeter(char c) {
        return (std::string(" =").find(c) != std::string::npos);
    }

    // Метод возвращает true, если проверяемый символ - оператор
    static bool IsOperator(char c) {
        return (std::string("+-/*^()LCSKTDR|").find(c) != std::string::npos);
    }

    // Метод возвращает приоритет оператора
    static unsigned char GetPriority(char s) {
        switch (s) {
        case '(': return 0;
        case ')': return 1;
        case '+': return 2;
        case '-': return 3;
        case '*': return 4;
        case '/': return 5;
        case '^': return 6;
        case 'L': return 7;
        case 'C': return 8;
        case 'S': return 9;
        case 'T': return 10;
        case 'K': return 11;
        case 'R': return 12;
        case 'D': return 13;
        case '|': return 14;
        default:  return 15;
        }
    }

public:
    // Основной метод вычисления
    static double Calculate(const std::string& input) {
        std::string output = GetExpression(input);
        double x = Counting(output);
        if (isnormal(x) && isfinite(x) ||x==0) return x;
        else throw - 1;
    }

private:
    // Преобразование в обратную польскую запись
    static std::string GetExpression(const std::string& input) {
        std::string output;
        std::stack<char> operStack;

        for (size_t i = 0; i < input.length(); i++) {
            if (IsDelimeter(input[i]))
                continue;

            if (std::isdigit(input[i])) {
                while (i < input.length() && !IsDelimeter(input[i]) && !IsOperator(input[i])) {
                    output += input[i++];
                }
                output += ' ';
                i--;
            }

            if (IsOperator(input[i])) {
                if (input[i] == '(') {
                    operStack.push(input[i]);
                }
                else if (input[i] == ')') {
                    char s = operStack.top();
                    operStack.pop();
                    while (s != '(') {
                        output += s;
                        output += ' ';
                        s = operStack.top();
                        operStack.pop();
                    }
                }
                else {
                    while (!operStack.empty() &&
                        GetPriority(input[i]) <= GetPriority(operStack.top())) {
                        output += operStack.top();
                        output += ' ';
                        operStack.pop();
                    }
                    operStack.push(input[i]);
                }
            }
        }

        while (!operStack.empty()) {
            output += operStack.top();
            output += ' ';
            operStack.pop();
        }
        return output;
    }

    // Вычисление выражения в ОПЗ
    static double Counting(const std::string& input) {
        std::stack<double> temp;

        for (size_t i = 0; i < input.length(); i++) {
            if (std::isdigit(input[i])) {
                std::string a;
                while (i < input.length() && !IsDelimeter(input[i]) && !IsOperator(input[i])) {
                    a += input[i++];
                }
                temp.push(std::stod(a));
                i--;
            }
            else if (IsOperator(input[i])) {
                double a = temp.top();
                temp.pop();
                double b = 0;
                double result = 0;

                if (!(input[i] == 'L' || input[i] == 'C' || input[i] == 'S' ||
                    input[i] == 'T' || input[i] == 'K' || input[i] == 'D' || input[i] == 'R' || input[i] == '|')) {
                    b = temp.top();
                    temp.pop();
                }

                switch (input[i]) {
                case '+': result = b + a; break;
                case '-': result = b - a; break;
                case '*': result = b * a; break;
                case '/': result = b / a; break;
                case '^': result = std::pow(b, a); break;
                case 'L': result = std::log(a); break;
                case 'C': result = std::cos(a); break;
                case 'S': result = std::sin(a); break;
                case 'T': result = std::tan(a); break;
                case 'R': result = std::sqrt(a); break;
                case 'K': result = 1.0 / std::tan(a); break;
                case 'D': result = std::log10(a); break;
                case'|': result = a * (-1); break;
                }
                temp.push(result);
            }
        }
        return temp.top();
    }
};


std::string replace(std::string a)
{
    std::string new_str = "";
    if (!(a[0] != '-')) a[0] = '|';
    for (int i = 0; i < expression.size(); i++)
    {
        if (expression[i] == 'x') new_str += "(" + a + ")";
        else new_str += expression[i];
    }
    return new_str;
}

// Функция f(x), которую мы интегрируем
double f(double x) {
    std::string _x = std::to_string(x);
    std::string curr_expr = replace(_x);
    double result = RPN::Calculate(curr_expr);

    if (!isnan(RPN::Calculate(curr_expr))) return result;
    else throw - 1;
}

// Вычисление интеграла методом Симпсона (параллельно)
double simpson_integral(double a, double x_n, int n_threads = 10) {
    const int n = 100; // Число разбиений
    double h = (x_n - a) / (2.0 * n);
    double sum_odd = 0.0, sum_even = 0.0;

    // Параллельное вычисление f(x_i) для нечётных и чётных точек
    #pragma omp parallel num_threads(n_threads)
    {
        double local_odd = 0.0, local_even = 0.0;

        #pragma omp for
        for (int i = 1; i <= n; ++i) {
            double x_odd = a + (2 * i - 1) * h;
            local_odd += f(x_odd);
        }

        #pragma omp for
        for (int i = 1; i < n; ++i) {
            double x_even = a + 2 * i * h;
            local_even += f(x_even);
        }

        #pragma omp critical
        {
            sum_odd += local_odd;
            sum_even += local_even;
        }
    }

    double integral = (h / 3.0) * (f(a) + 4.0 * sum_odd + 2.0 * sum_even + f(x_n));
    return integral;
}

// Метод Ньютона для решения g(x) = 0
double newton_method(double a, double b, double eps, double x_prev, int max_iter = 15) {
    
    double x_next = x_prev;
    try
    {
        for (int i = 0; i < max_iter; ++i) {
            double integral = simpson_integral(a, x_prev);
            double g = integral - b;
            double df = f(x_prev); // Производная g'(x) = f(x)

            x_next = x_prev - g / df;

            if (std::abs(x_next - x_prev) < eps) {
                std::cout << "Решение найдено за " << i + 1 << " итераций.\n";
                return x_next;
            }            
            x_prev = x_next;
        }
        std::cerr << "Достигнуто максимальное число итераций!\n";
        return x_next;
    }
    catch (...)
    {
        throw "Проверьте значения параметра a и функции f(x)!";
    }
    

    
}



boolean isnum(std::string a)
{
    if (a[0] != '-' && !std::isdigit(a[0])) return false;
    for (int i = 1; i < a.size(); i++)
        if (!std::isdigit(a[i]) && ! a[i]==',' && ! a[i]=='.') return false;
    return true;
}


int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    boolean smth_wrong = 1;
    std::string a_, b_;
    double a=0, b, eps, x_0;
    std::string curr_expr;
    do
    {
        std::cout << "Обозначение элементарных функций:\n1)L-ln\n2)C-cos\n3)T-tg\n4)K-ctg\n5)D-log10\n6)S-sin\nВведите подынтегральную функцию:";
        std::cin >> expression;
           
        std::cout << "Введите a (нижний предел интегрирования): ";
        std::cin >> a_;
        try {
            if (isnum(a_)) { curr_expr = replace(a_); RPN::Calculate(curr_expr); a = std::stod(a_); }
            else { std::cout << "Проверьте правильность ввода a";continue; }
        }
        catch (...)
        {
            smth_wrong = 1; std::cout << "Данное значение не вычисляется выбранной функцией" << std::endl; continue;
        }
        std::cout << "Введите b (целевое значение интеграла): ";
        std::cin >> b_;
        try {
            if (isnum(b_)) { curr_expr = replace(b_); RPN::Calculate(curr_expr); b = std::stod(b_); }
            else { std::cout << "Проверьте правильность ввода b"; continue; }
        }
        catch (...)
        {
            smth_wrong = 1; std::cout << "Данное значение не вычисляется выбранной функцией" << std::endl; continue;
        }
        {
            double h = min(0.1 * b, 1.0); // Шаг для оценки f_avg
            double f_avg = (f(a) + f(a + h)) / 2.0;
            double x_prev = a + b / f_avg; // Начальное приближение
            std::cout << "Предлагаемое начальное приближение: " << x_prev << " Использовать? (Y/N)";
            std::string answ = ""; std::cin >> answ; 
            if (answ == "Y") x_0 = x_prev; 
            else {
                std::cout << "Введите начальное приближение: ";
                std::cin >> x_0;
            }
            
        }
        
        std::cout << "Введите eps (точность): ";
        std::cin >> eps;
        if (eps <= 0) {
            smth_wrong = 1; std::cout << "Значение точности должно быть положительным" << std::endl; continue;
        }
        smth_wrong = 0;
    } 
    while (smth_wrong);

    try {
        auto start = std::chrono::high_resolution_clock::now(); // начало замера
        double root = newton_method(a, b, eps, x_0);
        auto end = std::chrono::high_resolution_clock::now(); // конец замера
        std::chrono::duration<double> duration = end - start; // вычисление длительности      
        std::cout << "Найденный корень: " << root << std::endl;
        std::cout << "Время вычисления корня: " << duration.count() << " секунд" << std::endl;
    }
    catch (...)
    {
        std::cout << "Проверьте правильность введеной функции f(x)" << std::endl;
    }
    return 0;
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
