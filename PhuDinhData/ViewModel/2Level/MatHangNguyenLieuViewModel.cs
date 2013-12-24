using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhuDinhData.Filter;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;
using PhuDinhCommon;

namespace PhuDinhData.ViewModel
{
    public class MatHangNguyenLieuViewModel : BaseViewModel<rMatHangNguyenLieu>
    {
        private List<tMatHang> _tMatHangs;
        private List<rNguyenLieu> _rNguyenLieus;

        private string _filterMatHang = string.Empty;
        private string _filterNguyenLieu = string.Empty;

        public static HeaderTextFilterModel Header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
        public static HeaderTextFilterModel Header_NguyenLieu = new HeaderTextFilterModel(Constant.ColumnName_NguyenLieu);

        public MatHangNguyenLieuViewModel()
        {
            Entity = new ObservableCollection<rMatHangNguyenLieu>();

            MainFilter = new Filter_rMatHangNguyenLieu();

            SetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang, (p => true));
            SetReferenceFilter<rNguyenLieu>(Constant.ColumnName_NguyenLieu, (p => true));
        }

        public override void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            Header_MatHang.Text = _filterMatHang;
            Header_MatHang.PropertyChanged += Header_MatHang_PropertyChanged;

            Header_NguyenLieu.Text = _filterNguyenLieu;
            Header_NguyenLieu.PropertyChanged += Header_NguyenLieu_PropertyChanged;
        }

        public override void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterMatHang = Header_MatHang.Text;
            Header_MatHang.PropertyChanged -= Header_MatHang_PropertyChanged;

            _filterNguyenLieu = Header_NguyenLieu.Text;
            Header_NguyenLieu.PropertyChanged -= Header_NguyenLieu_PropertyChanged;
        }

        void Header_MatHang_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rMatHangNguyenLieu.TenMatHang, Header_MatHang.Text);

            OnHeaderFilterChanged();
        }

        void Header_NguyenLieu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilter(Filter_rMatHangNguyenLieu.TenNguyenLieu, Header_NguyenLieu.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var rMatHangNguyenLieu = e.NewItems[0] as rMatHangNguyenLieu;
                rMatHangNguyenLieu.tMatHangList = _tMatHangs;
                rMatHangNguyenLieu.rNguyenLieuList = _rNguyenLieus;

                if (GetDefaultValue(Constant.ColumnName_MaMatHang) != null)
                {
                    rMatHangNguyenLieu.MaMatHang = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaMatHang));
                }

                if (GetDefaultValue(Constant.ColumnName_MaNguyenLieu) != null)
                {
                    rMatHangNguyenLieu.MaNguyenLieu = Convert.ToInt32(GetDefaultValue(Constant.ColumnName_MaNguyenLieu));
                }
            }
        }

        private void UpdateMatHangReferenceData()
        {
            UpdateReferenceData(out _tMatHangs
                , GetReferenceFilter<tMatHang>(Constant.ColumnName_MatHang)
                , (p => p.tMatHangList = _tMatHangs));
        }

        private void UpdateNguyenLieuReferenceData()
        {
            UpdateReferenceData(out _rNguyenLieus
                , GetReferenceFilter<rNguyenLieu>(Constant.ColumnName_NguyenLieu)
                , (p => p.rNguyenLieuList = _rNguyenLieus));
        }

        protected override void UpdateAllReferenceData()
        {
            UpdateMatHangReferenceData();
            UpdateNguyenLieuReferenceData();
        }

        public override void UpdateReferenceData(string columnName)
        {
            switch (columnName)
            {
                case Constant.ColumnName_MatHang:
                    UpdateMatHangReferenceData();
                    break;
                case Constant.ColumnName_NguyenLieu:
                    UpdateNguyenLieuReferenceData();
                    break;
            }
        }
    }
}
