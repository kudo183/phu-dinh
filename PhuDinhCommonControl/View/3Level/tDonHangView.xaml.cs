using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Common;
using CustomControl;
using PhuDinhCommon;
using PhuDinhCommon.PrintTemplate;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tDonHangView.xaml
    /// </summary>
    public partial class tDonHangView : BaseView<PhuDinhDataEntity.tDonHang>
    {
        public tDonHangView()
        {
            InitializeComponent();

            dg = dgDonHang;

            _viewModel = new DonHangViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as DonHangViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as DonHangViewModel).Header_KhachHang;
            dg.Columns[3].Header = (_viewModel as DonHangViewModel).Header_KhoHang;
            dg.Columns[4].Header = (_viewModel as DonHangViewModel).Header_Chanh;
        }

        private void dgDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();
            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_KhachHang:
                    view = new rKhachHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhachHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_Chanh:
                    view = new rKhachHangChanhView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhachHangChanh, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_KhoHang:
                    view = new rKhoHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhoHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }

        protected override void bmMenu_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;

            if (button == null)
                return;

            if (button.Content.ToString() == "In")
            {
                var tChiTietDonHangs = PhuDinhData.ClientContext.Instance
                    .GetDataWithRelated<PhuDinhDataEntity.tChiTietDonHang>(
                    p => p.MaDonHang == SelectedItem.Ma,
                    new List<string> { "tMatHang" }).ToList();

                var content = tChiTietDonHangs.Select(
                    ctdh => string.Format("{0,3}  {1}", ctdh.SoLuong, ctdh.tMatHang.TenMatHangIn)).ToList();

                var rKhachHang = PhuDinhData.ClientContext.Instance
                    .GetData<PhuDinhDataEntity.rKhachHang>(p => p.Ma == SelectedItem.MaKhachHang).ToList()[0];

                var document = new PrintTemplateDonHang
                {
                    Title = rKhachHang.TenKhachHang,
                    Content = new ReadOnlyCollection<string>(content)
                };

                PrintService.Print(document.Document);
                return;
            }
            else if (button.Content.ToString() == "Ton Kho")
            {
                var tChiTietDonHangs = PhuDinhData.ClientContext.Instance
                    .GetDataWithRelated<PhuDinhDataEntity.tChiTietDonHang>(
                    p => p.MaDonHang == SelectedItem.Ma,
                    new List<string>()).ToList();

                var maMatHangs = tChiTietDonHangs.Select(p => p.MaMatHang).ToList();

                var tTonKhoes = PhuDinhData.ClientContext.Instance
                    .GetDataWithRelated<PhuDinhDataEntity.tTonKho>(p => p.Ngay == SelectedItem.Ngay
                    && p.MaKhoHang == SelectedItem.MaKhoHang
                    && maMatHangs.Contains(p.MaMatHang),
                    new List<string> { "tMatHang" });

                var data = new List<CustomControl.MessageBox2.MessageBox2Data>();

                foreach (var tTonKho in tTonKhoes)
                {
                    data.Add(new MessageBox2.MessageBox2Data
                    {
                        Title = tTonKho.tMatHang.TenMatHangDayDu,
                        Content = tTonKho.SoLuong.ToString("N0")
                    });
                }

                CustomControl.MessageBox2.Show("Ton Kho", data);
            }

            base.bmMenu_Click(sender, e);
        }
    }
}
