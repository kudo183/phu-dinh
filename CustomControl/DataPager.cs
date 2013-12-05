using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace CustomControl
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomControl;assembly=CustomControl"
    ///
    /// You will also need to add a project reference from the project wNextButtonhere the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:DataPager/>
    ///
    /// </summary>

    [TemplatePartAttribute(Name = DataPager.NextPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePartAttribute(Name = DataPager.PrevPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePartAttribute(Name = DataPager.FirstPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePartAttribute(Name = DataPager.LastPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePartAttribute(Name = DataPager.CurrentPageIndexElementName, Type = typeof(TextBox))]
    [TemplatePartAttribute(Name = DataPager.PageCountElementName, Type = typeof(TextBlock))]

    [TemplatePart(Name = DataPager.NextPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = DataPager.PrevPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = DataPager.FirstPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = DataPager.LastPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = DataPager.CurrentPageIndexElementName, Type = typeof(TextBox))]
    [TemplatePart(Name = DataPager.PageCountElementName, Type = typeof(TextBlock))]

    public class DataPager : Control
    {
        static DataPager()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataPager), new FrameworkPropertyMetadata(typeof(DataPager)));
        }

        private const string NextPageButtonElementName = "NextPageButton";
        public ButtonBase NextPageButtonElement { get; set; }

        private const string PrevPageButtonElementName = "PrevPageButton";
        public ButtonBase PrevPageButtonElement { get; set; }

        private const string FirstPageButtonElementName = "FirstPageButton";
        public ButtonBase FirstPageButtonElement { get; set; }

        private const string LastPageButtonElementName = "LastPageButton";
        public ButtonBase LastPageButtonElement { get; set; }

        private const string CurrentPageIndexElementName = "CurrentPageIndexTextBox";
        public TextBox CurrentPageIndexElement { get; set; }

        private const string PageCountElementName = "PageCountTextBlock";
        public TextBlock PageCountElement { get; set; }

        #region Page
        public IEnumerable<object> Page
        {
            get { return (IEnumerable<object>)GetValue(PageProperty); }
            protected set { SetValue(PageKey, value); }
        }

        // Using a DependencyProperty as the backing store for Page.  This enables animation, styling, binding, etc...
        protected static readonly DependencyPropertyKey PageKey =
            DependencyProperty.RegisterReadOnly("Page", typeof(IEnumerable<object>), typeof(DataPager), new PropertyMetadata(null));

        public static readonly DependencyProperty PageProperty = PageKey.DependencyProperty;
        #endregion

        #region PageSize
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(DataPager), new PropertyMetadata(0, PageSizePropertyChangedCallback));

        private static void PageSizePropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var pager = dependencyObject as DataPager;
            if (pager.Source == null)
            {
                return;
            }

            pager.Reset();
            pager.RefreshPage();
        }

        #endregion

        #region PageCount
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            protected set { SetValue(PageCountKey, value); }
        }

        // Using a DependencyProperty as the backing store for PageCount.  This enables animation, styling, binding, etc...
        protected static readonly DependencyPropertyKey PageCountKey =
            DependencyProperty.RegisterReadOnly("PageCount", typeof(int), typeof(DataPager), new PropertyMetadata(0));

        public static readonly DependencyProperty PageCountProperty = PageCountKey.DependencyProperty;
        #endregion

        #region ItemCount
        public int ItemCount
        {
            get { return (int)GetValue(ItemCountProperty); }
            protected set { SetValue(ItemCountKey, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCount.  This enables animation, styling, binding, etc...
        protected static readonly DependencyPropertyKey ItemCountKey =
            DependencyProperty.RegisterReadOnly("ItemCount", typeof(int), typeof(DataPager), new PropertyMetadata(0));

        public static readonly DependencyProperty ItemCountProperty = ItemCountKey.DependencyProperty;
        #endregion

        #region CurrentPageIndex
        public int CurrentPageIndex
        {
            get { return (int)GetValue(CurrentPageIndexProperty); }
            set { SetValue(CurrentPageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageIndexProperty =
            DependencyProperty.Register("CurrentPageIndex", typeof(int), typeof(DataPager),
            new PropertyMetadata(0, CurrentPageIndexPropertyChangedCallback));

        private static void CurrentPageIndexPropertyChangedCallback(DependencyObject dependencyObject
            , DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var pager = dependencyObject as DataPager;
            pager.RefreshPage();
        }
        #endregion

        #region Source
        public IQueryable<object> Source
        {
            get { return ((IQueryable<object>)GetValue(SourceProperty)); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(IQueryable<object>), typeof(DataPager), new PropertyMetadata(null, SourcePropertyChangedCallback));

        private static void SourcePropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var pager = dependencyObject as DataPager;
            if (pager.Source == null)
            {
                return;
            }

            pager.Reset();
            pager.RefreshPage();
        }
        #endregion

        public DataPager()
        {
            PageSize = 30;
            DataContext = this;
        }

        public override void OnApplyTemplate()
        {
            this.NextPageButtonElement = this.GetTemplateChild(DataPager.NextPageButtonElementName) as Button;
            this.ThrowIfMissing(this.NextPageButtonElement, DataPager.NextPageButtonElementName);
            this.NextPageButtonElement.Click += NextPageButtonElement_Click;

            this.PrevPageButtonElement = this.GetTemplateChild(DataPager.PrevPageButtonElementName) as Button;
            this.ThrowIfMissing(this.PrevPageButtonElement, DataPager.PrevPageButtonElementName);
            this.PrevPageButtonElement.Click += PrevPageButtonElement_Click;

            this.FirstPageButtonElement = this.GetTemplateChild(DataPager.FirstPageButtonElementName) as Button;
            this.ThrowIfMissing(this.FirstPageButtonElement, DataPager.FirstPageButtonElementName);
            this.FirstPageButtonElement.Click += FirstPageButtonElement_Click;

            this.LastPageButtonElement = this.GetTemplateChild(DataPager.LastPageButtonElementName) as Button;
            this.ThrowIfMissing(this.LastPageButtonElement, DataPager.LastPageButtonElementName);
            this.LastPageButtonElement.Click += LastPageButtonElement_Click;

            this.CurrentPageIndexElement = this.GetTemplateChild(DataPager.CurrentPageIndexElementName) as TextBox;
            this.ThrowIfMissing(this.CurrentPageIndexElement, DataPager.CurrentPageIndexElementName);
            this.CurrentPageIndexElement.SetBinding(TextBox.TextProperty, new Binding("CurrentPageIndex") { Mode = BindingMode.TwoWay });

            this.PageCountElement = this.GetTemplateChild(DataPager.PageCountElementName) as TextBlock;
            this.ThrowIfMissing(this.PageCountElement, DataPager.PageCountElementName);
            this.PageCountElement.SetBinding(TextBlock.TextProperty, new Binding("PageCount") { Mode = BindingMode.OneWay });

            base.OnApplyTemplate();
        }

        void NextPageButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPageIndex == PageCount)
            {
                return;
            }

            CurrentPageIndex++;
        }

        void PrevPageButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPageIndex == 1)
            {
                return;
            }

            CurrentPageIndex--;
        }

        void FirstPageButtonElement_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageIndex = 1;
        }

        void LastPageButtonElement_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageIndex = PageCount;
        }        

        private void RefreshPage()
        {
            if (Source == null)
            {
                Page = null;
                return;
            }

            var skippedItem = PageSize * (CurrentPageIndex - 1);

            var takeItem = ItemCount - skippedItem;
            if (takeItem > PageSize)
                takeItem = PageSize;

            Page = Source.Skip(skippedItem).Take(takeItem).ToList();
        }

        private void Reset()
        {
            ItemCount = Source.Count();
            PageCount = (ItemCount + PageSize - 1) / PageSize;
            CurrentPageIndex = 1;
        }

        private void ThrowIfMissing(DependencyObject element, string name)
        {
            if (element == null)
                throw new Exception(string.Format("Missing required element '{0}'", name));
        }
    }
}
