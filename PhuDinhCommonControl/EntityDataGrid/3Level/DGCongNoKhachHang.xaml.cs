using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGCongNoKhachHang.xaml
    /// </summary>
    public partial class DGCongNoKhachHang : DataGridExt
    {
        public DGCongNoKhachHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
