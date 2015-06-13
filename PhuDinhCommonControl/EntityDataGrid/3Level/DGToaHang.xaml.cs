using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGToaHang.xaml
    /// </summary>
    public partial class DGToaHang : DataGridExt
    {
        public DGToaHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
            SkippedColumnIndex.Add(3);
        }
    }
}
