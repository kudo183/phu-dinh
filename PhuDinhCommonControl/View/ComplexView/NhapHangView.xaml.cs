using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for NhapHangView.xaml
    /// </summary>
    public partial class NhapHangView : UserControl
    {
        public NhapHangView()
        {
            InitializeComponent();

            Loaded += NhapHangView_Loaded;
            Unloaded += NhapHangView_Unloaded;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.D1:
                        FocustNhapHangView();
                        break;
                    case Key.D2:
                        FocustChiTietNhapHangView();
                        break;
                }
                
            }
        }

        void FocustNhapHangView()
        {
            _tNhapHangView.dg.SelectionChanged -= dgNhapHang_SelectionChanged;
            _tNhapHangView.dg.FocusCell(_tNhapHangView.dg.Items.Count - 1, 2);
            _tNhapHangView.dg.SelectionChanged += dgNhapHang_SelectionChanged;

            RefreshChiTietNhapHangView(_tNhapHangView.dg);
        }

        void FocustChiTietNhapHangView()
        {
            _tChiTietNhapHangView.dg.FocusCell(_tChiTietNhapHangView.dg.Items.Count - 1, 2);                        
        }

        void NhapHangView_Loaded(object sender, RoutedEventArgs e)
        {
            _tNhapHangView.dgNhapHang.SelectionChanged += dgNhapHang_SelectionChanged;
            _tChiTietNhapHangView.AfterSave += _tChiTietNhapHangView_AfterSave;
            _tNhapHangView.AfterSave += _tNhapHangView_AfterSave;

            _tChiTietNhapHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietNhapHang.MaNhapHang, null, true);
        }

        void NhapHangView_Unloaded(object sender, RoutedEventArgs e)
        {
            _tNhapHangView.dgNhapHang.SelectionChanged -= dgNhapHang_SelectionChanged;
            _tChiTietNhapHangView.AfterSave -= _tChiTietNhapHangView_AfterSave;
            _tNhapHangView.AfterSave -= _tNhapHangView_AfterSave;
        }

        void _tNhapHangView_AfterSave(object sender, System.EventArgs e)
        {
            RefreshChiTietNhapHangView(_tNhapHangView.dg);

            FocustChiTietNhapHangView();
        }

        void _tChiTietNhapHangView_AfterSave(object sender, System.EventArgs e)
        {
            _tNhapHangView.RefreshView();

            FocustNhapHangView();
        }

        void dgNhapHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            RefreshChiTietNhapHangView(_tNhapHangView.dg);
        }

        private void RefreshChiTietNhapHangView(DataGrid dataGrid)
        {
            var NhapHang = dataGrid.SelectedItem as PhuDinhData.tNhapHang;

            if (NhapHang == null)
            {
                _tChiTietNhapHangView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietNhapHang.MaNhapHang, null, true);

                _tChiTietNhapHangView.RefreshView();
                return;
            }

            _tChiTietNhapHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietNhapHang.MaNhapHang, NhapHang.Ma);

            _tChiTietNhapHangView.SetDefaultValue(Constant.ColumnName_MaNhapHang, NhapHang.Ma);

            _tChiTietNhapHangView.RefreshView();
        }
    }
}
