using PhuDinhCommon;

namespace PhuDinhData
{
    public static class ClientContext
    {
        static readonly IClientContext instance = Common.ServiceLocator.Instance.GetInstance<IClientContext>();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ClientContext()
        {
        }

        public static IClientContext Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
