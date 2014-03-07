using PhuDinhData.ReportData;
using System;
using System.Windows.Controls;

namespace PhuDinhReport
{
    /// <summary>
    /// Interaction logic for ReportByLoaiHangView.xaml
    /// </summary>
    public partial class ReportByLoaiHangView : UserControl
    {
        public ReportByLoaiHangView()
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

            dg.ItemsSource = ReportByLoaiHang.FilterByDate(ngay);
        }

        private void btnTuNgayDenNgay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var tuNgay = dpTuNgay.SelectedDate.Value.Date;

            var denNgay = dpDenNgay.SelectedDate.Value.Date;

            dg.ItemsSource = ReportByLoaiHang.FilterByDate(tuNgay, denNgay);
        }
    }
}
