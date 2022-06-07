global using System;
global using System.Linq;
global using System.Collections.Generic;
using org.mariuszgromada.math.mxparser;
//using NCalc;


//string es = WriteRead("Введите функцию");
//var e = new Expression(es);
//double h = ParseDouble(WriteRead("Введите шаг"));
//double y = ParseDouble(WriteRead("Введите y"));
//double a = ParseDouble(WriteRead("Введите a"));
//double b = ParseDouble(WriteRead("Введите b"));

//double x = a;
//do {
//    e.Parameters["x"] = x;
//    Console.WriteLine($"{x} : {y}");
//    y = y + h * (double)e.Evaluate();
//    x += h;
//}
//while (x > b);


////if (es.Contains('x')) {
////    e.Parameters["x"] = ParseDouble(WriteRead("Введите значение параметра x"));
////}
////if (es.Contains('y')) {
////    e.Parameters["y"] = ParseDouble(WriteRead("Введите значение параметра y"));
////}
//Console.WriteLine(e.Evaluate());
//Console.WriteLine(2^2);

//Expression e = new("fh4t34sg");
//double value = e.calculate();
//Console.WriteLine(value);

string exp = "sqrt(x)+sqrt(y)";
double x = 0;
exp = exp.Replace("x", x).Replace("y","2.3");
Expression f = new(exp);
Console.WriteLine(f.calculate());



static string? WriteRead(string text) {
    Console.Write($"{text}\n> ");
    return Console.ReadLine();
}
static double ParseDouble(string? text, string NaNtext = "[Ошибка] Не число") {
    if (!double.TryParse(text, out double number)) {
        Console.WriteLine(NaNtext);
        throw new Exception();
    }
    return number;
}