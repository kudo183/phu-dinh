using System;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rDiaDiemView.xaml
    /// </summary>
    public partial class rDiaDiemView : BaseView<PhuDinhData.rDiaDiem>
    {
        public rDiaDiemView()
        {
            InitializeComponent();

            dg = dgDiaDiem;

            _viewModel = new DiaDiemViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as DiaDiemViewModel).Header_Nuoc;
        }

        private void dgDiaDiem_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;
            
            var view = new rNuocView();
            
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_Nuoc, view);

            _viewModel.UpdateReferenceData(header.Name);
        }
    }
}
