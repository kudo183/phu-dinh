namespace PhuDinhCommon
{
    public static class Utils
    {
        public static string ToStr(this severity_type severityType)
        {
            switch (severityType)
            {
                case severity_type.st_all:
                    return "All";
                case severity_type.st_verbose:
                    return "Verbose";
                case severity_type.st_default:
                    return "Default";
                case severity_type.st_debug:
                    return "Debug";
                case severity_type.st_extra:
                    return "Extra";
                case severity_type.st_warning:
                    return "Warning";
                case severity_type.st_error:
                    return "Error";
                case severity_type.st_info:
                    return "Info";
                case severity_type.st_undefined:
                    return "Undefined";
                default:
                    return "Unknown";
            }
        }

        public static event_type EventTypeFromShortStr(string str)
        {
            switch (str)
            {
                case "Act":
                    return event_type.et_Accounts;
                case "Adm":
                    return event_type.et_Administrative;
                case "Aux":
                    return event_type.et_AutoX;
                case "IM ":
                    return event_type.et_Inside_Market;
                case "Int":
                    return event_type.et_Internal;
                case "Mrg":
                    return event_type.et_MarginCall;
                case "MD ":
                    return event_type.et_Market_Data_Control;
                case "MDp":
                    return event_type.et_Market_Depth;
                case "Ord":
                    return event_type.et_Orders;
                case "Pos":
                    return event_type.et_Positions;
                case "RM ":
                    return event_type.et_RiskManager;
                case "RT ":
                    return event_type.et_RoboTrader;
                case "RSS":
                    return event_type.et_RSS_Feed;
                case "Trd":
                    return event_type.et_Trades;
                case "Alt":
                    return event_type.et_Alert;
                case "Prf":
                    return event_type.et_Performance;
                case "Und":
                    return event_type.et_Undefined;
                default:
                    return event_type.et_Undefined;
            }
        }

        public static string ToShortStr(this event_type type)
        {
            switch (type)
            {
                case event_type.et_Accounts:
                    return "Act";
                case event_type.et_Administrative:
                    return "Adm";
                case event_type.et_Inside_Market:
                    return "IM ";
                case event_type.et_Internal:
                    return "Int";
                case event_type.et_MarginCall:
                    return "Mrg";
                case event_type.et_Market_Data_Control:
                    return "MD ";
                case event_type.et_Market_Depth:
                    return "MDp";
                case event_type.et_Orders:
                    return "Ord";
                case event_type.et_Positions:
                    return "Pos";
                case event_type.et_RiskManager:
                    return "RM ";
                case event_type.et_RoboTrader:
                    return "RT ";
                case event_type.et_RSS_Feed:
                    return "RSS";
                case event_type.et_Trades:
                    return "Trd";
                case event_type.et_Alert:
                    return "Alt";
                case event_type.et_Performance:
                    return "Prf";
                case event_type.et_Undefined:
                    return "Und";
                default:
                    return "Und";
            }
        }

        public static string ToStr(this event_type type)
        {
            switch (type)
            {
                case event_type.et_Accounts:
                    return "Accounts";
                case event_type.et_Administrative:
                    return "Administrative";
                case event_type.et_Inside_Market:
                    return "Inside Market";
                case event_type.et_Internal:
                    return "Internal";
                case event_type.et_MarginCall:
                    return "Margin Call";
                case event_type.et_Market_Data_Control:
                    return "Market Data";
                case event_type.et_Market_Depth:
                    return "Market Depth";
                case event_type.et_Orders:
                    return "Orders";
                case event_type.et_Positions:
                    return "Positions";
                case event_type.et_RiskManager:
                    return "Risk Manager";
                case event_type.et_RoboTrader:
                    return "NEO Robot";
                case event_type.et_RSS_Feed:
                    return "RSS Feed";
                case event_type.et_Trades:
                    return "Trades";
                case event_type.et_Alert:
                    return "Alert";
                case event_type.et_Performance:
                    return "Performance";
                case event_type.et_Undefined:
                default:
                    return "Undefined";
            }
        }
    }
}
