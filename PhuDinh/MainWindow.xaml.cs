using System.Windows;
using PhuDinhCommon;
using PhuDinhCommonControl;

namespace PhuDinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _spMenu.MenuItemClick += _spMenu_MenuItemClick;
        }

        void _spMenu_MenuItemClick(object sender, RoutedEventArgs e)
        {
            var menu = sender as PhuDinhCommonControl.Menu.MainMenu;
            BaseView view = CreateView(menu.SelectedItem);

            _brdMain.Child = view;

            if (view != null)
            {
                view.RefreshView();
            }
        }

        private BaseView CreateView(Constant.MainMenuItems selectedView)
        {
            BaseView result = null;
            switch (selectedView)
            {
                case Constant.MainMenuItems.rBaiXe:
                    result = new rBaiXeView();
                    break;
                case Constant.MainMenuItems.rChanh:
                    result = new rChanhView();
                    break;
                case Constant.MainMenuItems.rKhachHang:
                    result = new rKhachHangView();
                    break;
                case Constant.MainMenuItems.rLoaiChiPhi:
                    result = new rLoaiChiPhiView();
                    break;
                case Constant.MainMenuItems.rLoaiHang:
                    result = new rLoaiHangView();
                    break;
                case Constant.MainMenuItems.rNhanVienGiaoHang:
                    result = new rNhanVienGiaoHangView();
                    break;
                case Constant.MainMenuItems.rPhuongTien:
                    result = new rPhuongTienView();
                    break;
                case Constant.MainMenuItems.tChiPhiNhanVienGiaoHang:
                    result = new tChiPhiNhanVienGiaoHangView();
                    break;
                case Constant.MainMenuItems.tChiTietDonHang:
                    //result = new tChiTietDonHangView();
                    break;
                case Constant.MainMenuItems.tChuyenHang:
                    result = new tChuyenHangView();
                    break;
                case Constant.MainMenuItems.tDonHang:
                    result = new tDonHangView();
                    break;
                case Constant.MainMenuItems.tMatHang:
                    result = new tMatHangView();
                    break;
            }
            return result;
        }
    }
}
