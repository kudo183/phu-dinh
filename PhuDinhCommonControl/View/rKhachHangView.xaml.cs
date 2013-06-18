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
                var data = this.dgKhachHang.DataContext as ObservableCollection<PhuDinhData.rKhachHang>;
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
            _rDiaDiems = PhuDinhData.Repository.rDiaDiemRepository.GetData(_context, FilterDiaDiem);

            var data = PhuDinhData.Repository.rKhachHangRepository.GetData(_context, FilterKhachHang);

            foreach (var rKhachHang in data)
            {
                rKhachHang.rDiaDiemList = _rDiaDiems;
            }

            var collection = new ObservableCollection<PhuDinhData.rKhachHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgKhachHang.DataContext = collection;

            this.dgKhachHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var khachHang = e.NewItems[0] as PhuDinhData.rKhachHang;
                khachHang.rDiaDiemList = _rDiaDiems;
            }
        }

        #endregion
    }
}
