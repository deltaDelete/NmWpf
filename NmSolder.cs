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
        double n = Math.Round((b - a) / h, 1);
        if (n % 1 != 0) {
            throw new ArgumentOutOfRangeException(message: "Результат деления отрезка на шаг должен быть целым", null);
        }
        List<XY> result = new();
        result.Add(new(x, y, precision));
        for (int i = 0; i <= n - 1; i++) {
            string xString = x.ToString().Replace(',', '.');
            string yString = y.ToString().Replace(',', '.');
            string expString = fValue.Replace("x", xString).Replace("y", yString);
            Expression exp = new(expString);
            y = y + h * exp.calculate();
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
