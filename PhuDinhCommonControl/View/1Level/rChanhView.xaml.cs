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
    /// Interaction logic for rChanhView.xaml
    /// </summary>
    public partial class rChanhView : BaseView
    {
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }
        public Expression<Func<PhuDinhData.rBaiXe, bool>> FilterBaiXe { get; set; }

        private List<PhuDinhData.rChanh> _rChanhs;
        private List<PhuDinhData.rBaiXe> _rBaiXes;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public rChanhView()
        {
            InitializeComponent();

            FilterChanh = (p => true);
            FilterBaiXe = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = dgChanh.DataContext as ObservableCollection<PhuDinhData.rChanh>;
                PhuDinhData.Repository.rChanhRepository.Save(_context, data.ToList(), FilterChanh);
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
            _rChanhs = PhuDinhData.Repository.rChanhRepository.GetData(_context, FilterChanh);

            var collection = new ObservableCollection<PhuDinhData.rChanh>(_rChanhs);
            collection.CollectionChanged += collection_CollectionChanged;
            dgChanh.DataContext = collection;

            UpdateBaiXeReferenceData();

            dgChanh.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chanh = e.NewItems[0] as PhuDinhData.rChanh;
                chanh.rBaiXeList = _rBaiXes;
            }
        }

        #endregion

        private void dgChanh_HeaderAddButtonClick(object sender, EventArgs e)
        {
            var view = new rBaiXeView();
            view.RefreshView();
            var w = new Window { Title = "Bãi Xe", Content = view };
            w.ShowDialog();

            UpdateBaiXeReferenceData();
        }

        private void UpdateBaiXeReferenceData()
        {
            _rBaiXes = PhuDinhData.Repository.rBaiXeRepository.GetData(_context, FilterBaiXe);
            foreach (var rChanh in _rChanhs)
            {
                rChanh.rBaiXeList = _rBaiXes;
            }
        }
    }
}
