using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tPhuThuKhachHangView.xaml
    /// </summary>
    public partial class tPhuThuKhachHangView : BaseView<PhuDinhDataEntity.tPhuThuKhachHang>
    {
        public tPhuThuKhachHangView()
        {
            InitializeComponent();
            
            dg = dgPhuThuKhachHang;

            _viewModel = new PhuThuKhachHangViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as PhuThuKhachHangViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as PhuThuKhachHangViewModel).Header_KhachHang;
        }

        private void dgPhuThuKhachHang_HeaderAddButtonClick(object sender, EventArgs e)
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
    }
}
