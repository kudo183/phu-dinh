using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tDonHangView.xaml
    /// </summary>
    public partial class tDonHangView : BaseView
    {
        private readonly DonHangViewModel _viewModel = new DonHangViewModel();
        public DonHangViewModel ViewModel { get { return _viewModel; } }

        public tDonHangView()
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Contructor", "Enter"));

            InitializeComponent();

            Loaded += tDonHangView_Loaded;
            Unloaded += tDonHangView_Unloaded;

            DataContext = _viewModel;

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Contructor", "Exit"));
        }

        void tDonHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.HeaderFilterChanged += _viewModel_HeaderFilterChanged;

            _viewModel.Load();

            RefreshView();
        }

        void tDonHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.Unload();
        }

        void _viewModel_HeaderFilterChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void dgDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "dgDonHang_HeaderAddButtonClick", "Enter"));

            CommitEdit();
            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;
            LogManager.Log(event_type.et_Internal, severity_type.st_info, string.Format("{0} {1}", "Header Add Button Click", header.Name));

            BaseView view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_KhachHang:
                    view = new rKhachHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhachHang, view);

                    _viewModel.UpdateKhachHangReferenceData();
                    break;
                case Constant.ColumnName_KhachHangChanh:
                    view = new rKhachHangChanhView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhachHangChanh, view);

                    _viewModel.UpdateChanhReferenceData();
                    break;
            }

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "dgDonHang_HeaderAddButtonClick", "Exit"));
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgDonHang.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Save", "Enter"));

            CommitEdit();
            try
            {
                _viewModel.Save();
            }
            catch (Exception ex)
            {
                LogManager.Log(event_type.et_Internal, severity_type.st_error, string.Format("{0} {1}", "tDonHangView_Save", "Exception"), ex);
            }

            base.Save();

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Save", "Exit"));
        }

        public override void Cancel()
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Cancel", "Enter"));

            RefreshView();

            base.Cancel();

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Cancel", "Exit"));
        }

        public override void RefreshView()
        {
            if (_viewModel.MainFilter.IsClearAllData == true)
            {
                _viewModel.Entity.Clear();
                return;
            }

            var index = dgDonHang.SelectedIndex;

            _viewModel.RefreshData();

            dgDonHang.SelectedIndex = index;
        }
        #endregion
    }
}
