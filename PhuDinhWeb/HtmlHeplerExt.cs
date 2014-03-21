using System.Web;
using System.Web.Mvc;

namespace PhuDinhWeb
{
    public static class HtmlHeplerExt
    {
        public static IHtmlString FormatSoLuongTonKho(this HtmlHelper helper, PhuDinhData.tTonKho t)
        {
            var result = "<font color=\"{0}\">{1}</font> ";
            if (t.CanhBao == 0)
            {
                result = t.SoLuong.ToString();
            }
            else if (t.CanhBao < 0)
            {
                result = string.Format(result, "red", t.SoLuong);
            }
            else if (t.CanhBao > 0)
            {
                result = string.Format(result, "blue", t.SoLuong);
            }

            return new HtmlString(result);
        }
    }
}