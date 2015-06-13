using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PhuDinhCommon.PrintTemplate
{
    /// <summary>
    /// Interaction logic for PrintTemplateDonHang.xaml
    /// </summary>
    public partial class PrintTemplateDonHang : UserControl
    {
        public PrintTemplateDonHang()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return title.Text; }
            set { title.Text = value; }
        }

        private ReadOnlyCollection<string> _content;
        public ReadOnlyCollection<string> Content
        {
            get { return _content; }
            set
            {
                _content = value;
                content.Inlines.Clear();
                foreach (var s in value)
                {
                    content.Inlines.Add(s);
                    content.Inlines.Add(new LineBreak());
                }
            }
        }

        public IDocumentPaginatorSource Document
        {
            get { return document; }
        }
    }
}
