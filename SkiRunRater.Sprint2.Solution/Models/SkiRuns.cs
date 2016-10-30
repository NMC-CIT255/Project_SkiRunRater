using System.Collections.Generic;
using System.Xml.Serialization;

namespace SkiRunRater
{
    [XmlRoot("SkiRuns")]
    public class SkiRuns
    {
        
        [XmlElement("SkiRun")]
        public List<SkiRun> skiRuns;

    }
}
