using System;
using PhuDinhData.ViewModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class tMatHangView : BaseView<PhuDinhData.tMatHang>
    {
        private readonly MatHangViewModel _viewModel = new MatHangViewModel();
        public MatHangViewModel ViewModel { get { return _viewModel; } }

        public tMatHangView()
        {
            InitializeComponent();

            Loaded += tMatHangView_Loaded;
            Unloaded += tMatHangView_Unloaded;

            DataContext = _viewModel;
        }

        void tMatHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged += _viewModel_FilterChanged;

            _viewModel.Load();

            RefreshView();
        }

        void tMatHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged -= _viewModel_FilterChanged;

            _viewModel.Unload();
        }

        void _viewModel_FilterChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void dgMatHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var view = new rLoaiHangView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_LoaiHang, view);

            _viewModel.UpdateLoaiHangReferenceData();
        }
        #region Override base view method
        public override void CommitEdit()
        {
            dgMatHang.CommitEdit();
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

            var index = dgMatHang.SelectedIndex;

            _viewModel.RefreshData();

            dgMatHang.SelectedIndex = index;
        }
        #endregion
    }
}
