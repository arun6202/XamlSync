using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XamarinFormsStarterKit.UserInterfaceBuilder.Preserver
{
	[Serializable]
	[DataContract]
	public class Preserve
	{
		public Preserve()
		{
            Layout = new List<Color>();
            Text = new List<Text>();
            Image = new List<Image>();

		}
		[DataMember]
		public List<Color> Layout
		{
			get;
			set;
		}
		[DataMember]
		public List<Text> Text
		{
			get;
			set;
		}

		[DataMember]
		public List<Image> Image
		{
			get;
			set;
		}
	}
}
