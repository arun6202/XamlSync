using Newtonsoft.Json;
using System;

namespace XamlSync.Models
{
    [Serializable]

    [JsonObject]
    public class XamlPayload : IComparable
    {

        [JsonConstructor]
        public XamlPayload()
        {
        }

        [JsonProperty("XAML")]
        public string XAML
        {
            get;
            set;
        }


        [JsonProperty("PreserveXML")]
        public string PreserveXML
        {
            get;
            set;
        }

        public int CompareTo(object obj)
        {
            XamlPayload Temp = (XamlPayload)obj;

            return XAML.Equals(Temp.XAML) & PreserveXML.Equals(Temp.PreserveXML) ? 0 : 1;
        }
    }
}