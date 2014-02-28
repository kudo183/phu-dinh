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
        public BottomMenu()
        {
            ExtButton = new List<UIElement>();
            InitializeComponent();

            Loaded += BottomMenu_Loaded;
        }

        void BottomMenu_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var button in ExtButton)
            {
                sp.Children.Add(button);
            }
        }
    }
}
