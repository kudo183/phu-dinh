using System.Linq.Expressions;
using PhuDinhData.ReportData;
using System;
using System.Linq;
using System.Windows.Controls;

namespace PhuDinhReport
{
    /// <summary>
    /// Interaction logic for ReportByChiPhiView.xaml
    /// </summary>
    public partial class ReportByChiPhiView : UserControl
    {
        private int _type = 0;
        public ReportByChiPhiView()
        {
            InitializeComponent();

            reportDatePicker.NgaySelected += reportDatePicker_NgaySelected;
            reportDatePicker.TuNgayDenNgaySelected += reportDatePicker_TuNgayDenNgaySelected;

            dg.SelectionChanged += dg_SelectionChanged;
        }

        void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            Expression<Func<PhuDinhDataEntity.tChiPhi, bool>> filter = null;

            var maLoai = (e.AddedItems[0] as ReportByChiPhi.ReportByChiPhiData).MaLoaiChiPhi;

            switch (_type)
            {
                case 0:
                    var ngay = reportDatePicker.Ngay.Value.Date;

                    filter = p => p.Ngay == ngay && p.MaLoaiChiPhi == maLoai;
                    break;
                case 1:
                    var tuNgay = reportDatePicker.TuNgay.Value.Date;
                    var denNgay = reportDatePicker.DenNgay.Value.Date;

                    filter = p => p.Ngay >= tuNgay && p.Ngay <= denNgay && p.MaLoaiChiPhi == maLoai;
                    break;
            }

            dgDetail.ItemsSource = ReportByChiPhi.FilterDetail(filter);
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
            _type = 0;

            var ngay = reportDatePicker.Ngay.Value.Date;

            var result = ReportByChiPhi.FilterByDate(ngay);

            reportDatePicker.NgayMsg = string.Format("Tong so tien: {0}", result.Sum(p => p.SoTien).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void ReportFromDateToDate()
        {
            _type = 1;

            var tuNgay = reportDatePicker.TuNgay.Value.Date;

            var denNgay = reportDatePicker.DenNgay.Value.Date;

            var result = ReportByChiPhi.FilterByDate(tuNgay, denNgay);

            reportDatePicker.TuNgayDenNgayMsg = string.Format("Tong so tien: {0}", result.Sum(p => p.SoTien).ToString("N0"));

            dg.ItemsSource = result;
        }
    }
}
