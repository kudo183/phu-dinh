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
            if (view is tChiTietDonHangView)
            {
                
            }
        }

        protected override void OnSelectionChanged(object view)
        {
            if (view is DGDonHang)
            {
                RefreshChiTietDonHangView();
            }
        }

        private void RefreshChiTietDonHangView()
        {
            if (_tDonHangView.SelectedItem == null || _tDonHangView.SelectedItem.Ma == 0)
            {
                _tChiTietDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietDonHang.MaDonHang, null, true);

                _tChiTietDonHangView.RefreshView();
                return;
            }

            _tChiTietDonHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietDonHang.MaDonHang, _tDonHangView.SelectedItem.Ma);

            _tChiTietDonHangView.SetDefaultValue(Constant.ColumnName_MaDonHang, _tDonHangView.SelectedItem.Ma);

            _tChiTietDonHangView.RefreshView();
        }
    }
}
