using System;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.IO;

namespace XamarinFormsStarterKit.UserInterfaceBuilder.Helpers
{
	public static class Extensions
	{
		public static Color ToXamarinColor(this Preserver.Color color)
		{
			return Color.FromHex(color.Hex);
		}

		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}
		public static string ToHex(this Color color)
		{
			int red = (int)(color.R * 255);
			int green = (int)(color.G * 255);
			int blue = (int)(color.B * 255);
			string hex = red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
			return hex;
		}

		public static Stream GenerateStreamFromString(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

	}
}
