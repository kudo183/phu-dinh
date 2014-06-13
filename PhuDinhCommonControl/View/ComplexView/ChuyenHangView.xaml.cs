using System.Windows.Input;
using PhuDinhCommon;
using System.Linq;
using System.Windows.Controls;
using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ChuyenHangView.xaml
    /// </summary>
    public partial class ChuyenHangView : UserControl
    {
        public ChuyenHangView()
        {
            InitializeComponent();

            Loaded += ChuyenHangView_Loaded;
            Unloaded += ChuyenHangView_Unloaded;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.D1:
                        FocustChuyenHangView();
                        break;
                    case Key.D2:
                        FocustChuyenHangDonHangView();
                        break;
                    case Key.D3:
                        FocustChiTietChuyenHangDonHangView();
                        break;
                }
            }
        }

        void FocustChuyenHangView()
        {
            _tChuyenHangView.dg.SelectionChanged -= dgChuyenHang_SelectionChanged;
            _tChuyenHangView.dg.FocusCell(_tChuyenHangView.dg.Items.Count - 1, 3);
            _tChuyenHangView.dg.SelectionChanged += dgChuyenHang_SelectionChanged;

            RefreshChuyenHangDonHangView(_tChuyenHangView.dg);
        }

        void FocustChuyenHangDonHangView()
        {
            _tChuyenHangDonHangView.dg.SelectionChanged -= dgChuyenHangDonHang_SelectionChanged;
            _tChuyenHangDonHangView.dg.FocusCell(_tChuyenHangDonHangView.dg.Items.Count - 1, 2);
            _tChuyenHangDonHangView.dg.SelectionChanged += dgChuyenHangDonHang_SelectionChanged;

            RefreshChiTietChuyenHangDonHangView(_tChuyenHangDonHangView.dg);
        }

        void FocustChiTietChuyenHangDonHangView(bool callBeginEdit = true)
        {
            _tChiTietChuyenHangDonHangView.dg.FocusCell(_tChiTietChuyenHangDonHangView.dg.Items.Count - 1, 2, callBeginEdit);
        }

        void ChuyenHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _tChuyenHangView.dgChuyenHang.SelectionChanged += dgChuyenHang_SelectionChanged;
            _tChuyenHangDonHangView.dgChuyenHangDonHang.SelectionChanged += dgChuyenHangDonHang_SelectionChanged;

            _tChuyenHangView.AfterSave += _tChuyenHangView_AfterSave;
            _tChuyenHangView.MoveFocus += _tChuyenHangView_MoveFocus;
            _tChuyenHangDonHangView.AfterSave += _tChuyenHangDonHangView_AfterSave;
            _tChuyenHangDonHangView.MoveFocus += _tChuyenHangDonHangView_MoveFocus;
            _tChiTietChuyenHangDonHangView.AfterSave += _tChiTietChuyenHangDonHangView_AfterSave;

            _tChuyenHangDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChuyenHangDonHang.MaChuyenHang, null, true);
            _tChiTietChuyenHangDonHangView.SetMainFilter(
                            PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, null, true);
        }

        void ChuyenHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _tChuyenHangView.dgChuyenHang.SelectionChanged -= dgChuyenHang_SelectionChanged;
            _tChuyenHangDonHangView.dgChuyenHangDonHang.SelectionChanged -= dgChuyenHangDonHang_SelectionChanged;

            _tChuyenHangView.AfterSave -= _tChuyenHangView_AfterSave;
            _tChuyenHangView.MoveFocus -= _tChuyenHangView_MoveFocus;
            _tChuyenHangDonHangView.AfterSave -= _tChuyenHangDonHangView_AfterSave;
            _tChuyenHangDonHangView.MoveFocus -= _tChuyenHangDonHangView_MoveFocus;
            _tChiTietChuyenHangDonHangView.AfterSave -= _tChiTietChuyenHangDonHangView_AfterSave;
        }

        void _tChiTietChuyenHangDonHangView_AfterSave(object sender, System.EventArgs e)
        {
            _tChuyenHangView.RefreshView();

            Keyboard.Focus(_tChiTietChuyenHangDonHangView.dg);
        }

        void _tChuyenHangDonHangView_AfterSave(object sender, System.EventArgs e)
        {
            var chuyenHangDonHang =
                _tChuyenHangDonHangView.dgChuyenHangDonHang.SelectedItem
                as PhuDinhData.tChuyenHangDonHang;

            RefreshChiTietChuyenHangDonHangView(_tChuyenHangDonHangView.dg);

            if (chuyenHangDonHang != null && chuyenHangDonHang.tChiTietChuyenHangDonHangs.Count > 0)
            {
                return;
            }

            var context =
                _tChiTietChuyenHangDonHangView.dgChiTietChuyenHangDonHang.DataContext
                as ChiTietChuyenHangDonHangViewModel;

            if (chuyenHangDonHang == null)
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

        void _tChuyenHangDonHangView_MoveFocus(object sender, System.EventArgs e)
        {
            FocustChiTietChuyenHangDonHangView(false);
        }

        void _tChuyenHangView_AfterSave(object sender, System.EventArgs e)
        {
            RefreshChuyenHangDonHangView(_tChuyenHangView.dg);
        }

        void _tChuyenHangView_MoveFocus(object sender, System.EventArgs e)
        {
            FocustChuyenHangDonHangView();
        }

        void dgChuyenHangDonHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            RefreshChiTietChuyenHangDonHangView(_tChuyenHangDonHangView.dg);
        }

        void dgChuyenHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            if (_tChuyenHangView.dg.SelectedIndex == -1)
            {
                return;
            }

            RefreshChuyenHangDonHangView(_tChuyenHangView.dg);
        }

        private void RefreshChuyenHangDonHangView(DataGrid dataGrid)
        {
            var chuyenHang = dataGrid.SelectedItem as PhuDinhData.tChuyenHang;

            _tChiTietChuyenHangDonHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, null, true);

            _tChiTietChuyenHangDonHangView.RefreshView();

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
