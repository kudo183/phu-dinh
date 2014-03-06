using PhuDinhData.ReportData;
using System;
using System.Windows.Controls;

namespace PhuDinhReport
{
    /// <summary>
    /// Interaction logic for ReportByMatHangView.xaml
    /// </summary>
    public partial class ReportByMatHangView : UserControl
    {
        public ReportByMatHangView()
        {
            InitializeComponent();

            var now = DateTime.Now;
            dpNgay.SelectedDate = now;
            dpTuNgay.SelectedDate = now;
            dpDenNgay.SelectedDate = now;
        }

        private void btnNgay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ngay = dpNgay.SelectedDate.Value.Date;

            dg.ItemsSource = ReportByMatHang.FilterByDate(ngay);
        }

        private void btnTuNgayDenNgay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var tuNgay = dpTuNgay.SelectedDate.Value.Date;

            var denNgay = dpDenNgay.SelectedDate.Value.Date;

            dg.ItemsSource = ReportByMatHang.FilterByDate(tuNgay, denNgay);
        }
    }
}
