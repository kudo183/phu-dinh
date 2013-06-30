using System.Windows;

namespace PhuDinhCommonControl
{
    public static class ChildWindowUtils
    {
        public static void ShowChildWindow(string title, BaseView view)
        {
            var w = new Window { Title = title, Content = view, WindowStartupLocation = WindowStartupLocation.CenterScreen};
            w.ShowDialog();
        }
    }
}
