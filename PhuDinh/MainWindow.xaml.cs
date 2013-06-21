using System.Windows;
using PhuDinh.View;

namespace PhuDinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ChuyenHangView _chuyenHangView = new ChuyenHangView();
        private readonly DonHangView _donHangView = new DonHangView();
        private readonly BaiXeView _baiXeView = new BaiXeView();
        private readonly MatHangView _matHangView = new MatHangView();
        private readonly NhanVienView _nhanVienView = new NhanVienView();
        private readonly ChiPhiView _chiPhiView = new ChiPhiView();
        private readonly KhachHangView _khachHangView = new KhachHangView();
        private readonly AdminView _adminView = new AdminView();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnChuyenHang_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = _chuyenHangView;
        }

        private void btnDonHang_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = _donHangView;
        }

        private void btnBaiXe_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = _baiXeView;
        }

        private void btnMatHang_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = _matHangView;
        }

        private void btnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = _nhanVienView;
        }

        private void btnChiPhi_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = _chiPhiView;
        }

        private void btnKhachHang_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = _khachHangView;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            _brdMainContent.Child = _adminView;
        }
    }
}
