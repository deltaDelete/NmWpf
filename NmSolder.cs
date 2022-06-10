using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;
using System.Linq;

namespace NmWpf;
internal class NmSolder {
    public static List<XY> Solve(string fValue, double a, double b, double h, double y0, int precision) {
        double x = a;
        double y = y0;
        double n = Math.Round((b - a) / h, 3);
        if (n % 1 != 0) {
            throw new ArgumentOutOfRangeException(message: "Результат деления отрезка на шаг должен быть целым", null);
        }
        List<XY> result = new();
        result.Add(new(x, y, precision));
        for (int i = 0; i <= n - 1; i++) {
            y = y + h * Function(fValue, x, y);
            x = x + h;
            result.Add(new(
                x,
                y,
                precision
                ));
            if (double.IsNaN(x) || double.IsNaN(y)) {
                throw new Exception("Не число");
            }
        }
        return result;
    }
    public static double Function(string f, double x, double y) {
        string xString = x.ToString().Replace(',', '.');
        string yString = y.ToString().Replace(',', '.');
        string expString = f.ToLower().Replace("x", xString).Replace("y", yString);
        return new Expression(expString).calculate();
    }
    public static List<XY> RKSolve(string fValue, double x0, double y0, double b, double h, double eps, int precision) {
        var l1 = R_K4(fValue, x0, y0, b, h, precision);

        double z = l1.Last().Y;

        h /= 2;

        double p;
        List<XY> l2;
        do {
            l2 = R_K4(fValue, x0, y0, b, h, precision);
            double y = l2.Last().Y;

            p = 1 / 30 * Math.Abs(z - y);

            z = y;

            h /= 2;
        }
        while (p > eps);
        return l2;
    }
    public static List<XY> R_K4(string fValue, double x, double y, double b, double h, int precision) {
        double n = Math.Round((b - x) / h, 1);

        if (n % 1 != 0) {
            throw new ArgumentOutOfRangeException(message: "Результат деления отрезка на шаг должен быть целым", null);
        }

        double r1;
        double r2;
        double r3;
        double r4;

        List<XY> result = new List<XY>();
        result.Add(new XY(x, y, precision));

        for(int i = 1; i <= n; i++) {
            r1 = h * Function(fValue, x, y);
            r2 = h * Function(fValue, x + h / 2, y + r1 / 2);
            r3 = h * Function(fValue, x + h / 2, y + r2 / 2);
            r4 = h * Function(fValue, x + h, y + r3);

            y += (r1 + 2 * r2 + 2 * r3 + r4) / 6;
            x += h;

            result.Add(new XY(x, y, precision));
        }
        return result;
    }
}

public class XY {
    public double X { get; set; }
    public double Y { get; set; }
    public int Precision { get; set; }
    public XY(double x, double y, int precision = 3) {
        Precision = precision;
        X = Math.Round(x, precision);
        Y = Math.Round(y, precision);
    }
    public static explicit operator (double, double)(XY value) => (value.X, value.Y);
    public static explicit operator XY((double, double) value) => new(value.Item1, value.Item2);
    public override string ToString() {
        return $"{Math.Round(X, Precision).ToString()} : {Math.Round(Y, Precision).ToString()}";
    }
}
