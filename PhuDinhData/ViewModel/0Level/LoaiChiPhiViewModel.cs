using PhuDinhDataEntity;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;

namespace PhuDinhData.ViewModel
{
    public class LoaiChiPhiViewModel : BaseViewModel<rLoaiChiPhi>
    {
        public LoaiChiPhiViewModel()
        {
            Entity = new ObservableCollection<rLoaiChiPhi>();

            MainFilter = new Filter_rLoaiChiPhi();
        }

        public override void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                
            }
        }

        protected override void UpdateAllReferenceData()
        {
            
        }

        public override void UpdateReferenceData(string columnName)
        {
            
        }
    }
}
