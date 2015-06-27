using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rPhuongTienView.xaml
    /// </summary>
    public partial class rPhuongTienView : BaseView<PhuDinhDataEntity.rPhuongTien>
    {
        public rPhuongTienView()
        {
            InitializeComponent();

            dg = dgPhuongTien;

            _viewModel = new PhuongTienViewModel();
            DataContext = _viewModel;
        }
    }
}
