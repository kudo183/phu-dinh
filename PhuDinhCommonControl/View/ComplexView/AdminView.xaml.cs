using System.Collections.Generic;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        Dictionary<string, BaseView> _dictView = new Dictionary<string, BaseView>();
        public AdminView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as TreeViewItem;
            brdContent.Child = GetView(item.Header.ToString());
        }

        private BaseView GetView(string name)
        {
            if (_dictView.ContainsKey(name))
            {
                return _dictView[name];
            }

            _dictView.Add(name, ViewFactory.CreateView(name));

            return _dictView[name];
        }
    }
}
