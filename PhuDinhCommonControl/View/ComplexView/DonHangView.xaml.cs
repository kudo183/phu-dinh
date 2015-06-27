using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhuDinhCommon;
using PhuDinhCommonControl.EntityDataGrid;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for DonHangView.xaml
    /// </summary>
    public partial class DonHangView : _BaseComplexView
    {
        public DonHangView()
        {
            InitializeComponent();

            AddView(_tDonHangView);
            AddView(_tChiTietDonHangView);
        }

        protected override void OnLoaded()
        {
            _tChiTietDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietDonHang.MaDonHang, null, true);
        }

        protected override void OnAfterSave(IBaseView view)
        {
            if (view is tDonHangView)
            {
                RefreshChiTietDonHangView(_tDonHangView.dg);
            }
            else if (view is tChiTietDonHangView)
            {
                _tDonHangView.RefreshView();

                Keyboard.Focus(_tChiTietDonHangView.dg);
            }
        }

        protected override void OnSelectionChanged(object view)
        {
            if (view is DGDonHang)
            {
                RefreshChiTietDonHangView(_tDonHangView.dg);
            }
        }

        private void RefreshChiTietDonHangView(DataGrid dataGrid)
        {
            var donHang = dataGrid.SelectedItem as PhuDinhDataEntity.tDonHang;

            if (donHang == null || donHang.Ma == 0)
            {
                _tChiTietDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietDonHang.MaDonHang, null, true);

                _tChiTietDonHangView.RefreshView();
                return;
            }

            _tChiTietDonHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietDonHang.MaDonHang, donHang.Ma);

            _tChiTietDonHangView.SetDefaultValue(Constant.ColumnName_MaDonHang, donHang.Ma);

            _tChiTietDonHangView.RefreshView();
        }
    }
}
