using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tDonHangView.xaml
    /// </summary>
    public partial class tDonHangView : BaseView
    {
        public Filter_tDonHang FilterDonHang { get; private set; }
        public Expression<Func<PhuDinhData.rKhachHang, bool>> FilterKhachHang { get; set; }
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }
        public PhuDinhData.rKhachHang rKhachHangDefault { get; set; }

        private ObservableCollection<PhuDinhData.tDonHang> _tDonHangs;
        private List<PhuDinhData.rKhachHang> _rKhachHangs;
        private List<PhuDinhData.rChanh> _rChanhs;

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private bool _isUsedDateFilter = true;
        private DateTime _filterDate = DateTime.Now.Date;

        private string _filterKhachHang = string.Empty;

        private string _filterChanh = string.Empty;

        public tDonHangView()
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Contructor", "Enter"));

            InitializeComponent();

            FilterDonHang = new Filter_tDonHang();
            FilterKhachHang = (p => true);
            FilterChanh = (p => true);

            Loaded += tDonHangView_Loaded;
            Unloaded += tDonHangView_Unloaded;

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Contructor", "Exit"));
        }

        void tDonHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Loaded", "Enter"));
            LogManager.Log(event_type.et_Internal, severity_type.st_info, "Enter tDonHangView");

            DataGridColumnHeaderDateFilter.DonHang.Date = _filterDate;
            DataGridColumnHeaderDateFilter.DonHang.IsUsed = _isUsedDateFilter;
            DataGridColumnHeaderDateFilter.DonHang.PropertyChanged += DonHang_PropertyChanged;
            FilterDonHang.FilterNgay = (p => p.Ngay == DataGridColumnHeaderDateFilter.DonHang.Date);

            DataGridColumnHeaderTextFilter.DonHang_KhachHang.Text = _filterKhachHang;
            DataGridColumnHeaderTextFilter.DonHang_KhachHang.PropertyChanged += DonHang_KhachHang_PropertyChanged;

            DataGridColumnHeaderTextFilter.DonHang_KhachHangChanh.Text = _filterChanh;
            DataGridColumnHeaderTextFilter.DonHang_KhachHangChanh.PropertyChanged += DonHang_Chanh_PropertyChanged;

            RefreshView();

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Loaded", "Exit"));
        }

        void tDonHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Unloaded", "Enter"));

            DataGridColumnHeaderDateFilter.DonHang.PropertyChanged -= DonHang_PropertyChanged;
            _filterDate = DataGridColumnHeaderDateFilter.DonHang.Date;
            _isUsedDateFilter = DataGridColumnHeaderDateFilter.DonHang.IsUsed;

            DataGridColumnHeaderTextFilter.DonHang_KhachHangChanh.PropertyChanged -= DonHang_Chanh_PropertyChanged;
            _filterChanh = DataGridColumnHeaderTextFilter.DonHang_KhachHangChanh.Text;

            DataGridColumnHeaderTextFilter.DonHang_KhachHang.PropertyChanged -= DonHang_KhachHang_PropertyChanged;
            _filterKhachHang = DataGridColumnHeaderTextFilter.DonHang_KhachHang.Text;

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Unloaded", "Exit"));
        }

        void DonHang_KhachHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_DonHang_KhachHang_PropertyChanged", "Enter"));
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "FilterKhachHang", FilterDonHang.FilterKhachHang));

            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.DonHang_KhachHang.Text) == false)
            {
                FilterDonHang.FilterKhachHang = (p => p.rKhachHang.TenKhachHang.Contains(DataGridColumnHeaderTextFilter.DonHang_KhachHang.Text));
            }
            else
            {
                FilterDonHang.FilterKhachHang = (p => true);
            }

            RefreshView();

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_DonHang_KhachHang_PropertyChanged", "Exit"));
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "FilterKhachHang", FilterDonHang.FilterKhachHang));
        }

        void DonHang_Chanh_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_DonHang_Chanh_PropertyChanged", "Enter"));
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "FilterChanh", FilterDonHang.FilterChanh));

            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.DonHang_KhachHangChanh.Text) == false)
            {
                FilterDonHang.FilterChanh = (p => p.rChanh.TenChanh.Contains(DataGridColumnHeaderTextFilter.DonHang_KhachHangChanh.Text));
            }
            else
            {
                FilterDonHang.FilterChanh = (p => true);
            }

            RefreshView();

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_DonHang_Chanh_PropertyChanged", "Exit"));
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "FilterChanh", FilterDonHang.FilterChanh));
        }

        void DonHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_DonHang_PropertyChanged", "Enter"));
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "FilterDonHang", FilterDonHang.FilterDonHang));

            if (DataGridColumnHeaderDateFilter.DonHang.IsUsed)
            {
                FilterDonHang.FilterNgay = (p => p.Ngay == DataGridColumnHeaderDateFilter.DonHang.Date);
            }
            else
            {
                FilterDonHang.FilterNgay = (p => true);
            }

            RefreshView();

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_DonHang_PropertyChanged", "Exit"));
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "FilterDonHang", FilterDonHang.FilterDonHang));
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
                if (FilterDonHang == null)
                {
                    return;
                }

                var data = dgDonHang.DataContext as ObservableCollection<PhuDinhData.tDonHang>;
                PhuDinhData.Repository.tDonHangRepository.Save(_context, data.ToList(), FilterDonHang.FilterDonHang);
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
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
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_RefreshView", "Enter"));
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "FilterDonHang", FilterDonHang.FilterDonHang));

            if (FilterDonHang.IsClearAllData == true)
            {
                dgDonHang.DataContext = null;
                return;
            }

            var index = dgDonHang.SelectedIndex;

            if (_tDonHangs != null)
            {
                _tDonHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang.FilterDonHang);

            _tDonHangs = new ObservableCollection<PhuDinhData.tDonHang>(tDonHangs);
            LogManager.Log(event_type.et_Internal, severity_type.st_info, string.Format("{0} {1} {2}", "tDonHangView_RefreshView", "Count", _tDonHangs.Count));
            _tDonHangs.CollectionChanged += collection_CollectionChanged;

            UpdateChanhReferenceData();
            UpdateKhachHangReferenceData();

            dgDonHang.DataContext = _tDonHangs;

            dgDonHang.SelectedIndex = index;

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_RefreshView", "Exit"));
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_collection_CollectionChanged", "Enter"));

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tDonHang = e.NewItems[0] as PhuDinhData.tDonHang;
                tDonHang.Ngay = (DataGridColumnHeaderDateFilter.DonHang.IsUsed) ? DataGridColumnHeaderDateFilter.DonHang.Date : DateTime.Now;
                tDonHang.rChanhList = _rChanhs;
                tDonHang.rKhachHangList = _rKhachHangs;

                if (rKhachHangDefault != null)
                {
                    tDonHang.MaKhachHang = rKhachHangDefault.Ma;
                }
            }

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_collection_CollectionChanged", "Exit"));
        }

        #endregion

        private void dgDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "dgDonHang_HeaderAddButtonClick", "Enter"));

            CommitEdit();
            var header = (sender as DataGridColumnHeader).Content as DataGridColumnHeaderTextFilter;
            LogManager.Log(event_type.et_Internal, severity_type.st_info, string.Format("{0} {1}", "Header Add Button Click", header.Name));

            BaseView view = null;

            switch (header.Name)
            {
                case Constant.ViewName_KhachHang:
                    view = new rKhachHangView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow(header.Name, view);

                    UpdateKhachHangReferenceData();
                    break;
                case Constant.ViewName_KhachHangChanh:
                    view = new rKhachHangChanhView();
                    view.RefreshView();
                    ChildWindowUtils.ShowChildWindow(header.Name, view);

                    UpdateChanhReferenceData();
                    break;
            }

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "dgDonHang_HeaderAddButtonClick", "Exit"));
        }

        private void UpdateChanhReferenceData()
        {
            _rChanhs = PhuDinhData.Repository.rChanhRepository.GetData(_context, FilterChanh);

            if (_tDonHangs == null)
            {
                return;
            }

            foreach (var tDonHang in _tDonHangs)
            {
                tDonHang.rChanhList = _rChanhs;
            }
        }

        private void UpdateKhachHangReferenceData()
        {
            _rKhachHangs = PhuDinhData.Repository.rKhachHangRepository.GetData(_context, FilterKhachHang);

            if (_tDonHangs == null)
            {
                return;
            }

            foreach (var tDonHang in _tDonHangs)
            {
                tDonHang.rKhachHangList = _rKhachHangs;
            }
        }
    }
}
