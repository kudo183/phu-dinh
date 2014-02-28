using PhuDinhData;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for CapNhatTonKho.xaml
    /// </summary>
    public partial class CapNhatTonKhoView : UserControl
    {
        public CapNhatTonKhoView()
        {
            InitializeComponent();

            Loaded += CapNhatTonKhoView_Loaded;
        }

        void CapNhatTonKhoView_Loaded(object sender, RoutedEventArgs e)
        {
            dp.SelectedDate = DateTime.Now.Date;
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            TonKhoManager.UpdateTonKho(dp.SelectedDate.Value, DateTime.Now.Date);
        }
    }
}
