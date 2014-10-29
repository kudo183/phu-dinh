using System.Linq;
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

            dpNgay.dp.SelectedDateChanged += dp_SelectedDateChanged;

            var now = DateTime.Now;
            dpNgay.dp.SelectedDate = now;
            dpTuNgay.dp.SelectedDate = now;
            dpDenNgay.dp.SelectedDate = now;
        }

        void dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ReportByDate();
        }

        private void ReportByDate()
        {
            var ngay = dpNgay.dp.SelectedDate.Value.Date;

            var result = ReportByMatHang.FilterByDate(ngay);

            txtMsg.Text = result.Sum(p => p.SoLuong).ToString("N0");

            dg.ItemsSource = result;
        }

        private void btnTuNgayDenNgay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ReportFromDateToDate();
        }

        private void ReportFromDateToDate()
        {
            var tuNgay = dpTuNgay.dp.SelectedDate.Value.Date;

            var denNgay = dpDenNgay.dp.SelectedDate.Value.Date;

            var result = ReportByMatHang.FilterByDate(tuNgay, denNgay);

            txtMsg.Text = result.Sum(p => p.SoLuong).ToString("N0");

            dg.ItemsSource = result;
        }
    }
}
