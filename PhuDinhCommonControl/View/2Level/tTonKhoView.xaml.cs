using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tTonKhoView.xaml
    /// </summary>
    public partial class tTonKhoView : BaseView<PhuDinhDataEntity.tTonKho>
    {
        public tTonKhoView()
        {
            InitializeComponent();

            dg = dgTonKho;

            _viewModel = new TonKhoViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as TonKhoViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as TonKhoViewModel).Header_KhoHang;
            dg.Columns[3].Header = (_viewModel as TonKhoViewModel).Header_MatHang;
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

        protected override void bmMenu_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender != e.OriginalSource)
                return;

            var button = e.OriginalSource as Button;
            if (button.Content.ToString() == "Cap Nhat Ton Kho")
            {
                ChildWindowUtils.ShowChildWindow("Cap Nhat Ton Kho", new CapNhatTonKhoView());
                return;
            }

            base.bmMenu_Click(sender, e);
        }
    }
}
