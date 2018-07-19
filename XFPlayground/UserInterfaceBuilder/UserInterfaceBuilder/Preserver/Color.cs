using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using XamarinFormsStarterKit.UserInterfaceBuilder.Helpers;

namespace XamarinFormsStarterKit.UserInterfaceBuilder.Preserver
{
	public class Color
	{
		private const string WhiteColor = "#ffffff";

		public Color(Xamarin.Forms.Color color)
		{
			R = color.R;
			G = color.G;
			B = color.B;
			Hue = color.Hue;
			Saturation = color.Saturation;
			Luminosity = color.Luminosity;
			Hex = color.ToHex();
		}

		public Color()
		{

		}
		[XmlIgnore]
		public double R
		{
			get;
			set;

		}

		public string Hex
		{
			get;
			set;

		} = WhiteColor;

		[XmlIgnore]
		public double G
		{
			get;
			set;
		}
		[XmlIgnore]
		public double B
		{
			get;
			set;
		}
		[XmlIgnore]
		public double Hue
		{
			get;
			set;
		}
		[XmlIgnore]
		public double Saturation
		{
			get;
			set;
		}
		[XmlIgnore]
		public double Luminosity
		{
			get;
			set;
		}

	}
}
