using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TestWpfApplication
{
    /// <summary>
    /// Interaction logic for Pager.xaml
    /// </summary>
    public partial class Pager : UserControl
    {
        public IEnumerable<object> Page
        {
            get { return (IEnumerable<object>)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Page.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(IEnumerable<object>), typeof(Pager), new PropertyMetadata(null));


        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pager), new PropertyMetadata(0));


        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(Pager), new PropertyMetadata(0));


        public int CurrentPageIndex
        {
            get { return (int)GetValue(CurrentPageIndexProperty); }
            set { SetValue(CurrentPageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageIndexProperty =
            DependencyProperty.Register("CurrentPageIndex", typeof(int), typeof(Pager),
            new PropertyMetadata(0, CurrentPageIndexPropertyChangedCallback));

        private static void CurrentPageIndexPropertyChangedCallback(DependencyObject dependencyObject
            , DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var pager = dependencyObject as Pager;
            pager.ProcessPage();
        }


        public IQueryable<object> Source
        {
            get { return ((IQueryable<object>)GetValue(SourceProperty)); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(IQueryable<object>), typeof(Pager), new PropertyMetadata(null, SourcePropertyChangedCallback));

        private static void SourcePropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var pager = dependencyObject as Pager;
            pager.PageCount = pager.Source.Count() / pager.PageSize;
            pager.CurrentPageIndex = 0;
            pager.ProcessPage();
        }

        public Pager()
        {
            InitializeComponent();
            PageSize = 5;
            DataContext = this;
        }

        private void ProcessPage()
        {
            Page = Source == null ? null : Source.Skip(PageSize * CurrentPageIndex).Take(PageSize).ToList();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageIndex++;
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageIndex--;
        }
    }
}
