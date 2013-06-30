using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

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

        private List<PhuDinhData.rKhachHang> _rKhachHangs;
        private List<PhuDinhData.rDiaDiem> _rDiaDiems;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public rKhachHangView()
        {
            InitializeComponent();

            FilterKhachHang = (p => true);
            FilterDiaDiem = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                if (FilterKhachHang == null)
                {
                    return;
                }

                var data = dgKhachHang.DataContext as ObservableCollection<PhuDinhData.rKhachHang>;
                PhuDinhData.Repository.rKhachHangRepository.Save(_context, data.ToList(), FilterKhachHang);
                RefreshView();
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
            }
        }

        public override void Cancel()
        {
            RefreshView();
        }

        public override void RefreshView()
        {
            if (FilterKhachHang == null)
            {
                dgKhachHang.DataContext = null;
                return;
            }

            _rKhachHangs = PhuDinhData.Repository.rKhachHangRepository.GetData(_context, FilterKhachHang);
            var collection = new ObservableCollection<PhuDinhData.rKhachHang>(_rKhachHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgKhachHang.DataContext = collection;

            UpdateDiaDiemReferenceData();

            dgKhachHang.UpdateLayout();
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
            var view = new rDiaDiemView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Địa điểm", view);

            UpdateDiaDiemReferenceData();
        }

        private void UpdateDiaDiemReferenceData()
        {
            _rDiaDiems = PhuDinhData.Repository.rDiaDiemRepository.GetData(_context, FilterDiaDiem);
            foreach (var rKhachHang in _rKhachHangs)
            {
                rKhachHang.rDiaDiemList = _rDiaDiems;
            }

        }
    }
}
