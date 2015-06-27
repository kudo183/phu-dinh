using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhuDinhCommon;
using PhuDinhCommonControl.EntityDataGrid;

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
            if (view is tToaHangView)
            {
                RefreshChiTietToaHangView(_tToaHangView.dg);
            }
            else if (view is tChiTietToaHangView)
            {
                _tToaHangView.RefreshView();

                Keyboard.Focus(_tChiTietToaHangView.dg);
            }
        }

        protected override void OnSelectionChanged(object view)
        {
            if (view is DGToaHang)
            {
                RefreshChiTietToaHangView(_tToaHangView.dg);
            }
        }

        private void RefreshChiTietToaHangView(DataGrid dataGrid)
        {
            var ToaHang = dataGrid.SelectedItem as PhuDinhDataEntity.tToaHang;

            if (ToaHang == null || ToaHang.Ma == 0)
            {
                _tChiTietToaHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietToaHang.MaToaHang, null, true);

                _tChiTietToaHangView.RefreshView();
                return;
            }

            _tChiTietToaHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietToaHang.MaToaHang, ToaHang.Ma);
            _tChiTietToaHangView.SetReferenceFilter<PhuDinhDataEntity.tChiTietDonHang>(
                Constant.ColumnName_ChiTietDonHang, p => p.tDonHang.MaKhachHang == ToaHang.MaKhachHang);
            _tChiTietToaHangView.SetDefaultValue(Constant.ColumnName_MaToaHang, ToaHang.Ma);

            _tChiTietToaHangView.RefreshView();
        }
    }
}
