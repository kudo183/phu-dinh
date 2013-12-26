using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNhaCungCapView.xaml
    /// </summary>
    public partial class rNhaCungCapView : BaseView<PhuDinhData.rNhaCungCap>
    {
        public rNhaCungCapView()
        {
            InitializeComponent();

            dg = dgNhaCungCap;

            _viewModel = new NhaCungCapViewModel();
            DataContext = _viewModel;
        }
    }
}
