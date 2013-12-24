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
            _rPhuongTienView.RefreshView();

            _rPhuongTienView.dgPhuongTien.SelectionChanged += dgPhuongTien_SelectionChanged;
        }

        void dgPhuongTien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var phuongTien = ((DataGrid)sender).SelectedItem as PhuDinhData.rPhuongTien;
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
