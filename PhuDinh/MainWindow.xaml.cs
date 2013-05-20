using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void btnPhuongTien_Click(object sender, RoutedEventArgs e)
        {
            var view = new PhuDinhCommonControl.rPhuongTienView();
            this._brdMain.Child = view;
            view.RefreshGrid();

            this.UpdateToggleButton(sender as ToggleButton);
        }

        private void btnMatHang_Click(object sender, RoutedEventArgs e)
        {
            this._brdMain.Child = null;
            this.UpdateToggleButton(sender as ToggleButton);
        }

        private void btnLoaiChiPhi_Click(object sender, RoutedEventArgs e)
        {
            this._brdMain.Child = null;
            this.UpdateToggleButton(sender as ToggleButton);
        }

        private void UpdateToggleButton(ToggleButton currentButton)
        {
            foreach (ToggleButton btn in this._spMenu.Children)
            {
                btn.IsChecked = btn == currentButton;
            }
        }
    }
}
