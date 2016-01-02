using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tTonKhoView.xaml
    /// </summary>
    public partial class tTonKhoView : BaseView<PhuDinhDataEntity.tTonKho>
    {
        public tTonKhoView()
        {
            InitializeComponent();

            dg = dgTonKho;

            _viewModel = new TonKhoViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as TonKhoViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as TonKhoViewModel).Header_KhoHang;
            dg.Columns[3].Header = (_viewModel as TonKhoViewModel).Header_MatHang;
        }

        private void dgTonKho_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_KhoHang:
                    view = new rKhoHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhoHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_MatHang:
                    view = new tMatHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_MatHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }

        protected override void bmMenu_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;

            if (button == null)
                return;

            if (button.Content.ToString() == "Cap Nhat Ton Kho")
            {
                ChildWindowUtils.ShowChildWindow("Cap Nhat Ton Kho", new CapNhatTonKhoView());
                return;
            }

            if (button.Content.ToString() == "Copy Ton Kho Hang Nhap")
            {
                var maLoaiHang_HangNhap = new[] { 11, 1026, 1025, 1024, 17, 21, 1023, 8, 15 };
                var filterDate = (_viewModel as TonKhoViewModel).Header_Ngay.Date;
                var tTonKhos = PhuDinhData.ClientContext.Instance
                   .GetDataWithRelated<PhuDinhDataEntity.tTonKho>(
                       p => p.Ngay == filterDate && maLoaiHang_HangNhap.Contains(p.tMatHang.MaLoai),
                       new List<string> { "tMatHang" })
                   .OrderBy(p => p.tMatHang.TenMatHangDayDu)
                   .ToList();

                var builder = new StringBuilder();

                builder.AppendLine(string.Format("{0:dd/MM/yyyy}\t\t{1:N0} cuộn"
                    , filterDate, tTonKhos.Sum(p => p.SoLuong)));

                int count = tTonKhos.Count;
                var half = count / 2;
                for (int i = 0; i < half; i++)
                {
                    builder.Append(tTonKhos[i].tMatHang.TenMatHangDayDu);
                    builder.Append("\t");
                    builder.Append(tTonKhos[i].SoLuong);
                    builder.Append("\t\t");
                    builder.Append(tTonKhos[half + i].tMatHang.TenMatHangDayDu);
                    builder.Append("\t");
                    builder.Append(tTonKhos[half + i].SoLuong);
                    builder.AppendLine("");
                }

                if ((count & 1) == 1)
                {
                    builder.Append(tTonKhos[count - 1].tMatHang.TenMatHangDayDu);
                    builder.Append("\t");
                    builder.Append(tTonKhos[count - 1].SoLuong);
                    builder.AppendLine("");
                }

                Clipboard.SetText(builder.ToString());
                return;
            }

            base.bmMenu_Click(sender, e);
        }
    }
}
