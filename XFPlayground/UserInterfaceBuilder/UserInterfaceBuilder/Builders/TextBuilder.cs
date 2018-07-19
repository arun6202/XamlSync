using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bogus.DataSets;
using Xamarin.Forms;
using XamarinFormsStarterKit.UserInterfaceBuilder.Helpers;
using Preserver = XamarinFormsStarterKit.UserInterfaceBuilder.Preserver;

namespace XamarinFormsStarterKit.UserInterfaceBuilder
{
	public static class TextBuilder
	{
		static readonly Lorem Lorem = new Lorem();
		static readonly List<Color> ColorList = new List<Color>();
		static TextBuilder()
		{
			foreach (var item in typeof(Color).GetFields())
			{
				ColorList.Add((Color)item.GetValue(new Color()));

			}
		}

		public static void GenerateLoremText(Layout layout, bool suppressBackGroundColor = true)
		{
			foreach (var child in layout.Children)
			{
				if (child is Layout currentLayout)
				{
					GenerateLoremText(currentLayout, suppressBackGroundColor);
				}

				if (child.GetType().GetTypeInfo().GetDeclaredProperty("Text") != null)
				{
					Type type = child.GetType();

                    PropertyInfo prop = type.GetProperty("Text");

					if (suppressBackGroundColor)
					{
						var currentControl = (VisualElement)child;
						ComponentBuilder.PreserveUIAttributes.Text.Add(new Preserver.Text { TextValue = GenerateLoremText(prop.GetValue(child).ToString())  });

					}
                    else
					{
						ComponentBuilder.PreserveUIAttributes.Text.Add(new Preserver.Text { TextValue = GetLoremText(prop.GetValue(child).ToString(), true), TextColor = new Preserver.Color(GetColor()) });

					}
     
				}

			}

		}

		public static void LoadLoremText(Layout layout, bool apply = true, bool suppressBackGroundColor = true, bool preserveSession = false)
		{
			preserveSession = !preserveSession;

			if (!apply)
			{
				return;
			}

			foreach (var child in layout.Children)
			{
				if (child is Layout currentLayout)
				{
					LoadLoremText(currentLayout, apply, suppressBackGroundColor);
				}

				if (child.GetType().GetTypeInfo().GetDeclaredProperty("Text") != null)
				{
     
					Type type = child.GetType();

					PropertyInfo prop = type.GetProperty("Text");

					prop.SetValue(child, GetLoremText(prop.GetValue(child).ToString(),preserveSession), null);

					prop = type.GetProperty("TextColor");
					if (suppressBackGroundColor)
                    {
						prop.SetValue(child, Color.Default, null);    
						prop = type.GetProperty("BackgroundColor");
						prop.SetValue(child, Color.Default, null);    
      
                    }
                    else
					{
                    prop.SetValue(child, GetColor(preserveSession), null);    
					}
                     
				}
                
			}
		}

		private static string GetLoremText(string text, bool isRandom = true)
		{
			if (isRandom)
			{
				return GenerateLoremText(text);

			}
			else
			{
				try
				{
					if (ComponentBuilder.RestoredUIAttributes.Text.Any())
					{
						var textValue = ComponentBuilder.RestoredUIAttributes.Text[0];
 						return textValue.TextValue;
					}
				}
				finally
				{

				}
			}

			return GetLoremText(text, true);
		}

		public static string GenerateLoremText(string text)
		{         
			text = text.ToLower();
			if (text.StartsWith("w", StringComparison.CurrentCulture))
			{
				text = text.Replace("w", string.Empty);
				if (text.Length == 0)
				{
					text = Lorem.Word();
				}
				else
				{

					if (Int32.TryParse(text, out int number))
					{
						text = string.Join(" ", Lorem.Words(number));
					}
				}

			}

			if (text.StartsWith("l", StringComparison.CurrentCulture))
			{
				text = text.Replace("l", string.Empty);
				if (text.Length == 0)
				{
					text = Lorem.Lines();
				}
				else
				{
					if (Int32.TryParse(text, out int number))
					{
						text = string.Join(" ", Lorem.Lines(number));
					}
				}
			}
			if (text.StartsWith("p", StringComparison.CurrentCulture))
			{
				text = text.Replace("p", string.Empty);
				if (text.Length == 0)
				{
					text = Lorem.Paragraph();
				}
				else
				{

					if (Int32.TryParse(text, out int number))
					{
						text = string.Join(" ", Lorem.Paragraph(number));
					}
				}
			}
			if (text.StartsWith("t", StringComparison.CurrentCulture))
			{
				text = text.Replace("t", string.Empty);
				if (text.Length == 0)
				{
					text = Lorem.Text();
				}

			}
			if (text.StartsWith("sl", StringComparison.CurrentCulture))
			{
				text = text.Replace("sl", string.Empty);
				if (text.Length == 0)
				{
					text = Lorem.Slug();
				}
				else
				{

					if (Int32.TryParse(text, out int number))
					{
						text = string.Join(" ", Lorem.Slug(number));
					}
				}
			}

			return text;
		}

		public static Color GetColor(bool isRandom = true)
		{
			if (isRandom)
			{
				if (ColorList.Count == 0)
				{
					foreach (var item in typeof(Color).GetFields())
					{
						ColorList.Add((Color)item.GetValue(new Color()));

					}
				}
				var randomIndex = new Random().Next(ColorList.Count);
				var color = ColorList[randomIndex];
				ColorList.RemoveAt(randomIndex);
				return color;
			}
			else
			{
				try
				{
					if (ComponentBuilder.RestoredUIAttributes.Text.Any())
					{
						var text = ComponentBuilder.RestoredUIAttributes.Text[0];
						ComponentBuilder.RestoredUIAttributes.Text.RemoveAt(0);
						return text.TextColor.ToXamarinColor();
					}
				}
				finally
				{

				}
			}

			return GetColor(true);
		}

	}
}
