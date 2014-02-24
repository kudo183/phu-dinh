using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGChiTietChuyenKho.xaml
    /// </summary>
    public partial class DGChiTietChuyenKho : DataGridExt
    {
        public DGChiTietChuyenKho()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
