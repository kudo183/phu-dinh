using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rCanhBaoTonKhoView.xaml
    /// </summary>
    public partial class rCanhBaoTonKhoView : BaseView<PhuDinhData.rCanhBaoTonKho>
    {
        public rCanhBaoTonKhoView()
        {
            InitializeComponent();

            dg = dgTonKho;

            _viewModel = new CanhBaoTonKhoViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as CanhBaoTonKhoViewModel).Header_KhoHang;
            dg.Columns[2].Header = (_viewModel as CanhBaoTonKhoViewModel).Header_MatHang;
        }

        private void dgTonKho_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_KhoHang:
                    view = new rKhoHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhoHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_MatHang:
                    view = new tMatHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_MatHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
