using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiNguyenLieuView.xaml
    /// </summary>
    public partial class rLoaiNguyenLieuView : BaseView<PhuDinhData.rLoaiNguyenLieu>
    {
        public rLoaiNguyenLieuView()
        {
            InitializeComponent();

            dg = dgLoaiNguyenLieu;

            _viewModel = new LoaiNguyenLieuViewModel();
            DataContext = _viewModel;
        }
    }
}
