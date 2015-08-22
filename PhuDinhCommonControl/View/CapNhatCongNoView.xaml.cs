using PhuDinhData;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for CapNhatCongNo.xaml
    /// </summary>
    public partial class CapNhatCongNoView : UserControl
    {
        public CapNhatCongNoView()
        {
            InitializeComponent();

            Loaded += CapNhatCongNoView_Loaded;
        }

        void CapNhatCongNoView_Loaded(object sender, RoutedEventArgs e)
        {
            dp.SelectedDate = DateTime.Now.Date;
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            CongNoManager.UpdateCongNo(dp.SelectedDate.Value, DateTime.Now.Date);
        }
    }
}
