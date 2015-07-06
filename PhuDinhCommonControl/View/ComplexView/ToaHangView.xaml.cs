using PhuDinhCommon;
using PhuDinhCommonControl.EntityDataGrid;
using PhuDinhDataEntity;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ToaHangView.xaml
    /// </summary>
    public partial class ToaHangView : _BaseComplexView
    {
        public ToaHangView()
        {
            InitializeComponent();

            AddView(_tToaHangView);
            AddView(_tChiTietToaHangView);
        }

        protected override void OnLoaded()
        {
            _tChiTietToaHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietToaHang.MaToaHang, null, true);
        }

        protected override void OnAfterSave(IBaseView view)
        {
            if (view is tChiTietToaHangView)
            {
                
            }
        }

        protected override void OnSelectionChanged(object view)
        {
            if (view is DGToaHang)
            {
                RefreshChiTietToaHangView();
            }
        }

        private void RefreshChiTietToaHangView()
        {
            if (_tToaHangView.SelectedItem == null || _tToaHangView.SelectedItem.Ma == 0)
            {
                _tChiTietToaHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietToaHang.MaToaHang, null, true);

                _tChiTietToaHangView.RefreshView();
                return;
            }

            _tChiTietToaHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietToaHang.MaToaHang, _tToaHangView.SelectedItem.Ma);
            _tChiTietToaHangView.SetReferenceFilter<tChiTietDonHang>(
                Constant.ColumnName_ChiTietDonHang, p => p.tDonHang.MaKhachHang == _tToaHangView.SelectedItem.MaKhachHang);
            _tChiTietToaHangView.SetDefaultValue(Constant.ColumnName_MaToaHang, _tToaHangView.SelectedItem.Ma);

            _tChiTietToaHangView.RefreshView();
        }
    }
}
