using System.Windows;
using System.Windows.Controls;

namespace PhuDinh
{
    /// <summary>
    /// Interaction logic for ButtonPage.xaml
    /// </summary>
    public partial class ButtonPage : UserControl
    {
        public ButtonPage()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var button = (e.OriginalSource as Button);
            if (button == null)
            {
                return;
            }

            var w = new Window
            {
                Title = button.Content.ToString(),
                Content = button.Tag,
                WindowState = WindowState.Maximized
            };
            w.Show();
        }
    }
}
