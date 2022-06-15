using System;
using System.Collections.Generic;
using System.Globalization;
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
using WPFUI.Appearance;

namespace NmWpf;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
        Loaded += (sender , e) => Theme.Apply(GetThemeTypeReversed(), GetBackgroundType(), true, true);
        NumberFormatInfo.CurrentInfo.NumberDecimalSeparator = ",";
    }
    private void NavigationButtonTheme_OnClick(object sender, RoutedEventArgs e) {
        Theme.Apply(GetThemeTypeReversed(), GetBackgroundType(), true, true);
    }
    private BackgroundType GetBackgroundType() {
        if (Environment.OSVersion.Version.Build >= 22000) {
            return BackgroundType.Mica;
        }
        return BackgroundType.Auto;
    }
    private static ThemeType GetThemeTypeReversed() {
        if (Environment.OSVersion.Version.Build >= 10240 && Theme.GetAppTheme() == ThemeType.Light) {
            return ThemeType.Dark;
        }
        return ThemeType.Light;
    }
}