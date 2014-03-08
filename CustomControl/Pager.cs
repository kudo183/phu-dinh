using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

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
    ///     <MyNamespace:Pager/>
    ///
    /// </summary>

    [TemplatePartAttribute(Name = Pager.NextPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePartAttribute(Name = Pager.PrevPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePartAttribute(Name = Pager.FirstPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePartAttribute(Name = Pager.LastPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePartAttribute(Name = Pager.CurrentPageIndexElementName, Type = typeof(TextBox))]
    [TemplatePartAttribute(Name = Pager.PageCountElementName, Type = typeof(TextBlock))]
    [TemplatePartAttribute(Name = Pager.ItemCountElementName, Type = typeof(TextBlock))]

    [TemplatePart(Name = Pager.NextPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = Pager.PrevPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = Pager.FirstPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = Pager.LastPageButtonElementName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = Pager.CurrentPageIndexElementName, Type = typeof(TextBox))]
    [TemplatePart(Name = Pager.PageCountElementName, Type = typeof(TextBlock))]
    [TemplatePart(Name = Pager.ItemCountElementName, Type = typeof(TextBlock))]

    public class Pager : Control
    {
        static Pager()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pager), new FrameworkPropertyMetadata(typeof(Pager)));
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

        private const string ItemCountElementName = "ItemCountTextBlock";
        public TextBlock ItemCountElement { get; set; }

        #region PageSize
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pager), new PropertyMetadata(30, PageSizePropertyChangedCallback));

        private static void PageSizePropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var pager = dependencyObject as Pager;

            pager.Reset();
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
            DependencyProperty.RegisterReadOnly("PageCount", typeof(int), typeof(Pager), new PropertyMetadata(0));

        public static readonly DependencyProperty PageCountProperty = PageCountKey.DependencyProperty;
        #endregion

        #region ItemCount
        public int ItemCount
        {
            get { return (int)GetValue(ItemCountProperty); }
            set { SetValue(ItemCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCountProperty =
            DependencyProperty.Register("ItemCount", typeof(int), typeof(Pager), new PropertyMetadata(0, ItemCountPropertyChangedCallback));

        private static void ItemCountPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var pager = dependencyObject as Pager;

            pager.Reset();
        }

        #endregion

        #region CurrentPageIndex
        public int CurrentPageIndex
        {
            get { return (int)GetValue(CurrentPageIndexProperty); }
            set { SetValue(CurrentPageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageIndexProperty =
            DependencyProperty.Register("CurrentPageIndex", typeof(int), typeof(Pager),
            new PropertyMetadata(1));
        #endregion

        public event EventHandler PageChanged;

        public override void OnApplyTemplate()
        {
            this.NextPageButtonElement = this.GetTemplateChild(Pager.NextPageButtonElementName) as Button;
            this.ThrowIfMissing(this.NextPageButtonElement, Pager.NextPageButtonElementName);
            this.NextPageButtonElement.Click += NextPageButtonElement_Click;

            this.PrevPageButtonElement = this.GetTemplateChild(Pager.PrevPageButtonElementName) as Button;
            this.ThrowIfMissing(this.PrevPageButtonElement, Pager.PrevPageButtonElementName);
            this.PrevPageButtonElement.Click += PrevPageButtonElement_Click;

            this.FirstPageButtonElement = this.GetTemplateChild(Pager.FirstPageButtonElementName) as Button;
            this.ThrowIfMissing(this.FirstPageButtonElement, Pager.FirstPageButtonElementName);
            this.FirstPageButtonElement.Click += FirstPageButtonElement_Click;

            this.LastPageButtonElement = this.GetTemplateChild(Pager.LastPageButtonElementName) as Button;
            this.ThrowIfMissing(this.LastPageButtonElement, Pager.LastPageButtonElementName);
            this.LastPageButtonElement.Click += LastPageButtonElement_Click;

            this.CurrentPageIndexElement = this.GetTemplateChild(Pager.CurrentPageIndexElementName) as TextBox;
            this.ThrowIfMissing(this.CurrentPageIndexElement, Pager.CurrentPageIndexElementName);
            this.CurrentPageIndexElement.SetBinding(TextBox.TextProperty, new Binding("CurrentPageIndex") { Source = this, Mode = BindingMode.OneWay });
            this.CurrentPageIndexElement.KeyDown += CurrentPageIndexElement_KeyDown;

            this.PageCountElement = this.GetTemplateChild(Pager.PageCountElementName) as TextBlock;
            this.ThrowIfMissing(this.PageCountElement, Pager.PageCountElementName);
            this.PageCountElement.SetBinding(TextBlock.TextProperty, new Binding("PageCount") { Source = this, Mode = BindingMode.OneWay });

            this.ItemCountElement = this.GetTemplateChild(Pager.ItemCountElementName) as TextBlock;
            this.ThrowIfMissing(this.ItemCountElement, Pager.ItemCountElementName);
            this.ItemCountElement.SetBinding(TextBlock.TextProperty, new Binding("ItemCount") { Source = this, Mode = BindingMode.OneWay });

            base.OnApplyTemplate();
        }

        void CurrentPageIndexElement_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var index = int.Parse(CurrentPageIndexElement.Text);
                if (index < 1)
                {
                    CurrentPageIndex = 1;
                }
                else if (index > PageCount)
                {
                    CurrentPageIndex = PageCount;
                }
                else
                {
                    CurrentPageIndex = index;
                }
            }
        }

        void NextPageButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPageIndex >= PageCount)
            {
                return;
            }

            CurrentPageIndex++;
        }

        void PrevPageButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPageIndex <= 1)
            {
                return;
            }

            CurrentPageIndex--;
        }

        void FirstPageButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (PageCount > 0)
            {
                CurrentPageIndex = 1;
            }
        }

        void LastPageButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                CurrentPageIndex = 1;
            }
            else
            {
                CurrentPageIndex = PageCount;
            }
        }

        private void Reset()
        {
            PageCount = (ItemCount + PageSize - 1) / PageSize;

            if (PageCount == 0)
            {
                CurrentPageIndex = 1;
            }
            else if (CurrentPageIndex > PageCount)
            {
                CurrentPageIndex = PageCount;
            }
        }

        private void ThrowIfMissing(DependencyObject element, string name)
        {
            if (element == null)
                throw new Exception(string.Format("Missing required element '{0}'", name));
        }
    }
}
