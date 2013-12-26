using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNuocView.xaml
    /// </summary>
    public partial class rNuocView : BaseView<PhuDinhData.rNuoc>
    {
        public rNuocView()
        {
            InitializeComponent();

            dg = dgNuoc;

            _viewModel = new NuocViewModel();
            DataContext = _viewModel;
        }
    }
}
