using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for DonHangView.xaml
    /// </summary>
    public partial class DonHangView : UserControl
    {
        public DonHangView()
        {
            InitializeComponent();

            Loaded += DonHangView_Loaded;
            Unloaded += DonHangView_Unloaded;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.D1:
                        FocustDonHangView();
                        break;
                    case Key.D2:
                        FocustChiTietDonHangView();
                        break;
                }
            }
        }

        void FocustDonHangView()
        {
            _tDonHangView.dg.SelectionChanged -= dgDonHang_SelectionChanged;
            _tDonHangView.dg.FocusCell(_tDonHangView.dg.Items.Count - 1, 2);
            _tDonHangView.dg.SelectionChanged += dgDonHang_SelectionChanged;

            RefreshChiTietDonHangView(_tDonHangView.dg);
        }

        void FocustChiTietDonHangView()
        {
            _tChiTietDonHangView.dg.FocusCell(_tChiTietDonHangView.dg.Items.Count - 1, 2);
        }

        void DonHangView_Loaded(object sender, RoutedEventArgs e)
        {
            _tDonHangView.dgDonHang.SelectionChanged += dgDonHang_SelectionChanged;
            _tChiTietDonHangView.AfterSave += _tChiTietDonHangView_AfterSave;
            _tChiTietDonHangView.MoveFocus += _tChiTietDonHangView_MoveFocus;
            _tDonHangView.AfterSave += _tDonHangView_AfterSave;
            _tDonHangView.MoveFocus += _tDonHangView_MoveFocus;

            _tChiTietDonHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietDonHang.MaDonHang, null, true);
        }

        void DonHangView_Unloaded(object sender, RoutedEventArgs e)
        {
            _tDonHangView.dgDonHang.SelectionChanged -= dgDonHang_SelectionChanged;
            _tChiTietDonHangView.AfterSave -= _tChiTietDonHangView_AfterSave;
            _tChiTietDonHangView.MoveFocus -= _tChiTietDonHangView_MoveFocus;
            _tDonHangView.AfterSave -= _tDonHangView_AfterSave;
            _tDonHangView.MoveFocus -= _tDonHangView_MoveFocus;
        }

        void _tDonHangView_AfterSave(object sender, System.EventArgs e)
        {
            RefreshChiTietDonHangView(_tDonHangView.dg);
        }

        void _tDonHangView_MoveFocus(object sender, System.EventArgs e)
        {
            FocustChiTietDonHangView();
        }

        void _tChiTietDonHangView_AfterSave(object sender, System.EventArgs e)
        {
            _tDonHangView.RefreshView();

            Keyboard.Focus(_tChiTietDonHangView.dg);
        }

        void _tChiTietDonHangView_MoveFocus(object sender, System.EventArgs e)
        {
            FocustDonHangView();
        }

        void dgDonHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            RefreshChiTietDonHangView(_tDonHangView.dg);
        }

        private void RefreshChiTietDonHangView(DataGrid dataGrid)
        {
            var donHang = dataGrid.SelectedItem as PhuDinhData.tDonHang;

            if (donHang == null)
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
