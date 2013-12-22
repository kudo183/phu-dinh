using System;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChuyenHangDonHangView : BaseView
    {
        private readonly ChuyenHangDonHangViewModel _viewModel = new ChuyenHangDonHangViewModel();
        public ChuyenHangDonHangViewModel ViewModel { get { return _viewModel; } }

        public tChuyenHangDonHangView()
        {
            InitializeComponent();

            Loaded += tChuyenHangDonHangView_Loaded;
            Unloaded += tChuyenHangDonHangView_Unloaded;

            DataContext = _viewModel;
        }

        void tChuyenHangDonHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged += _viewModel_HeaderFilterChanged;

            _viewModel.Load();

            RefreshView();
        }

        void tChuyenHangDonHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged -= _viewModel_HeaderFilterChanged;

            _viewModel.Unload();
        }

        void _viewModel_HeaderFilterChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void dgChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            BaseView view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_ChuyenHang:
                    view = new tChuyenHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_ChuyenHang, view);

                    _viewModel.UpdateChuyenHangReferenceData();
                    break;
                case Constant.ColumnName_DonHang:
                    view = new DonHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_DonHang, view);

                    _viewModel.UpdateDonHangReferenceData();
                    break;
            }
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgChuyenHangDonHang.CommitEdit();
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
                LogManager.Log(event_type.et_Internal, severity_type.st_error, string.Format("{0} {1}", "tChuyenHangDonHangView_Save", "Exception"), ex);
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
            if (_viewModel.MainFilter.IsClearAllData == true)
            {
                _viewModel.Entity.Clear();
                return;
            }

            var index = dgChuyenHangDonHang.SelectedIndex;

            _viewModel.RefreshData();

            dgChuyenHangDonHang.SelectedIndex = index;
        }
        #endregion
    }
}
