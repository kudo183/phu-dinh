using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGChuyenHang.xaml
    /// </summary>
    public partial class DGChuyenHang : DataGridExt
    {
        public DGChuyenHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
