using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiNguyenLieuView.xaml
    /// </summary>
    public partial class rLoaiNguyenLieuView : BaseView<PhuDinhDataEntity.rLoaiNguyenLieu>
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
