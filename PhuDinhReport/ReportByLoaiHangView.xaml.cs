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
                    switch (comboLoaiKhachHang.SelectionBoxItem.ToString())
                    {
                        case "Khach Thuong":
                            filter = p => p.tDonHang.Ngay == ngay && p.tMatHang.MaLoai == maLoai && p.tDonHang.rKhachHang.KhachRieng == false;
                            break;
                        case "Khach Rieng":
                            filter = p => p.tDonHang.Ngay == ngay && p.tMatHang.MaLoai == maLoai && p.tDonHang.rKhachHang.KhachRieng == true;
                            break;
                        case "Tat Ca":
                            filter = p => p.tDonHang.Ngay == ngay && p.tMatHang.MaLoai == maLoai;
                            break;
                    }
                    break;
                case 1:
                    var tuNgay = reportDatePicker.TuNgay.Value.Date;
                    var denNgay = reportDatePicker.DenNgay.Value.Date;

                    switch (comboLoaiKhachHang.SelectionBoxItem.ToString())
                    {
                        case "Khach Thuong":
                            filter = p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay && p.tMatHang.MaLoai == maLoai && p.tDonHang.rKhachHang.KhachRieng == false;
                            break;
                        case "Khach Rieng":
                            filter = p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay && p.tMatHang.MaLoai == maLoai && p.tDonHang.rKhachHang.KhachRieng == true;
                            break;
                        case "Tat Ca":
                            filter = p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay && p.tMatHang.MaLoai == maLoai;
                            break;
                    }

                    break;
            }

            dgDetail.ItemsSource = ReportByMatHang.Filter(filter);
        }

        private void ReportByDate()
        {
            _type = 0;
            Expression<Func<tChiTietDonHang, bool>> filter = null;
            var ngay = reportDatePicker.Ngay.Value.Date;
            switch (comboLoaiKhachHang.SelectionBoxItem.ToString())
            {
                case "Khach Thuong":
                    filter = p => p.tDonHang.Ngay == ngay && p.tDonHang.rKhachHang.KhachRieng == false;
                    break;
                case "Khach Rieng":
                    filter = p => p.tDonHang.Ngay == ngay && p.tDonHang.rKhachHang.KhachRieng == true;
                    break;
                case "Tat Ca":
                    filter = p => p.tDonHang.Ngay == ngay;
                    break;
            }
            var result = ReportByLoaiHang.Filter(filter);

            reportDatePicker.NgayMsg = string.Format("Tong so cuon: {0} Tong so ky: {1} kg"
                , result.Sum(p => p.SoLuong).ToString("N0")
                , result.Sum(p => p.SoKy).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void ReportFromDateToDate()
        {
            _type = 1;
            Expression<Func<tChiTietDonHang, bool>> filter = null;
            var tuNgay = reportDatePicker.TuNgay.Value.Date;

            var denNgay = reportDatePicker.DenNgay.Value.Date;

            switch (comboLoaiKhachHang.SelectionBoxItem.ToString())
            {
                case "Khach Thuong":
                    filter = p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay && p.tDonHang.rKhachHang.KhachRieng == false;
                    break;
                case "Khach Rieng":
                    filter = p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay && p.tDonHang.rKhachHang.KhachRieng == true;
                    break;
                case "Tat Ca":
                    filter = p => p.tDonHang.Ngay >= tuNgay && p.tDonHang.Ngay <= denNgay;
                    break;
            }
            var result = ReportByLoaiHang.Filter(filter);

            reportDatePicker.TuNgayDenNgayMsg = string.Format("Tong so cuon: {0} Tong so ky: {1} kg"
                , result.Sum(p => p.SoLuong).ToString("N0")
                , result.Sum(p => p.SoKy).ToString("N0"));

            dg.ItemsSource = result;
        }
    }
}
