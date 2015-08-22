using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tNhanTienKhachHangView.xaml
    /// </summary>
    public partial class tNhanTienKhachHangView : BaseView<PhuDinhDataEntity.tNhanTienKhachHang>
    {
        public tNhanTienKhachHangView()
        {
            InitializeComponent();
            
            dg = dgNhanTienKhachHang;

            _viewModel = new NhanTienKhachHangViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as NhanTienKhachHangViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as NhanTienKhachHangViewModel).Header_KhachHang;
        }

        private void dgNhanTienKhachHang_HeaderAddButtonClick(object sender, EventArgs e)
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
