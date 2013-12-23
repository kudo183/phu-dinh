using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    public static class ChildWindowUtils
    {
        public static void ShowChildWindow(string title, UserControl view)
        {
            var w = new Window { Title = title, Content = view, WindowStartupLocation = WindowStartupLocation.CenterScreen };
            w.ShowDialog();
        }
    }
}
