using PhuDinhData.ReportData;
using System;
using System.Windows.Controls;

namespace PhuDinhReport
{
    /// <summary>
    /// Interaction logic for ReportNhapHangView.xaml
    /// </summary>
    public partial class ReportNhapHangView : UserControl
    {
        private string _reportType = ReportNhapHang.ReportType_TuLam;

        public ReportNhapHangView()
        {
            InitializeComponent();

            dpNgay.dp.SelectedDateChanged += dp_SelectedDateChanged;

            var now = DateTime.Now;
            dpNgay.dp.SelectedDate = now;
            dpTuNgay.dp.SelectedDate = now;
            dpDenNgay.dp.SelectedDate = now;

            comboReportType.SelectedIndex = 0;
        }

        void dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ReportByDate();
        }

        private void ReportByDate()
        {
            var ngay = dpNgay.dp.SelectedDate.Value.Date;

            dg.ItemsSource = ReportNhapHang.FilterByDate(ngay, _reportType);
        }

        private void btnTuNgayDenNgay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ReportFromDateToDate();
        }

        private void ReportFromDateToDate()
        {
            var tuNgay = dpTuNgay.dp.SelectedDate.Value.Date;

            var denNgay = dpDenNgay.dp.SelectedDate.Value.Date;

            dg.ItemsSource = ReportNhapHang.FilterByDate(tuNgay, denNgay, _reportType);
        }

        private void comboReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _reportType = (e.AddedItems[0] as ComboBoxItem).Content.ToString();
        }
    }
}
