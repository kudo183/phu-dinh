using System.Linq;
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

            reportDatePicker.NgaySelected += reportDatePicker_NgaySelected;
            reportDatePicker.TuNgayDenNgaySelected += reportDatePicker_TuNgayDenNgaySelected;

            comboReportType.SelectedIndex = 0;
        }

        void reportDatePicker_NgaySelected(object sender, EventArgs e)
        {
            ReportByDate();
        }

        void reportDatePicker_TuNgayDenNgaySelected(object sender, EventArgs e)
        {
            ReportFromDateToDate();
        }

        private void ReportByDate()
        {
            var ngay = reportDatePicker.Ngay.Value.Date;

            var result = ReportNhapHang.FilterByDate(ngay, _reportType);

            reportDatePicker.NgayMsg = string.Format("Tong cong: {0}", result.Sum(p => p.SoLuong).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void ReportFromDateToDate()
        {
            var tuNgay = reportDatePicker.TuNgay.Value.Date;

            var denNgay = reportDatePicker.DenNgay.Value.Date;

            var result = ReportNhapHang.FilterByDate(tuNgay, denNgay, _reportType);

            reportDatePicker.TuNgayDenNgayMsg = string.Format("Tong cong: {0}", result.Sum(p => p.SoLuong).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void comboReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _reportType = (e.AddedItems[0] as ComboBoxItem).Content.ToString();
        }
    }
}
