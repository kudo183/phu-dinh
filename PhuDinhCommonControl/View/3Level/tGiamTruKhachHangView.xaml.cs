using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tGiamTruKhachHangView.xaml
    /// </summary>
    public partial class tGiamTruKhachHangView : BaseView<PhuDinhDataEntity.tGiamTruKhachHang>
    {
        public tGiamTruKhachHangView()
        {
            InitializeComponent();
            
            dg = dgGiamTruKhachHang;

            _viewModel = new GiamTruKhachHangViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as GiamTruKhachHangViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as GiamTruKhachHangViewModel).Header_KhachHang;
        }

        private void dgGiamTruKhachHang_HeaderAddButtonClick(object sender, EventArgs e)
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
