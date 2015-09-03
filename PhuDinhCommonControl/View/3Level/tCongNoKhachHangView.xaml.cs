using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tCongNoKhachHangView.xaml
    /// </summary>
    public partial class tCongNoKhachHangView : BaseView<PhuDinhDataEntity.tCongNoKhachHang>
    {
        public tCongNoKhachHangView()
        {
            InitializeComponent();

            dg = dgCongNoKhachHang;

            _viewModel = new CongNoKhachHangViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as CongNoKhachHangViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as CongNoKhachHangViewModel).Header_KhachHang;
        }

        private void dgCongNoKhachHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();
            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_KhachHang:
                    view = new rKhachHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhachHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }

        protected override void bmMenu_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;

            if (button == null)
                return;

            if (button.Content.ToString() == "Cap Nhat Cong No")
            {
                ChildWindowUtils.ShowChildWindow("Cap Nhat Cong No", new CapNhatCongNoView());
                return;
            }

            base.bmMenu_Click(sender, e);
        }
    }
}
