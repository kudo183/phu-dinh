using System.Linq;
using PhuDinhData.ReportData;
using System;
using System.Windows.Controls;

namespace PhuDinhReport
{
    /// <summary>
    /// Interaction logic for ReportByNhapNguyenLieuView.xaml
    /// </summary>
    public partial class ReportByNhapNguyenLieuView : UserControl
    {
        public ReportByNhapNguyenLieuView()
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
            
            var result = ReportByNhapNguyenLieu.FilterByDate(ngay);

            reportDatePicker.NgayMsg = string.Format("Tong cong: {0}", result.Sum(p => p.SoLuong).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void ReportFromDateToDate()
        {
            var tuNgay = reportDatePicker.TuNgay.Value.Date;

            var denNgay = reportDatePicker.DenNgay.Value.Date;

            var result = ReportByNhapNguyenLieu.FilterByDate(tuNgay, denNgay);

            reportDatePicker.TuNgayDenNgayMsg = string.Format("Tong cong: {0}", result.Sum(p => p.SoLuong).ToString("N0"));

            dg.ItemsSource = result;
        }
    }
}
