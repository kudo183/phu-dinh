using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rKhachHangView.xaml
    /// </summary>
    public partial class rKhachHangView : BaseView
    {
        public Expression<Func<PhuDinhData.rKhachHang, bool>> FilterKhachHang { get; set; }
        public Expression<Func<PhuDinhData.rDiaDiem, bool>> FilterDiaDiem { get; set; }
        public PhuDinhData.rDiaDiem rDiaDiemDefault { get; set; }

        private ObservableCollection<PhuDinhData.rKhachHang> _rKhachHangs;
        private List<PhuDinhData.rDiaDiem> _rDiaDiems;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        public rKhachHangView()
        {
            InitializeComponent();

            FilterKhachHang = (p => true);
            FilterDiaDiem = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgKhachHang.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterKhachHang == null)
                {
                    return;
                }

                var data = dgKhachHang.DataContext as ObservableCollection<PhuDinhData.rKhachHang>;
                PhuDinhData.Repository.rKhachHangRepository.Save(_context, data.ToList(), FilterKhachHang);
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
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
            if (FilterKhachHang == null)
            {
                dgKhachHang.DataContext = null;
                return;
            }

            var index = dgKhachHang.SelectedIndex;

            if (_rKhachHangs != null)
            {
                _rKhachHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var rKhachHangs = PhuDinhData.Repository.rKhachHangRepository.GetData(_context, FilterKhachHang);

            _rKhachHangs = new ObservableCollection<PhuDinhData.rKhachHang>(rKhachHangs);
            _rKhachHangs.CollectionChanged += collection_CollectionChanged;

            UpdateDiaDiemReferenceData();

            dgKhachHang.DataContext = _rKhachHangs;

            dgKhachHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var khachHang = e.NewItems[0] as PhuDinhData.rKhachHang;
                khachHang.rDiaDiemList = _rDiaDiems;

                if (rDiaDiemDefault != null)
                {
                    khachHang.MaDiaDiem = rDiaDiemDefault.Ma;
                }
            }
        }

        #endregion

        private void dgKhachHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var view = new rDiaDiemView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Địa điểm", view);

            UpdateDiaDiemReferenceData();
        }

        private void UpdateDiaDiemReferenceData()
        {
            _rDiaDiems = PhuDinhData.Repository.rDiaDiemRepository.GetData(_context, FilterDiaDiem);

            if (_rKhachHangs == null)
            {
                return;
            }

            foreach (var rKhachHang in _rKhachHangs)
            {
                rKhachHang.rDiaDiemList = _rDiaDiems;
            }
        }
    }
}
