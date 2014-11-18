using System.Collections.Generic;
using System.Linq.Expressions;
using PhuDinhData;
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

            dpNgay.dp.SelectedDateChanged += dp_SelectedDateChanged;

            var now = DateTime.Now;
            dpNgay.dp.SelectedDate = now;
            dpTuNgay.dp.SelectedDate = now;
            dpDenNgay.dp.SelectedDate = now;

            dg.SelectionChanged += dg_SelectionChanged;
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
                    var ngay = dpNgay.dp.SelectedDate.Value.Date;

                    filter = p => p.tDonHang.Ngay == ngay && p.tMatHang.MaLoai == maLoai;
                    break;
                case 1:
                    var tuNgay = dpTuNgay.dp.SelectedDate.Value.Date;
                    var denNgay = dpDenNgay.dp.SelectedDate.Value.Date;

                    filter = p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay && p.tMatHang.MaLoai == maLoai;
                    break;
            }

            dgDetail.ItemsSource = ReportByMatHang.Filter(filter);
        }

        void dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ReportByDate();
        }

        private void ReportByDate()
        {
            _type = 0;

            var ngay = dpNgay.dp.SelectedDate.Value.Date;

            dg.ItemsSource = ReportByLoaiHang.FilterByDate(ngay);
        }

        private void btnTuNgayDenNgay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ReportFromDateToDate();
        }

        private void ReportFromDateToDate()
        {
            _type = 1;

            var tuNgay = dpTuNgay.dp.SelectedDate.Value.Date;

            var denNgay = dpDenNgay.dp.SelectedDate.Value.Date;

            dg.ItemsSource = ReportByLoaiHang.FilterByDate(tuNgay, denNgay);
        }
    }
}
