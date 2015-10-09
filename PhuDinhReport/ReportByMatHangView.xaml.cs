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

            var result = ReportByMatHang.FilterByDate(ngay);

            reportDatePicker.NgayMsg = string.Format("Tong so cuon: {0} Tong so ky: {1} kg"
                , result.Sum(p => p.SoLuong).ToString("N0")
                , result.Sum(p => p.SoKy).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void ReportFromDateToDate()
        {
            var tuNgay = reportDatePicker.TuNgay.Value.Date;

            var denNgay = reportDatePicker.DenNgay.Value.Date;

            var result = ReportByMatHang.FilterByDate(tuNgay, denNgay);

            reportDatePicker.TuNgayDenNgayMsg = string.Format("Tong so cuon: {0} Tong so ky: {1} kg"
                , result.Sum(p => p.SoLuong).ToString("N0")
                , result.Sum(p => p.SoKy).ToString("N0"));

            dg.ItemsSource = result;
        }
    }
}
