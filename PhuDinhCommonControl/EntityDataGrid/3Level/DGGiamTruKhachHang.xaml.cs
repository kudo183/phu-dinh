using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGGiamTruKhachHang.xaml
    /// </summary>
    public partial class DGGiamTruKhachHang : DataGridExt
    {
        public DGGiamTruKhachHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
