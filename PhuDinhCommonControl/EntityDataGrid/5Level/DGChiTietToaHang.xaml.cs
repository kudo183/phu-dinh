using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGChiTietToaHang.xaml
    /// </summary>
    public partial class DGChiTietToaHang : DataGridExt
    {
        public DGChiTietToaHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
