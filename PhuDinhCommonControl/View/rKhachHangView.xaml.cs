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
    /// Interaction logic for rKhachHangView.xaml
    /// </summary>
    public partial class rKhachHangView : BaseView
    {
        public Expression<Func<PhuDinhData.rKhachHang, bool>> FilterKhachHang { get; set; }
        public Expression<Func<PhuDinhData.rDiaDiem, bool>> FilterDiaDiem { get; set; }

        private List<PhuDinhData.rDiaDiem> _rDiaDiems;

        public rKhachHangView()
        {
            InitializeComponent();

            FilterKhachHang = (p => true);
            FilterDiaDiem = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgKhachHang.DataContext as ObservableCollection<PhuDinhData.rKhachHang>;
                PhuDinhData.Repository.rKhachHangRepository.Save(data.ToList(), FilterKhachHang);
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
            _rDiaDiems = PhuDinhData.Repository.rDiaDiemRepository.GetData(context, FilterDiaDiem);

            var data = PhuDinhData.Repository.rKhachHangRepository.GetData(context, FilterKhachHang);

            foreach (var rKhachHang in data)
            {
                rKhachHang.DiaDiem = _rDiaDiems.FirstOrDefault(
                    p => p.Ma == rKhachHang.MaDiaDiem);

                rKhachHang.rDiaDiemList = _rDiaDiems;
            }

            var collection = new ObservableCollection<PhuDinhData.rKhachHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgKhachHang.DataContext = collection;

            this.dgKhachHang.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var khachHang = e.NewItems[0] as PhuDinhData.rKhachHang;
                khachHang.rDiaDiemList = _rDiaDiems;
            }
        }

        #endregion
    }
}
