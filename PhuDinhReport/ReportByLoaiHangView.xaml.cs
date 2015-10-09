using System.Linq;
using System.Linq.Expressions;
using PhuDinhDataEntity;
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
        private int _type = 0;
        public ReportByLoaiHangView()
        {
            InitializeComponent();

            reportDatePicker.NgaySelected += reportDatePicker_NgaySelected;
            reportDatePicker.TuNgayDenNgaySelected += reportDatePicker_TuNgayDenNgaySelected;

            dg.SelectionChanged += dg_SelectionChanged;
        }

        void reportDatePicker_NgaySelected(object sender, EventArgs e)
        {
            ReportByDate();
        }

        void reportDatePicker_TuNgayDenNgaySelected(object sender, EventArgs e)
        {
            ReportFromDateToDate();
        }

        void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            Expression<Func<tChiTietDonHang, bool>> filter = null;

            var maLoai = (e.AddedItems[0] as ReportByLoaiHang.ReportByLoaiHangData).MaLoaiHang;

            switch (_type)
            {
                case 0:
                    var ngay = reportDatePicker.Ngay.Value.Date;

                    filter = p => p.tDonHang.Ngay == ngay && p.tMatHang.MaLoai == maLoai;
                    break;
                case 1:
                    var tuNgay = reportDatePicker.TuNgay.Value.Date;
                    var denNgay = reportDatePicker.DenNgay.Value.Date;

                    filter = p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay && p.tMatHang.MaLoai == maLoai;
                    break;
            }

            dgDetail.ItemsSource = ReportByMatHang.Filter(filter);
        }

        private void ReportByDate()
        {
            _type = 0;

            var ngay = reportDatePicker.Ngay.Value.Date;

            var result = ReportByLoaiHang.FilterByDate(ngay);

            reportDatePicker.NgayMsg = string.Format("Tong so cuon: {0} Tong so ky: {1} kg"
                , result.Sum(p => p.SoLuong).ToString("N0")
                , result.Sum(p => p.SoKy).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void ReportFromDateToDate()
        {
            _type = 1;

            var tuNgay = reportDatePicker.TuNgay.Value.Date;

            var denNgay = reportDatePicker.DenNgay.Value.Date;

            var result = ReportByLoaiHang.FilterByDate(tuNgay, denNgay);

            reportDatePicker.TuNgayDenNgayMsg = string.Format("Tong so cuon: {0} Tong so ky: {1} kg"
                , result.Sum(p => p.SoLuong).ToString("N0")
                , result.Sum(p => p.SoKy).ToString("N0"));

            dg.ItemsSource = result;
        }
    }
}
