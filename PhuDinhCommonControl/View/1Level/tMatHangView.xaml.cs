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
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class tMatHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }
        public Expression<Func<PhuDinhData.rLoaiHang, bool>> FilterLoaiHang { get; set; }

        private List<PhuDinhData.tMatHang> _tMatHangs;
        private List<PhuDinhData.rLoaiHang> _rLoaiHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tMatHangView()
        {
            InitializeComponent();

            FilterMatHang = (p => true);
            FilterLoaiHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = dgMatHang.DataContext as ObservableCollection<PhuDinhData.tMatHang>;
                PhuDinhData.Repository.tMatHangRepository.Save(_context, data.ToList(), FilterMatHang);
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
            _tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang);
            var collection = new ObservableCollection<PhuDinhData.tMatHang>(_tMatHangs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgMatHang.DataContext = collection;

            UpdateLoaiHangReferenceData();

            UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tMatHang = e.NewItems[0] as PhuDinhData.tMatHang;
                tMatHang.rLoaiHangList = _rLoaiHangs;
            }
        }

        #endregion

        private void dgMatHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            var view = new rLoaiHangView();
            view.RefreshView();
            var w = new Window { Title = "Loại Hàng", Content = view };
            w.ShowDialog();

            UpdateLoaiHangReferenceData();
        }

        private void UpdateLoaiHangReferenceData()
        {
            _rLoaiHangs = PhuDinhData.Repository.rLoaiHangRepository.GetData(_context, FilterLoaiHang);
            foreach (var tMatHang in _tMatHangs)
            {
                tMatHang.rLoaiHangList = _rLoaiHangs;
            }

        }
    }
}
