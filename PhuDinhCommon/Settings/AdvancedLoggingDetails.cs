using System;
using System.Xml.Serialization;

namespace PhuDinhCommon
{
    [Serializable]
    public class AdvancedLoggingDetails
    {
        [XmlAttribute]
        public severity_type SeverityType
        { get; set; }

        [XmlIgnore]
        public string Severity
        {
            get
            {
                return SeverityType.ToStr();
            }
        }

        [XmlAttribute]
        public bool LogToGrid { get; set; }

        [XmlAttribute]
        public bool LogToFile { get; set; }

        [XmlAttribute]
        public int ForegroundArgb { get; set; }

        [XmlAttribute]
        public int BackgroundArgb { get; set; }
    }
}
