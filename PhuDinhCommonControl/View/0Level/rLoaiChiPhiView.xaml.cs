using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiChiPhiView.xaml
    /// </summary>
    public partial class rLoaiChiPhiView : BaseView<PhuDinhDataEntity.rLoaiChiPhi>
    {
        public rLoaiChiPhiView()
        {
            InitializeComponent();

            dg = dgLoaiChiPhi;

            _viewModel = new LoaiChiPhiViewModel();
            DataContext = _viewModel;
        }
    }
}
