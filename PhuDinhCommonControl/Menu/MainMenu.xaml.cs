using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;

namespace PhuDinhCommonControl.Menu
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        // Create a custom routed event by first registering a RoutedEventID 
        // This event uses the bubbling routing strategy 
        public static readonly RoutedEvent MenuItemClickEvent = EventManager.RegisterRoutedEvent(
            "MenuItemClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MainMenu));

        // Provide CLR accessors for the event 
        public event RoutedEventHandler MenuItemClick
        {
            add { AddHandler(MenuItemClickEvent, value); }
            remove { RemoveHandler(MenuItemClickEvent, value); }
        }

        // This method raises the Tap event 
        void RaiseMenuItemClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(MainMenu.MenuItemClickEvent);
            RaiseEvent(newEventArgs);
        }
        // For demonstration purposes we raise the event when the MyButtonSimple is clicked 
        
        public Constant.MainMenuItems SelectedItem { get; set; }

        public MainMenu()
        {
            InitializeComponent();
        }

        private void ProcessButtonClick(ToggleButton currentButton)
        {
            if (currentButton == null)
            {
                return;
            }

            UpdateSelectedItem(currentButton);
            UpdateToggleButton(currentButton);

            RaiseMenuItemClickEvent();
        }

        private void UpdateSelectedItem(ToggleButton currentButton)
        {
            switch (currentButton.Name)
            {
                case "rBaiXe":
                    SelectedItem = Constant.MainMenuItems.rBaiXe;
                    break;
                case "rChanh":
                    SelectedItem = Constant.MainMenuItems.rChanh;
                    break;
                case "rKhachHang":
                    SelectedItem = Constant.MainMenuItems.rKhachHang;
                    break;
                case "rLoaiChiPhi":
                    SelectedItem = Constant.MainMenuItems.rLoaiChiPhi;
                    break;
                case "rLoaiHang":
                    SelectedItem = Constant.MainMenuItems.rLoaiHang;
                    break;
                case "rNhanVienGiaoHang":
                    SelectedItem = Constant.MainMenuItems.rNhanVienGiaoHang;
                    break;
                case "rNuoc":
                    SelectedItem = Constant.MainMenuItems.rNuoc;
                    break;
                case "rPhuongTien":
                    SelectedItem = Constant.MainMenuItems.rPhuongTien;
                    break;
                case "tChiPhiNhanVienGiaoHang":
                    SelectedItem = Constant.MainMenuItems.tChiPhiNhanVienGiaoHang;
                    break;
                case "tChiTietChuyenHangDonHang":
                    SelectedItem = Constant.MainMenuItems.tChiTietChuyenHangDonHang;
                    break;
                case "tChiTietDonHang":
                    SelectedItem = Constant.MainMenuItems.tChiTietDonHang;
                    break;
                case "tChuyenHang":
                    SelectedItem = Constant.MainMenuItems.tChuyenHang;
                    break;
                case "tChuyenHangDonHang":
                    SelectedItem = Constant.MainMenuItems.tChuyenHangDonHang;
                    break;
                case "tDonHang":
                    SelectedItem = Constant.MainMenuItems.tDonHang;
                    break;
                case "tMatHang":
                    SelectedItem = Constant.MainMenuItems.tMatHang;
                    break;
                default:
                    break;
            }
        }

        private void UpdateToggleButton(ToggleButton currentButton)
        {
            foreach (ToggleButton btn in this._spMenu.Children)
            {
                btn.IsChecked = btn == currentButton;
            }
        }

        private void _spMenu_Click(object sender, RoutedEventArgs e)
        {
            ProcessButtonClick(e.OriginalSource as ToggleButton);
        }
    }
}
