using System.Windows;
using PhuDinh.View;

namespace PhuDinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBaiXe_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = new BaiXeView();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = new AdminView();
        }
    }
}
