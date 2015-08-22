using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGNhanTienKhachHang.xaml
    /// </summary>
    public partial class DGNhanTienKhachHang : DataGridExt
    {
        public DGNhanTienKhachHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
