using PhuDinhCommon;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for NhanVienView.xaml
    /// </summary>
    public partial class NhanVienView : UserControl
    {
        public NhanVienView()
        {
            InitializeComponent();

            Loaded += NhanVienView_Loaded;
            Unloaded += NhanVienView_Unloaded;
        }

        void NhanVienView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rPhuongTienView.dgPhuongTien.SelectionChanged += dgPhuongTien_SelectionChanged;

            _rNhanVienView.SetMainFilter(PhuDinhData.Filter.Filter_rNhanVien.MaPhuongTien, null, true);
        }

        void NhanVienView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rPhuongTienView.dgPhuongTien.SelectionChanged -= dgPhuongTien_SelectionChanged;
        }

        void dgPhuongTien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var phuongTien = ((DataGrid)sender).SelectedItem as PhuDinhDataEntity.rPhuongTien;
            if (phuongTien == null)
            {
                _rNhanVienView.SetMainFilter(PhuDinhData.Filter.Filter_rNhanVien.MaPhuongTien, null, true);
                _rNhanVienView.RefreshView();
                return;
            }

            _rNhanVienView.SetMainFilter(PhuDinhData.Filter.Filter_rNhanVien.MaPhuongTien, phuongTien.Ma);
            _rNhanVienView.SetDefaultValue(Constant.ColumnName_MaPhuongTien, phuongTien.Ma);
            _rNhanVienView.RefreshView();
        }
    }
}
