using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGTonKho.xaml
    /// </summary>
    public partial class DGTonKho : DataGridExt
    {
        public DGTonKho()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
