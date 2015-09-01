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
    public class NhanVienViewModel : BaseViewModel<rNhanVien>
    {
        private List<rPhuongTien> _rPhuongTiens;

        public HeaderTextFilterModel Header_PhuongTien { get; set; }

        public NhanVienViewModel()
        {
            Entity = new ObservableCollection<rNhanVien>();

            MainFilter = new Filter_rNhanVien();

            Header_PhuongTien = new HeaderTextFilterModel(Constant.ColumnName_PhuongTien);
        }

        public override void Load()
        {
            _isLoading = true;

            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_PhuongTien.PropertyChanged += Header_PhuongTien_PropertyChanged;

            Header_PhuongTien_PropertyChanged(null, null);

            _isLoading = false;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            Header_PhuongTien.PropertyChanged -= Header_PhuongTien_PropertyChanged;
        }

        void Header_PhuongTien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rNhanVien.TenPhuongTien, Header_PhuongTien.GetFilterValue());

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var nhanVienGiaoHang = e.NewItems[0] as rNhanVien;
                nhanVienGiaoHang.rPhuongTienList = _rPhuongTiens;

                if (GetDefaultValue(Constant.ColumnName_MaPhuongTien) != null)
                {
                    nhanVienGiaoHang.MaPhuongTien = Convert.ToInt32(
                        GetDefaultValue(Constant.ColumnName_MaPhuongTien));
                }
            }
        }

        private void UpdatePhuongTienReferenceData()
        {
            UpdateReferenceData(out _rPhuongTiens
                , GetReferenceFilter<rPhuongTien>(Constant.ColumnName_PhuongTien)
                , (p => p.rPhuongTienList = _rPhuongTiens));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdatePhuongTienReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_PhuongTien:
                    UpdatePhuongTienReferenceData();
                    break;
            }
        }
    }
}
