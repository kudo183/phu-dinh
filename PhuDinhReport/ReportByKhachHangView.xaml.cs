using System.Linq;
using PhuDinhData.ReportData;
using System;
using System.Windows.Controls;

namespace PhuDinhReport
{
    /// <summary>
    /// Interaction logic for ReportByKhachHangView.xaml
    /// </summary>
    public partial class ReportByKhachHangView : UserControl
    {
        public ReportByKhachHangView()
        {
            InitializeComponent();

            reportDatePicker.NgaySelected += reportDatePicker_NgaySelected;
            reportDatePicker.TuNgayDenNgaySelected += reportDatePicker_TuNgayDenNgaySelected;
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

            var result = ReportByKhachHang.FilterByDate(ngay);

            reportDatePicker.NgayMsg = string.Format("Tong so cuon: {0}", result.Sum(p => p.SoLuong).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void ReportFromDateToDate()
        {
            var tuNgay = reportDatePicker.TuNgay.Value.Date;

            var denNgay = reportDatePicker.DenNgay.Value.Date;

            var result = ReportByKhachHang.FilterByDate(tuNgay, denNgay);

            reportDatePicker.TuNgayDenNgayMsg = string.Format("Tong so cuon: {0}", result.Sum(p => p.SoLuong).ToString("N0"));

            dg.ItemsSource = result;
        }
    }
}
