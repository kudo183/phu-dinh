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
    public class DiaDiemViewModel : BaseViewModel<rDiaDiem>
    {
        private List<rNuoc> _rNuocs;
        private string _filterNuoc = string.Empty;

        public Expression<Func<rNuoc, bool>> RFilter_Nuoc { get; set; }

        public rNuoc rNuocDefault { get; set; }

        public static HeaderTextFilter Header_Nuoc = new HeaderTextFilter("Nước");

        public DiaDiemViewModel()
        {
            Entity = new ObservableCollection<rDiaDiem>();

            RFilter_Nuoc = (p => true);
            MainFilter = new Filter_rDiaDiem();
        }

        public void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            DiaDiemViewModel.Header_Nuoc.Text = _filterNuoc;
            DiaDiemViewModel.Header_Nuoc.PropertyChanged += Nuoc_PropertyChanged;
        }

        public void Unload()
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

        public override void RefreshData()
        {
            if (MainFilter.Filter == null)
            {
                return;
            }

            _context = ContextFactory.CreateContext();

            ItemCount = rDiaDiemRepository.GetDataCount(_context, MainFilter.Filter);
            _origData = rDiaDiemRepository.GetData(_context, MainFilter.Filter, PageSize, CurrentPageIndex, ItemCount);

            Unload();
            Entity.Clear();

            foreach (var rDiaDiem in _origData)
            {
                Entity.Add(rDiaDiem);
            }

            UpdateNuocReferenceData();

            Load();
        }

        public void UpdateNuocReferenceData()
        {
            UpdateReferenceData(out _rNuocs, RFilter_Nuoc, (p => p.rNuocList = _rNuocs));
        }
    }
}
