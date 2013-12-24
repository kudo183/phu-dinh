using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class NhanVienModel : BaseViewModel<rNhanVien>
    {
        private List<rPhuongTien> _rPhuongTiens;
        private string _filterPhuongTien = string.Empty;

        public static HeaderTextFilterModel Header_PhuongTien = new HeaderTextFilterModel(Constant.ColumnName_PhuongTien);

        public NhanVienModel()
        {
            Entity = new ObservableCollection<rNhanVien>();

            MainFilter = new Filter_rNhanVien();

            SetReferenceFilter<rPhuongTien>(Constant.ColumnName_PhuongTien, (p => true));
        }

        public override void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_PhuongTien.Text = _filterPhuongTien;
            Header_PhuongTien.PropertyChanged += Header_PhuongTien_PropertyChanged;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterPhuongTien = Header_PhuongTien.Text;
            Header_PhuongTien.PropertyChanged -= Header_PhuongTien_PropertyChanged;
        }

        void Header_PhuongTien_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rNhanVien.TenPhuongTien, Header_PhuongTien.Text);

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
