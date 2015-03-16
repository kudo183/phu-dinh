using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiPhiView.xaml
    /// </summary>
    public partial class tChiPhiView : BaseView<PhuDinhData.tChiPhi>
    {
        public tChiPhiView()
        {
            InitializeComponent();

            dg = dgChiPhi;

            _viewModel = new ChiPhiViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as ChiPhiViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as ChiPhiViewModel).Header_LoaiChiPhi;
            dg.Columns[3].Header = (_viewModel as ChiPhiViewModel).Header_NhanVien;
        }

        private void dgChiPhi_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_NhanVien:
                    view = new rNhanVienView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NhanVien, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_LoaiChiPhi:
                    view = new rLoaiChiPhiView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_LoaiChiPhi, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
