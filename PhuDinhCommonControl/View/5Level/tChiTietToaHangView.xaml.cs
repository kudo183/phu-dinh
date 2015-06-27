using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietToaHangView.xaml
    /// </summary>
    public partial class tChiTietToaHangView : BaseView<PhuDinhDataEntity.tChiTietToaHang>
    {
        public tChiTietToaHangView()
        {
            InitializeComponent();

            dg = dgChiTietToaHang;

            _viewModel = new ChiTietToaHangViewModel();
            DataContext = _viewModel;

            dg.Columns[2].Header = (_viewModel as ChiTietToaHangViewModel).Header_ChiTietDonHang;
        }

        private void dgChiTietToaHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_ChiTietDonHang:
                    view = new tChiTietDonHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_ChiTietDonHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
