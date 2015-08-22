using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGPhuThuKhachHang.xaml
    /// </summary>
    public partial class DGPhuThuKhachHang : DataGridExt
    {
        public DGPhuThuKhachHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
