using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rChanhView.xaml
    /// </summary>
    public partial class rChanhView : BaseView
    {
        private readonly DGChanhViewModel _viewModel = new DGChanhViewModel();
        public DGChanhViewModel ViewModel { get { return _viewModel; } }

        public rChanhView()
        {
            InitializeComponent();

            Loaded += rChanhView_Loaded;
            Unloaded += rChanhView_Unloaded;

            dgChanh.DataContext = _viewModel;
        }

        void rChanhView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.FilterChanged += _viewModel_FilterChanged;
            
            _viewModel.Load();

            RefreshView();
        }

        void rChanhView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.FilterChanged -= _viewModel_FilterChanged;

            _viewModel.Unload();
        }

        void _viewModel_FilterChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void dgChanh_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var view = new rBaiXeView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Bãi Xe", view);

            _viewModel.UpdateBaiXeReferenceData();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgChanh.CommitEdit();
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
            if (_viewModel.FilterChanh.IsClearAllData)
            {
                _viewModel.Entity.Clear();
                return;
            }

            var index = dgChanh.SelectedIndex;

            _viewModel.RefreshData();

            dgChanh.SelectedIndex = index;
        }
        #endregion
    }
}
