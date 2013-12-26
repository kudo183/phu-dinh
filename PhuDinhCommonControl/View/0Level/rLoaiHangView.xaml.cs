using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiHangView.xaml
    /// </summary>
    public partial class rLoaiHangView : BaseView<PhuDinhData.rLoaiHang>
    {
        public rLoaiHangView()
        {
            InitializeComponent();

            dg = dgLoaiHang;

            _viewModel = new LoaiHangViewModel();
            DataContext = _viewModel;
        }
    }
}
