using System;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChiTietChuyenHangDonHangView : BaseView
    {
        private readonly ChiTietChuyenHangDonHangViewModel _viewModel = new ChiTietChuyenHangDonHangViewModel();
        public ChiTietChuyenHangDonHangViewModel ViewModel { get { return _viewModel; } }

        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();

            Loaded += tChiTietChuyenHangDonHangView_Loaded;
            Unloaded += tChiTietChuyenHangDonHangView_Unloaded;

            DataContext = _viewModel;
        }

        void tChiTietChuyenHangDonHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged += _viewModel_HeaderFilterChanged;

            _viewModel.Load();

            RefreshView();
        }

        void tChiTietChuyenHangDonHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged -= _viewModel_HeaderFilterChanged;

            _viewModel.Unload();
        }

        void _viewModel_HeaderFilterChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void dgChiTietChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            BaseView view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_ChuyenHangDonHang:
                    view = new tChuyenHangDonHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_ChuyenHangDonHang, view);

                    _viewModel.UpdateChuyenHangDonHangReferenceData();
                    break;
                case Constant.ColumnName_ChiTietDonHang:
                    view = new tMatHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_ChiTietDonHang, view);

                    _viewModel.UpdateChiTietDonHangReferenceData();
                    break;
            }
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgChiTietChuyenHangDonHang.CommitEdit();
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
                LogManager.Log(event_type.et_Internal, severity_type.st_error, string.Format("{0} {1}", "tChiTietChuyenHangDonHangView_Save", "Exception"), ex);
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

            var index = dgChiTietChuyenHangDonHang.SelectedIndex;

            _viewModel.RefreshData();

            dgChiTietChuyenHangDonHang.SelectedIndex = index;
        }

        #endregion
    }
}
