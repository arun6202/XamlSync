using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace XamarinFormsStarterKit.UserInterfaceBuilder.Models
{
    [Serializable]
    [Preserve]
    [JsonObject]
    public class XamlPayload : IComparable
    {
        [Preserve]
        [JsonConstructor]
        public XamlPayload()
        {
        }
        [Preserve]
        [JsonProperty("XAML")]
        public string XAML
        {
            get;
            set;
        }

        [Preserve]
        [JsonProperty("PreserveXML")]
        public string PreserveXML
        {
            get;
            set;
        }
 
        public int CompareTo(object obj)
        {
            XamlPayload Temp = (XamlPayload)obj;

            return this.XAML.Equals(Temp.XAML) & this.PreserveXML.Equals(Temp.PreserveXML) ? 0 : 1;
        }
    }
}
