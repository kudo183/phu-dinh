using System.Windows.Controls;
using System.Windows.Documents;

namespace Common
{
    public static class PrintService
    {
        public static void Print(IDocumentPaginatorSource document)
        {
            var diag = new PrintDialog();

            diag.PrintDocument(document.DocumentPaginator, "Caption");
        }
    }
}
