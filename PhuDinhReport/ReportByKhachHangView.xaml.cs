using System.Collections.Generic;
using System.Linq;
using CustomControl.DataGridColumnHeaderFilterModel;
using System;
using System.Windows.Controls;
using PhuDinhData.ReportData;
using PhuDinhDataEntity;

namespace PhuDinhReport
{
    /// <summary>
    /// Interaction logic for ReportByKhachHangView.xaml
    /// </summary>
    public partial class ReportByKhachHangView : UserControl
    {
        public HeaderComboBoxFilterModel Header_KhachHang { get; set; }

        private int _mode = 1;
        private bool _isGroupByDate = false;

        public ReportByKhachHangView()
        {
            InitializeComponent();

            reportDatePicker.NgaySelected += reportDatePicker_NgaySelected;
            reportDatePicker.TuNgayDenNgaySelected += reportDatePicker_TuNgayDenNgaySelected;

            Header_KhachHang = new HeaderComboBoxFilterModel("Khach Hang");

            Header_KhachHang.ItemSource = PhuDinhData.ClientContext.Instance.GetData<rKhachHang>(null).OrderBy(p => p.TenKhachHang)
                .ToDictionary(p => p.Ma, p => p.TenKhachHang);
            Header_KhachHang.PropertyChanged += Header_KhachHang_PropertyChanged;
            Header_KhachHang.SelectedValue = 162;

            dg.Columns[0].Header = Header_KhachHang;
        }

        void Header_KhachHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Refresh();
        }

        void reportDatePicker_NgaySelected(object sender, EventArgs e)
        {
            _mode = 1;
            ReportByDate();
        }

        void reportDatePicker_TuNgayDenNgaySelected(object sender, EventArgs e)
        {
            _mode = 2;
            ReportFromDateToDate();
        }

        private void ReportByDate()
        {
            var ngay = reportDatePicker.Ngay.Value.Date;

            List<ReportByKhachHang.ReportByKhachHangData> result;
            if (Header_KhachHang.IsUsed)
            {
                result = ReportByKhachHang.FilterByDate(ngay, (int)Header_KhachHang.SelectedValue, _isGroupByDate);
            }
            else
            {
                result = ReportByKhachHang.FilterByDate(ngay, _isGroupByDate);
            }

            reportDatePicker.NgayMsg = string.Format("Tong so cuon: {0}", result.Sum(p => p.SoLuong).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void ReportFromDateToDate()
        {
            var tuNgay = reportDatePicker.TuNgay.Value.Date;

            var denNgay = reportDatePicker.DenNgay.Value.Date;

            List<ReportByKhachHang.ReportByKhachHangData> result;
            if (Header_KhachHang.IsUsed)
            {
                result = ReportByKhachHang.FilterByDate(tuNgay, denNgay, (int)Header_KhachHang.SelectedValue, _isGroupByDate);
            }
            else
            {
                result = ReportByKhachHang.FilterByDate(tuNgay, denNgay, _isGroupByDate);
            }

            reportDatePicker.TuNgayDenNgayMsg = string.Format("Tong so cuon: {0}", result.Sum(p => p.SoLuong).ToString("N0"));

            dg.ItemsSource = result;
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _isGroupByDate = true;
            Refresh();
        }

        private void CheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            _isGroupByDate = false;
            Refresh();
        }

        private void Refresh()
        {
            switch (_mode)
            {
                case 1:
                    ReportByDate();
                    break;
                case 2:
                    ReportFromDateToDate();
                    break;
            }
        }
    }
}
