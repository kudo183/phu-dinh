using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq.Expressions;
using PhuDinhData.Filter;
using PhuDinhData.Repository;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;

namespace PhuDinhData.ViewModel
{
    public class ChanhViewModel : BaseViewModel<rChanh>
    {
        private List<rBaiXe> _rBaiXes;
        private string _filterBaiXe = string.Empty;

        public Expression<Func<rBaiXe, bool>> RFilter_BaiXe { get; set; }

        public rBaiXe rBaiXeDefault { get; set; }

        public Filter_rChanh MainFilter { get; set; }

        public static HeaderTextFilter Header_BaiXe = new HeaderTextFilter("Bãi Xe");

        public ChanhViewModel()
        {
            Entity = new ObservableCollection<rChanh>();

            RFilter_BaiXe = (p => true);
            MainFilter = new Filter_rChanh();
        }

        public void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            ChanhViewModel.Header_BaiXe.Text = _filterBaiXe;
            ChanhViewModel.Header_BaiXe.PropertyChanged += BaiXe_PropertyChanged;
        }

        public void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterBaiXe = ChanhViewModel.Header_BaiXe.Text;
            ChanhViewModel.Header_BaiXe.PropertyChanged -= BaiXe_PropertyChanged;
        }

        void BaiXe_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rChanh.DiaDiemBaiXe, ChanhViewModel.Header_BaiXe.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chanh = e.NewItems[0] as rChanh;
                chanh.rBaiXeList = _rBaiXes;

                if (rBaiXeDefault != null)
                {
                    chanh.MaBaiXe = rBaiXeDefault.Ma;
                }
            }
        }

        public override void RefreshData()
        {
            if (MainFilter.Filter == null)
            {
                return;
            }

            _context = ContextFactory.CreateContext();

            ItemCount = rChanhRepository.GetDataCount(_context, MainFilter.Filter);
            _origData = rChanhRepository.GetData(_context, MainFilter.Filter, PageSize, CurrentPageIndex, ItemCount);

            Unload();
            Entity.Clear();

            foreach (var rChanh in _origData)
            {
                Entity.Add(rChanh);
            }

            UpdateBaiXeReferenceData();

            Load();
        }

        public void UpdateBaiXeReferenceData()
        {
            UpdateReferenceData(out _rBaiXes, RFilter_BaiXe, (p => p.rBaiXeList = _rBaiXes));
        }
    }
}
