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
    /// Interaction logic for tChiPhiNhanVienGiaoHangView.xaml
    /// </summary>
    public partial class tChiPhiNhanVienGiaoHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterChiPhiNhanVienGiaoHang { get; set; }
        public Expression<Func<PhuDinhData.rLoaiChiPhi, bool>> FilterLoaiChiPhi { get; set; }
        public Expression<Func<PhuDinhData.rNhanVienGiaoHang, bool>> FilterNhanVienGiaoHang { get; set; }

        private List<PhuDinhData.rLoaiChiPhi> _rLoaiChiPhis;
        private List<PhuDinhData.rNhanVienGiaoHang> _rNhanVienGiaoHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChiPhiNhanVienGiaoHangView()
        {
            InitializeComponent();

            FilterChiPhiNhanVienGiaoHang = (p => true);
            FilterLoaiChiPhi = (p => true);
            FilterNhanVienGiaoHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgChiPhiNhanVienGiaoHang.DataContext as ObservableCollection<PhuDinhData.tChiPhiNhanVienGiaoHang>;
                PhuDinhData.Repository.tChiPhiNhanVienGiaoHangRepository.Save(_context, data.ToList(), FilterChiPhiNhanVienGiaoHang);
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
            _rLoaiChiPhis = PhuDinhData.Repository.rLoaiChiPhiRepository.GetData(_context, FilterLoaiChiPhi);
            _rNhanVienGiaoHangs = PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(_context, FilterNhanVienGiaoHang);

            var data = PhuDinhData.Repository.tChiPhiNhanVienGiaoHangRepository.GetData(_context, FilterChiPhiNhanVienGiaoHang);

            foreach (var tChiPhiNhanVienGiaoHang in data)
            {
                tChiPhiNhanVienGiaoHang.rLoaiChiPhiList = _rLoaiChiPhis;
                tChiPhiNhanVienGiaoHang.rNhanVienGiaoHangList = _rNhanVienGiaoHangs;
            }

            var collection = new ObservableCollection<PhuDinhData.tChiPhiNhanVienGiaoHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgChiPhiNhanVienGiaoHang.DataContext = collection;

            this.dgChiPhiNhanVienGiaoHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chiPhiNhanVienGiaoHang = e.NewItems[0] as PhuDinhData.tChiPhiNhanVienGiaoHang;
                chiPhiNhanVienGiaoHang.Ngay = DateTime.Now;
            }
        }

        #endregion
    }
}
