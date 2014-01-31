using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGChiTietNhapHang.xaml
    /// </summary>
    public partial class DGChiTietNhapHang : DataGridExt
    {
        public DGChiTietNhapHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
