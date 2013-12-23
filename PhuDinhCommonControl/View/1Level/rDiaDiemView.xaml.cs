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
        private readonly DiaDiemViewModel _viewModel = new DiaDiemViewModel();
        public DiaDiemViewModel ViewModel { get { return _viewModel; } }

        public rDiaDiemView()
        {
            InitializeComponent();

            Loaded += rDiaDiemView_Loaded;
            Unloaded += rDiaDiemView_Unloaded;

            DataContext = _viewModel;
        }

        void rDiaDiemView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged += _viewModel_FilterChanged;

            _viewModel.Load();

            RefreshView();
        }

        void rDiaDiemView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged -= _viewModel_FilterChanged;

            _viewModel.Unload();
        }

        void _viewModel_FilterChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void dgDiaDiem_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;
            
            var view = new rNuocView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_Nuoc, view);

            _viewModel.UpdateReferenceData(header.Name);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgDiaDiem.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                _viewModel.Save();
            }
            catch (Exception ex)
            {
            }

            base.Save();
        }

        public override void Cancel()
        {
            RefreshView();

            base.Cancel();
        }

        public override void RefreshView()
        {
            if (_viewModel.MainFilter.IsClearAllData)
            {
                _viewModel.Entity.Clear();
                return;
            }

            var index = dgDiaDiem.SelectedIndex;

            _viewModel.RefreshData();

            dgDiaDiem.SelectedIndex = index;
        }
        #endregion

    }
}
