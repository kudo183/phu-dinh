using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class DiaDiemViewModel : BaseViewModel<rDiaDiem>
    {
        private List<rNuoc> _rNuocs;

        public HeaderTextFilterModel Header_Nuoc { get; set; }

        public DiaDiemViewModel()
        {
            Entity = new ObservableCollection<rDiaDiem>();

            MainFilter = new Filter_rDiaDiem();

            SetReferenceFilter<rNuoc>(Constant.ColumnName_Nuoc, (p => true));

            Header_Nuoc = new HeaderTextFilterModel(Constant.ColumnName_Nuoc);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_Nuoc.PropertyChanged += Header_Nuoc_PropertyChanged;

            Header_Nuoc_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_Nuoc.PropertyChanged -= Header_Nuoc_PropertyChanged;
        }

        void Header_Nuoc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rDiaDiem.TenNuoc, Header_Nuoc.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var diadiem = e.NewItems[0] as rDiaDiem;
                diadiem.rNuocList = _rNuocs;

                if (GetDefaultValue(Constant.ColumnName_Nuoc) != null)
                {
                    diadiem.MaNuoc = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_Nuoc));
                }
            }
        }

        private void UpdateNuocReferenceData()
        {
            UpdateReferenceData(out _rNuocs
                , GetReferenceFilter<rNuoc>(Constant.ColumnName_Nuoc)
                , (p => p.rNuocList = _rNuocs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateNuocReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_Nuoc:
                    UpdateNuocReferenceData();
                    break;
            }
        }
    }
}
