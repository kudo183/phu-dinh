using System.Windows.Input;
using PhuDinhCommon;
using System.Linq;
using PhuDinhData.ViewModel;
using PhuDinhCommonControl.EntityDataGrid;
using PhuDinhDataEntity;

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
                _tChuyenHangDonHangView.ViewModel.UpdateReferenceData(Constant.ColumnName_DonHang);
                Keyboard.Focus(_tChiTietChuyenHangDonHangView.dg);
            }
        }

        protected override void OnSelectionChanged(object view)
        {
            if (view is DGChuyenHang)
            {
                RefreshChuyenHangDonHangView();
            }
            else if (view is DGChuyenHangDonHang)
            {
                RefreshChiTietChuyenHangDonHangView();
            }
        }

        private void ProcessChuyenHangDonHangView_AfterSave()
        {
            if (_tChuyenHangDonHangView.SelectedItem != null && _tChuyenHangDonHangView.SelectedItem.tChiTietChuyenHangDonHangs.Count > 0)
            {
                return;
            }

            if (_tChuyenHangDonHangView.SelectedItem == null || _tChuyenHangDonHangView.SelectedItem.tDonHang == null)
            {
                return;
            }

            foreach (var tChiTietDonHang in _tChuyenHangDonHangView.SelectedItem.tDonHang.tChiTietDonHangs.Where(p => p.Xong == false))
            {
                var soLuong = tChiTietDonHang.SoLuong - tChiTietDonHang.tChiTietChuyenHangDonHangs.Sum(p => p.SoLuong);
                var ct = new tChiTietChuyenHangDonHang
                             {
                                 MaChiTietDonHang = tChiTietDonHang.Ma,
                                 MaChuyenHangDonHang = _tChuyenHangDonHangView.SelectedItem.Ma,
                                 tChiTietDonHang = tChiTietDonHang,
                                 SoLuong = soLuong
                             };

                _tChiTietChuyenHangDonHangView.ViewModel.Entity.Add(ct);
            }
        }

        private void RefreshChuyenHangDonHangView()
        {
            if (_tChuyenHangView.SelectedItem == null || _tChuyenHangView.SelectedItem.Ma == 0)
            {
                _tChuyenHangDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChuyenHangDonHang.MaChuyenHang, null, true);

                _tChuyenHangDonHangView.RefreshView();
                return;
            }

            _tChuyenHangDonHangView.SetReferenceFilter<tDonHang>(
                Constant.ColumnName_DonHang, (p => p.Xong == false));

            _tChuyenHangDonHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChuyenHangDonHang.MaChuyenHang, _tChuyenHangView.SelectedItem.Ma);

            _tChuyenHangDonHangView.SetDefaultValue(Constant.ColumnName_MaChuyenHang, _tChuyenHangView.SelectedItem.Ma);
            _tChuyenHangDonHangView.RefreshView();
        }

        private void RefreshChiTietChuyenHangDonHangView()
        {
            if (_tChuyenHangDonHangView.SelectedItem == null || _tChuyenHangDonHangView.SelectedItem.Ma == 0)
            {
                _tChiTietChuyenHangDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, null, true);

                _tChiTietChuyenHangDonHangView.RefreshView();

                return;
            }

            _tChiTietChuyenHangDonHangView.SetReferenceFilter<tChiTietDonHang>(
                Constant.ColumnName_ChiTietDonHang
                , (p => p.Xong == false && p.MaDonHang == _tChuyenHangDonHangView.SelectedItem.MaDonHang));

            _tChiTietChuyenHangDonHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, _tChuyenHangDonHangView.SelectedItem.Ma);

            _tChiTietChuyenHangDonHangView.SetDefaultValue(
                Constant.ColumnName_MaChuyenHangDonHang, _tChuyenHangDonHangView.SelectedItem.Ma);

            _tChiTietChuyenHangDonHangView.RefreshView();
        }
    }
}
