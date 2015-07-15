using PhuDinhDataEntity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using System;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhData.ViewModel
{
    public class ChanhViewModel : BaseViewModel<rChanh>
    {
        private List<rBaiXe> _rBaiXes;

        public HeaderTextFilterModel Header_BaiXe { get; set; }

        public ChanhViewModel()
        {
            Entity = new ObservableCollection<rChanh>();

            MainFilter = new Filter_rChanh();

            Header_BaiXe = new HeaderTextFilterModel(Constant.ColumnName_BaiXe);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_BaiXe.PropertyChanged += Header_BaiXe_PropertyChanged;

            Header_BaiXe_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_BaiXe.PropertyChanged -= Header_BaiXe_PropertyChanged;
        }

        void Header_BaiXe_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rChanh.DiaDiemBaiXe, Header_BaiXe.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chanh = e.NewItems[0] as rChanh;
                chanh.rBaiXeList = _rBaiXes;

                if (GetDefaultValue(Constant.ColumnName_MaBaiXe) != null)
                {
                    chanh.MaBaiXe = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaBaiXe));
                }
            }
        }

        private void UpdateBaiXeReferenceData()
        {
            UpdateReferenceData(out _rBaiXes, GetReferenceFilter<rBaiXe>(Constant.ColumnName_BaiXe), (p => p.rBaiXeList = _rBaiXes));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateBaiXeReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_BaiXe:
                    UpdateBaiXeReferenceData();
                    break;
            }
        }
    }
}
