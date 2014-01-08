using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class NguyenLieuViewModel : BaseViewModel<rNguyenLieu>
    {
        private List<rLoaiNguyenLieu> _rLoaiNguyenLieus;
        private string _filterLoaiNguyenLieu = string.Empty;

        public static HeaderTextFilterModel Header_LoaiNguyenLieu = new HeaderTextFilterModel(Constant.ColumnName_LoaiNguyenLieu);

        public NguyenLieuViewModel()
        {
            Entity = new ObservableCollection<rNguyenLieu>();

            MainFilter = new Filter_rNguyenLieu();

            SetReferenceFilter<rLoaiNguyenLieu>(Constant.ColumnName_LoaiNguyenLieu, (p => true));
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_LoaiNguyenLieu.Text = _filterLoaiNguyenLieu;
            Header_LoaiNguyenLieu.PropertyChanged += Header_LoaiNguyenLieu_PropertyChanged;

            Header_LoaiNguyenLieu_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterLoaiNguyenLieu = Header_LoaiNguyenLieu.Text;
            Header_LoaiNguyenLieu.PropertyChanged -= Header_LoaiNguyenLieu_PropertyChanged;
        }

        void Header_LoaiNguyenLieu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rNguyenLieu.TenLoaiNguyenLieu, Header_LoaiNguyenLieu.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var NguyenLieu = e.NewItems[0] as rNguyenLieu;
                NguyenLieu.rLoaiNguyenLieuList = _rLoaiNguyenLieus;

                if (GetDefaultValue(Constant.ColumnName_MaLoaiNguyenLieu) != null)
                {
                    NguyenLieu.MaLoaiNguyenLieu = Convert.ToInt32(
                        GetDefaultValue(Constant.ColumnName_MaLoaiNguyenLieu));
                }
            }
        }

        private void UpdateLoaiNguyenLieuReferenceData()
        {
            UpdateReferenceData(out _rLoaiNguyenLieus
                , GetReferenceFilter<rLoaiNguyenLieu>(Constant.ColumnName_LoaiNguyenLieu)
                , (p => p.rLoaiNguyenLieuList = _rLoaiNguyenLieus));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateLoaiNguyenLieuReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_LoaiNguyenLieu:
                    UpdateLoaiNguyenLieuReferenceData();
                    break;
            }
        }
    }
}
