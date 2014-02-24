using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ChuyenKhoView.xaml
    /// </summary>
    public partial class ChuyenKhoView : UserControl
    {
        public ChuyenKhoView()
        {
            InitializeComponent();

            Loaded += ChuyenKhoView_Loaded;
            Unloaded += ChuyenKhoView_Unloaded;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.D1:
                        FocustChuyenKhoView();
                        break;
                    case Key.D2:
                        FocustChiTietChuyenKhoView();
                        break;
                }
                
            }
        }

        void FocustChuyenKhoView()
        {
            _tChuyenKhoView.dg.SelectionChanged -= dgChuyenKho_SelectionChanged;
            _tChuyenKhoView.dg.FocusCell(_tChuyenKhoView.dg.Items.Count - 1, 2);
            _tChuyenKhoView.dg.SelectionChanged += dgChuyenKho_SelectionChanged;

            RefreshChiTietChuyenKhoView(_tChuyenKhoView.dg);
        }

        void FocustChiTietChuyenKhoView()
        {
            _tChiTietChuyenKhoView.dg.FocusCell(_tChiTietChuyenKhoView.dg.Items.Count - 1, 2);                        
        }

        void ChuyenKhoView_Loaded(object sender, RoutedEventArgs e)
        {
            _tChuyenKhoView.dgChuyenKho.SelectionChanged += dgChuyenKho_SelectionChanged;
            _tChiTietChuyenKhoView.AfterSave += _tChiTietChuyenKhoView_AfterSave;
            _tChuyenKhoView.AfterSave += _tChuyenKhoView_AfterSave;

            _tChiTietChuyenKhoView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietChuyenKho.MaChuyenKho, null, true);
        }

        void ChuyenKhoView_Unloaded(object sender, RoutedEventArgs e)
        {
            _tChuyenKhoView.dgChuyenKho.SelectionChanged -= dgChuyenKho_SelectionChanged;
            _tChiTietChuyenKhoView.AfterSave -= _tChiTietChuyenKhoView_AfterSave;
            _tChuyenKhoView.AfterSave -= _tChuyenKhoView_AfterSave;
        }

        void _tChuyenKhoView_AfterSave(object sender, System.EventArgs e)
        {
            RefreshChiTietChuyenKhoView(_tChuyenKhoView.dg);

            FocustChiTietChuyenKhoView();
        }

        void _tChiTietChuyenKhoView_AfterSave(object sender, System.EventArgs e)
        {
            _tChuyenKhoView.RefreshView();

            FocustChuyenKhoView();
        }

        void dgChuyenKho_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            RefreshChiTietChuyenKhoView(_tChuyenKhoView.dg);
        }

        private void RefreshChiTietChuyenKhoView(DataGrid dataGrid)
        {
            var ChuyenKho = dataGrid.SelectedItem as PhuDinhData.tChuyenKho;

            if (ChuyenKho == null)
            {
                _tChiTietChuyenKhoView.SetMainFilter(
                    PhuDinhData.Filter.Filter_tChiTietChuyenKho.MaChuyenKho, null, true);

                _tChiTietChuyenKhoView.RefreshView();
                return;
            }

            _tChiTietChuyenKhoView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiTietChuyenKho.MaChuyenKho, ChuyenKho.Ma);

            _tChiTietChuyenKhoView.SetDefaultValue(Constant.ColumnName_MaChuyenKho, ChuyenKho.Ma);

            _tChiTietChuyenKhoView.RefreshView();
        }
    }
}
