using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq.Expressions;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;

namespace PhuDinhData.ViewModel
{
    public class DiaDiemViewModel : BaseViewModel<rDiaDiem>
    {
        private List<rNuoc> _rNuocs;
        private string _filterNuoc = string.Empty;

        public Expression<Func<rNuoc, bool>> RFilter_Nuoc { get; set; }

        public rNuoc rNuocDefault { get; set; }

        public static HeaderTextFilterModel Header_Nuoc = new HeaderTextFilterModel("Nước");

        public DiaDiemViewModel()
        {
            Entity = new ObservableCollection<rDiaDiem>();

            RFilter_Nuoc = (p => true);
            MainFilter = new Filter_rDiaDiem();
        }

        public override void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            DiaDiemViewModel.Header_Nuoc.Text = _filterNuoc;
            DiaDiemViewModel.Header_Nuoc.PropertyChanged += Nuoc_PropertyChanged;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterNuoc = DiaDiemViewModel.Header_Nuoc.Text;
            DiaDiemViewModel.Header_Nuoc.PropertyChanged -= Nuoc_PropertyChanged;
        }

        void Nuoc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rDiaDiem.TenNuoc, DiaDiemViewModel.Header_Nuoc.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var diadiem = e.NewItems[0] as rDiaDiem;
                diadiem.rNuocList = _rNuocs;

                if (rNuocDefault != null)
                {
                    diadiem.MaNuoc = rNuocDefault.Ma;
                }
            }
        }

        public void UpdateNuocReferenceData()
        {
            UpdateReferenceData(out _rNuocs, RFilter_Nuoc, (p => p.rNuocList = _rNuocs));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateNuocReferenceData();
        }
    }
}
