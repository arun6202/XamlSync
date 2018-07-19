using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Acr.UserDialogs;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using XamarinFormsStarterKit.UserInterfaceBuilder.Helpers;

namespace XamarinFormsStarterKit.UserInterfaceBuilder
{
	public static class LayoutBuilder
	{

		static readonly List<Color> ColorList = new List<Color>();

		static LayoutBuilder()
		{
			foreach (var item in typeof(Color).GetFields())
			{
				ColorList.Add((Color)item.GetValue(new Color()));

			}

		}

		public static void GenerateLayoutColors(Layout layout, bool suppressBackGroundColor = true)
		{
			if (!suppressBackGroundColor)
			{
				ComponentBuilder.PreserveUIAttributes.Layout.Add(new Preserver.Color(GetColor()));

			}
			foreach (var child in layout.Children)
			{
				if (child is BoxView boxView)
                {
					ComponentBuilder.PreserveUIAttributes.Layout.Add(new Preserver.Color(GetColor()));
                }
				
				if (child is Layout currentLayout)
				{
					GenerateLayoutColors(currentLayout, suppressBackGroundColor);
				}
			}
		}

		public static void ColorizeLayout(Layout layout, bool apply = true, bool suppressBackGroundColor = true, bool preserveSession = false)
		{
			preserveSession = !preserveSession;
			if (!apply)
			{
				return;
			}

			layout.BackgroundColor = suppressBackGroundColor ? Color.Default : GetColor(preserveSession);

			foreach (var child in layout.Children)
			{
				if (child is BoxView boxView)
                {
					boxView.BackgroundColor = suppressBackGroundColor ? Color.Default : GetColor(preserveSession);
                }
				
				if (child is Layout currentLayout)
				{
					ColorizeLayout(currentLayout, apply, suppressBackGroundColor, !preserveSession);
				}     
			}
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
					if (ComponentBuilder.RestoredUIAttributes.Layout.Any())
					{
						var color = ComponentBuilder.RestoredUIAttributes.Layout[0];
						ComponentBuilder.RestoredUIAttributes.Layout.RemoveAt(0);
						return color.ToXamarinColor();
					}
				}
				finally
				{

				}
			}

			return GetColor(true);
		}
        
		#region  HookTapGestureRecognizer

		public static void CompressLayoutAsHeadless(Layout layout, bool apply = true)
		{
			if (!apply)
			{
				return;
			}

			foreach (var child in layout.Children)
			{
				if (child is Layout currentLayout)
				{
					CompressLayoutAsHeadless(currentLayout);
				}

				if (child is Layout currentLayoutHeadless)
				{
					CompressedLayout.SetIsHeadless(child, apply);

				}

			}
		}

		internal static void HookTapGestureRecognizer(Layout layout, bool apply)
		{
			if (!apply)
			{
				return;
			}
			foreach (var child in layout.Children)
			{
				if (child is Layout currentLayout)
				{
					HookTapGestureRecognizer(currentLayout, apply);
				}

				EnableToast(child);

			}
		}

		private static void EnableToast(Element child)
		{
			EnableSingleTapToast(child);
			EnableDoubleTapToast(child);
		}

		private static void EnableDoubleTapToast(Element element)
		{
			if (element is VisualElement visualElement)
			{
				if (element is View view)
				{
					var tapGestureRecognizer = new TapGestureRecognizer
					{
						NumberOfTapsRequired = 2
					};
					tapGestureRecognizer.Tapped += (s, e) =>
					{
						Rects.Clear();
						RectsStack.Clear();
						ExtractRect = true;
						ExtractCompleteRect(view);

						foreach (var item in RectsStack)
						{
							Rects.AppendLine(item.ToString());
						}

						UserDialogs.Instance.Toast($"{Rects.ToString()}", TimeSpan.FromSeconds(10));

					};
					view.GestureRecognizers.Add(tapGestureRecognizer);
				}
			}

		}

		static StringBuilder Rects = new StringBuilder();
		static Stack RectsStack = new Stack();


		static bool ExtractRect = true;

		private static void ExtractCompleteRect(Element element)
		{
			if (element is VisualElement visualElement)
			{
				RectsStack.Push(visualElement.Bounds.ToSKRect().ToString());

			}
			ExtractRect = !(element.Parent is ContentPage);
			while (ExtractRect)
			{
				ExtractCompleteRect(element.Parent);
			}

		}


		private static void EnableSingleTapToast(Element element)
		{
			if (element is VisualElement visualElement)
			{
				if (element is View view)
				{
					var tapGestureRecognizer = new TapGestureRecognizer();
					view.GestureRecognizers.Clear();
					tapGestureRecognizer.Tapped += (s, e) =>
					{
						UserDialogs.Instance.Toast($"{view.Bounds.ToSKRect().ToString()}", TimeSpan.FromSeconds(5));

					};
					view.GestureRecognizers.Add(tapGestureRecognizer);
				}

			}

		}
		#endregion
	}
}
