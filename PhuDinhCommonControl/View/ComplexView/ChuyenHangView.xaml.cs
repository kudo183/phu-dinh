using System.Windows.Input;
using PhuDinhCommon;
using System.Linq;
using System.Windows.Controls;
using PhuDinhData.ViewModel;
using PhuDinhCommonControl.EntityDataGrid;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ChuyenHangView.xaml
    /// </summary>
    public partial class ChuyenHangView : _BaseComplexView
    {
        public ChuyenHangView()
        {
            InitializeComponent();

            AddView(_tChuyenHangView);
            AddView(_tChuyenHangDonHangView);
            AddView(_tChiTietChuyenHangDonHangView);
        }

        protected override void OnLoaded()
        {
            _tChuyenHangDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChuyenHangDonHang.MaChuyenHang, null, true);
            _tChiTietChuyenHangDonHangView.SetMainFilter(
                            PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, null, true);
        }

        protected override void FocusView(IBaseView view)
        {
            if (view is tChuyenHangView)
            {
                _tChuyenHangView.dg.FocusCell(_tChuyenHangView.dg.Items.Count - 1, 3);
            }
            else if (view is tChuyenHangDonHangView)
            {
                _tChuyenHangDonHangView.dg.FocusCell(_tChuyenHangDonHangView.dg.Items.Count - 1, 2);
            }
            else if (view is tChiTietChuyenHangDonHangView)
            {
                _tChiTietChuyenHangDonHangView.dg.FocusCell(_tChiTietChuyenHangDonHangView.dg.Items.Count - 1, 2, false);
            }
        }

        protected override void OnAfterSave(IBaseView view)
        {
            if (view is tChuyenHangDonHangView)
            {
                ProcessChuyenHangDonHangView_AfterSave();
            }
            else if (view is tChiTietChuyenHangDonHangView)
            {
                (_tChuyenHangDonHangView.dg.DataContext as ChuyenHangDonHangViewModel).UpdateReferenceData(Constant.ColumnName_DonHang);
                Keyboard.Focus(_tChiTietChuyenHangDonHangView.dg);
            }
        }

        protected override void OnSelectionChanged(object view)
        {
            if (view is DGChuyenHang)
            {
                RefreshChuyenHangDonHangView(_tChuyenHangView.dg);
            }
            else if (view is DGChuyenHangDonHang)
            {
                RefreshChiTietChuyenHangDonHangView(_tChuyenHangDonHangView.dg);
            }
        }

        private void ProcessChuyenHangDonHangView_AfterSave()
        {
            var chuyenHangDonHang =
                _tChuyenHangDonHangView.dg.SelectedItem
                as PhuDinhData.tChuyenHangDonHang;

            if (chuyenHangDonHang != null && chuyenHangDonHang.tChiTietChuyenHangDonHangs.Count > 0)
            {
                return;
            }

            var context =
                _tChiTietChuyenHangDonHangView.dg.DataContext
                as ChiTietChuyenHangDonHangViewModel;

            if (chuyenHangDonHang == null || chuyenHangDonHang.tDonHang == null)
                return;

            foreach (var tChiTietDonHang in chuyenHangDonHang.tDonHang.tChiTietDonHangs.Where(p => p.Xong == false))
            {
                var soLuong = tChiTietDonHang.SoLuong - tChiTietDonHang.tChiTietChuyenHangDonHangs.Sum(p => p.SoLuong);
                var ct = new PhuDinhData.tChiTietChuyenHangDonHang
                             {
                                 MaChiTietDonHang = tChiTietDonHang.Ma,
                                 MaChuyenHangDonHang = chuyenHangDonHang.Ma,
                                 tChiTietDonHang = tChiTietDonHang,
                                 SoLuong = soLuong
                             };

                context.Entity.Add(ct);
            }
        }

        private void RefreshChuyenHangDonHangView(DataGrid dataGrid)
        {
            var chuyenHang = dataGrid.SelectedItem as PhuDinhData.tChuyenHang;

            if (chuyenHang == null)
            {
                _tChuyenHangDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChuyenHangDonHang.MaChuyenHang, null, true);

                _tChuyenHangDonHangView.RefreshView();
                return;
            }

            _tChuyenHangDonHangView.SetReferenceFilter<PhuDinhData.tDonHang>(
                Constant.ColumnName_DonHang, (p => p.Xong == false));

            _tChuyenHangDonHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChuyenHangDonHang.MaChuyenHang, chuyenHang.Ma);

            _tChuyenHangDonHangView.SetDefaultValue(Constant.ColumnName_MaChuyenHang, chuyenHang.Ma);
            _tChuyenHangDonHangView.RefreshView();
        }

        private void RefreshChiTietChuyenHangDonHangView(DataGrid dataGrid)
        {
            var chuyenHangDonHang = dataGrid.SelectedItem as PhuDinhData.tChuyenHangDonHang;

            if (chuyenHangDonHang == null)
            {
                _tChiTietChuyenHangDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, null, true);

                _tChiTietChuyenHangDonHangView.RefreshView();

                return;
            }

            _tChiTietChuyenHangDonHangView.SetReferenceFilter<PhuDinhData.tChiTietDonHang>(
                Constant.ColumnName_ChiTietDonHang
                , (p => p.Xong == false && p.MaDonHang == chuyenHangDonHang.MaDonHang));

            _tChiTietChuyenHangDonHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, chuyenHangDonHang.Ma);

            _tChiTietChuyenHangDonHangView.SetDefaultValue(
                Constant.ColumnName_MaChuyenHangDonHang, chuyenHangDonHang.Ma);

            _tChiTietChuyenHangDonHangView.RefreshView();
        }
    }
}
