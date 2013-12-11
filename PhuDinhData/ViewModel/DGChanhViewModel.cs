using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PhuDinhData.Filter;
using PhuDinhData.Repository;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;

namespace PhuDinhData.ViewModel
{
    public class DGChanhViewModel
    {
        private PhuDinhEntities _context;
        private List<rChanh> _origData;
        private List<rBaiXe> _rBaiXes;
        private string _filterBaiXe = string.Empty;

        public PhuDinhEntities Context { get { return _context; } }

        public List<rBaiXe> rBaiXe { get { return _rBaiXes; } }

        public Expression<Func<rBaiXe, bool>> FilterBaiXe { get; set; }

        public ObservableCollection<rChanh> Entity { get; set; }

        public rBaiXe rBaiXeDefault { get; set; }

        public Filter_rChanh FilterChanh { get; set; }

        public static HeaderTextFilter BaiXe = new HeaderTextFilter("Bãi Xe");

        public event EventHandler FilterChanged;

        public DGChanhViewModel()
        {
            Entity = new ObservableCollection<rChanh>();

            FilterBaiXe = (p => true);
            FilterChanh = new Filter_rChanh();            
        }

        public void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            DGChanhViewModel.BaiXe.Text = _filterBaiXe;
            DGChanhViewModel.BaiXe.PropertyChanged += BaiXe_PropertyChanged;
        }

        public void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterBaiXe = DGChanhViewModel.BaiXe.Text;
            DGChanhViewModel.BaiXe.PropertyChanged -= BaiXe_PropertyChanged;
        }

        void BaiXe_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DGChanhViewModel.BaiXe.Text) == false)
            {
                FilterChanh.FilterBaiXe = (p => p.rBaiXe.DiaDiemBaiXe.Contains(DGChanhViewModel.BaiXe.Text));
            }
            else
            {
                FilterChanh.FilterBaiXe = (p => true);
            }

            if (FilterChanged != null)
            {
                FilterChanged(this, null);
            }
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chanh = e.NewItems[0] as rChanh;
                chanh.rBaiXeList = rBaiXe;

                if (rBaiXeDefault != null)
                {
                    chanh.MaBaiXe = rBaiXeDefault.Ma;
                }
            }
        }

        public void RefreshData()
        {
            _context = ContextFactory.CreateContext();
            _origData = rChanhRepository.GetData(_context, FilterChanh.FilterChanh);

            Entity.Clear();

            var rChanhs = new ObservableCollection<rChanh>(_origData);

            foreach (var rChanh in rChanhs)
            {
                Entity.Add(rChanh);
            }

            UpdateBaiXeReferenceData();
        }

        public void UpdateBaiXeReferenceData()
        {
            _rBaiXes = rBaiXeRepository.GetData(_context, FilterBaiXe);

            if (Entity == null)
            {
                return;
            }

            foreach (var rChanh in Entity)
            {
                rChanh.rBaiXeList = _rBaiXes;
            }
        }

        public List<Repository<rChanh>.ChangedItemData> Save()
        {
            try
            {
                return rChanhRepository.Save(_context, Entity.ToList(), _origData);
            }
            catch (Exception)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);                
                throw;
            }
        }
    }
}
