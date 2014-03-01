using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl.Menu
{
    /// <summary>
    /// Interaction logic for BottomMenu.xaml
    /// </summary>
    public partial class BottomMenu : UserControl
    {
        public List<UIElement> ExtButton { get; set; }
        private bool _isFirstLoad = true;

        public BottomMenu()
        {
            ExtButton = new List<UIElement>();
            InitializeComponent();

            Loaded += BottomMenu_Loaded;
        }

        void BottomMenu_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isFirstLoad == false)
            {
                return;
            }

            foreach (var button in ExtButton)
            {
                sp.Children.Add(button);
            }

            _isFirstLoad = false;
        }
    }
}
