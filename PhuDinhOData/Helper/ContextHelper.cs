using System.Reflection;
using PhuDinhData;
using log4net;

namespace PhuDinhOData.Helper
{
    public class ContextHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static PhuDinhEntities CreateContext()
        {
            _logger.Info("Create Context");
            var _context = new PhuDinhEntities("PhuDinhEntities");

            _context.Database.Log = _logger.Info;

            return _context;
        }
    }
}