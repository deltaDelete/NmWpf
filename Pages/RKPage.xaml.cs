using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NmWpf.Pages;
/// <summary>
/// Логика взаимодействия для RKPage.xaml
/// </summary>
public partial class RKPage : Page {
    public string FValue {
        get {
            return FValueBox.Text;
        }
        set {
            FValueBox.Text = value;
        }
    }
    public double AValue {
        get {
            return AValueBox.Value;
        }
        set {
            AValueBox.Value = value;
        }
    }
    public double BValue {
        get {
            return BValueBox.Value;
        }
        set {
            BValueBox.Value = value;
        }
    }
    public double HValue {
        get {
            return HValueBox.Value;
        }
        set {
            HValueBox.Value = value;
        }
    }
    public double Y0Value {
        get {
            return Y0ValueBox.Value;
        }
        set {
            Y0ValueBox.Value = value;
        }
    }
    public double EpsValue {
        get {
            return EpsValueBox.Value;
        }
        set {
            EpsValueBox.Value = value;
        }
    }
    public int PrecisionValue {
        get {
            return Convert.ToInt32(PrecisionValueBox.Value);
        }
        set {
            Y0ValueBox.Value = value;
        }
    }
    public IEnumerable<XY> Result {
        set {
            ResultTable.ItemsSource = value;
        }
    }
    public RKPage() {
        InitializeComponent();
        DataContext = this;
        PropertyChanged += HomePage_PropertyChanged;
    }
    private void HomePage_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Button_Click(object sender, RoutedEventArgs e) {
        try {
            List<XY> r = NmSolder.RKSolve(FValue, AValue, Y0Value, BValue, HValue, EpsValue, PrecisionValue);
            List<double> vs = new List<double>();
            for (double i = AValue; i <= BValue; i += HValue) {
                vs.Add(Math.Round(i, PrecisionValue));
            }

            Result = r.Where(x => vs.Contains(x.X));
        }
        catch (Exception exc) {
            WPFUI.Controls.MessageBox mbox = new WPFUI.Controls.MessageBox();
            mbox.ResizeMode = ResizeMode.NoResize;
            mbox.Content = "Произошла ошибка,\nпроверьте корректность введенных значений";
            mbox.ButtonRightName = "Ладно";
            mbox.ButtonRightAppearance = WPFUI.Common.Appearance.Primary;
            mbox.ButtonRightClick += (sender, e) => {
                if (sender is WPFUI.Controls.MessageBox && sender is not null)
                    (sender as WPFUI.Controls.MessageBox).Close();
            };
            mbox.ButtonLeftName = "Не ладно";
            mbox.ButtonLeftAppearance = WPFUI.Common.Appearance.Danger;
            mbox.ButtonLeftClick += (sender, e) => {
                if (sender is WPFUI.Controls.MessageBox && sender is not null)
                    (sender as WPFUI.Controls.MessageBox).Close();
            };
            mbox.Show();
        }
    }

    private void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
        e.Handled = !e.Text.All(c => {
            if (c >= '0' && c <= '9')
                return true;
            if (c == ',')
                return true;
            return false;
        });
    }
}
