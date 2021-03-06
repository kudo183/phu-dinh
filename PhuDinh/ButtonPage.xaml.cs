﻿using PhuDinhCommonControl;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinh
{
    /// <summary>
    /// Interaction logic for ButtonPage.xaml
    /// </summary>
    public partial class ButtonPage : UserControl
    {
        public ButtonPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (e.OriginalSource as Button);
            if (button == null)
            {
                return;
            }

            var w = new WindowEx
            {
                Title = button.Content.ToString(),
                WindowState = WindowState.Maximized,
                brdContent = { Child = ViewFactory.CreateView(button.Tag.ToString()) }
            };
            w.Show();
        }
    }
}
