using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiPhiNhanVienGiaoHangView.xaml
    /// </summary>
    public partial class tChiPhiNhanVienGiaoHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterChiPhiNhanVienGiaoHang { get; set; }
        public Expression<Func<PhuDinhData.rLoaiChiPhi, bool>> FilterLoaiChiPhi { get; set; }
        public Expression<Func<PhuDinhData.rNhanVienGiaoHang, bool>> FilterNhanVienGiaoHang { get; set; }

        public tChiPhiNhanVienGiaoHangView()
        {
            InitializeComponent();

            FilterChiPhiNhanVienGiaoHang = (p => true);
            FilterLoaiChiPhi = (p => true);
            FilterNhanVienGiaoHang = (p => true);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgChiPhiNhanVienGiaoHang.DataContext as ObservableCollection<PhuDinhData.tChiPhiNhanVienGiaoHang>;
                PhuDinhData.Repository.tChiPhiNhanVienGiaoHangRepository.Save(data.ToList(), FilterChiPhiNhanVienGiaoHang);
                RefreshView();
            }
            catch (Exception ex)
            {
            }
        }

        public override void Cancel()
        {
            RefreshView();
        }

        public override void RefreshView()
        {
            var context = new PhuDinhData.PhuDinhEntities();
            PhuDinhData.tChiPhiNhanVienGiaoHang.rLoaiChiPhis = PhuDinhData.Repository.rLoaiChiPhiRepository.GetData(context, FilterLoaiChiPhi);
            PhuDinhData.tChiPhiNhanVienGiaoHang.rNhanVienGiaoHangs =
                PhuDinhData.Repository.rNhanVienGiaoHangRepository.GetData(context, FilterNhanVienGiaoHang);

            var data = PhuDinhData.Repository.tChiPhiNhanVienGiaoHangRepository.GetData(context, FilterChiPhiNhanVienGiaoHang);

            foreach (var tChiPhiNhanVienGiaoHang in data)
            {
            }

            var collection = new ObservableCollection<PhuDinhData.tChiPhiNhanVienGiaoHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgChiPhiNhanVienGiaoHang.DataContext = collection;

            this.dgChiPhiNhanVienGiaoHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chiPhiNhanVienGiaoHang = e.NewItems[0] as PhuDinhData.tChiPhiNhanVienGiaoHang;
                chiPhiNhanVienGiaoHang.Ngay = DateTime.Now;
            }
        }

        #endregion
    }
}
