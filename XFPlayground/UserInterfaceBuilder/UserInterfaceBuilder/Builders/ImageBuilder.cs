using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using System.Linq;
using XamarinFormsStarterKit.UserInterfaceBuilder.UIElements;
using SkiaSharp.Views.Forms;
using System.Security;

namespace XamarinFormsStarterKit.UserInterfaceBuilder
{
	public static class ImageBuilder
	{
		private const string PNGImageType = ".PNG.Patterns.";
 		private const double DefaultWidth = 40;
		private const double DefaultHeight = 40;
		private const string Small = "s";
		private const string Medium = "m";
		private const string Large = "l";
		private const string ExtraLarge = "xl";
		private const string DoubleExtraLarge = "xxl";
		private const string TripleExtraLarge = "xxxl";
		private const double SmallSize = 100;
		private const double MediumSize = 140;
		private const double LargeSize = 180;
		private const double ExtraLargeSize = 220;
		private const double DoubleExtraLargeSize = 260;
		private const double TripleExtraLargeSize = 300;
		static readonly List<string> PNGImageList = new List<string>();
 

		static ImageBuilder()
		{
			PNGImageList.AddRange(LoadImagesResourcesList(PNGImageType));
 
		}

		private static string[] LoadImagesResourcesList(string type)
		{
			var resourceNames = Assembly.GetCallingAssembly().GetManifestResourceNames();

			var resources = resourceNames
				.Where(x => x.Contains(type))
				.ToArray();

			return resources;

		}

		public static void GenerateImage(Layout layout, bool suppressBackGroundColor = true)
		{
			foreach (var child in layout.Children)
			{
				if (child is Layout currentLayout)
				{
					GenerateImage(currentLayout, suppressBackGroundColor);
				}

				var currentControl = (VisualElement)child;
				double height = DefaultWidth, width = DefaultHeight;

				switch (child)
				{
					case Image _:
					case SVGImage _:
						{
							var imageAttributtes = new Preserver.Image();
       
							FinalizeDimensions(child, out height, out width);

							if (child is Image img)
							{
								imageAttributtes.Source = GetPNGImage(true).Source;

							}
							if (child is SVGImage svgImage)
							{
								imageAttributtes = GetSVGImage();
                                
							}
							imageAttributtes.Height = height;
							imageAttributtes.Width = width;

							ComponentBuilder.PreserveUIAttributes.Image.Add(imageAttributtes);
							break;
						}
				}
			}

		}

		public static void LoadImage(Layout layout, bool apply = true, bool suppressBackGroundColor = true, bool preserveSession = false)
		{

			if (!apply)
			{
				return;
			}

			foreach (var child in layout.Children)
			{
				if (child is Layout currentLayout)
				{
					LoadImage(currentLayout, apply, suppressBackGroundColor);
				}

				var currentControl = (VisualElement)child;

				switch (child)
				{
					case Image _:
					case SVGImage _:
						{
							if (suppressBackGroundColor)
							{
								currentControl.BackgroundColor = Color.Default;
							}
							FinalizeDimensions(child, out double height, out double width);
							if (child is Image img)
							{

								var pngImageAttributtes = GetPNGImage(!preserveSession);

								img.Source = ImageSource.FromResource(pngImageAttributtes.Source);
								img.Aspect = Aspect.Fill;
								img.HeightRequest = pngImageAttributtes.Height;
								img.WidthRequest = pngImageAttributtes.Width;

							}

							if (child is SVGImage svgImg)
							{
								var svgImageAttributtes = GetSVGImage(!preserveSession);                                 
								svgImg.Source = svgImageAttributtes.Source;
								svgImg.Image = svgImageAttributtes;
								svgImg.HeightRequest = svgImageAttributtes.Height;
								svgImg.WidthRequest = svgImageAttributtes.Width;
                                
							}
							break;
						}
				}
			}

		}

		private static void FinalizeDimensions(Element element, out double height, out double width)
		{
			SVGImage svgImgLocal = new SVGImage();
			var source = "";

			if (element is Image img)
			{
				source = img.Source.ToString();
				source = source.Replace("File:", "").ToLower().Trim();

			}
			if (element is SVGImage svgImg)
			{
				source = svgImg.Source;
				svgImgLocal = svgImg;

			}
			height = DefaultHeight;
			width = DefaultWidth;
			switch (source)
			{
				case Small:
					ConfigureDimensions(SmallSize, ref height, ref width, svgImgLocal);
					break;

				case Medium:
					ConfigureDimensions(MediumSize, ref height, ref width, svgImgLocal);
					break;

				case Large:
					ConfigureDimensions(LargeSize, ref height, ref width, svgImgLocal);
					break;

				case ExtraLarge:
					ConfigureDimensions(ExtraLargeSize, ref height, ref width, svgImgLocal);
					break;

				case DoubleExtraLarge:
					ConfigureDimensions(DoubleExtraLargeSize, ref height, ref width, svgImgLocal);
					break;

				case TripleExtraLarge:
					ConfigureDimensions(TripleExtraLargeSize, ref height, ref width, svgImgLocal);
					break;


				default:
					try
					{
						if (source.Trim() != string.Empty)
						{
							var dimensions = source.Split(new string[] { "/" }, StringSplitOptions.None);

							if (Int32.TryParse(dimensions[0], out int localwidth))
							{
								width = localwidth;
							}

							if (Int32.TryParse(dimensions[1], out int localheight))
							{
								height = localheight;
							}

						}
					}
					catch
					{
						height = DefaultHeight;
						width = DefaultWidth;

					}

					break;
			}
		}

		private static void ConfigureDimensions(double size, ref double height, ref double width, SVGImage svgImgLocal)
		{
			switch (svgImgLocal.Shape)
			{
				case Shape.Square:
					height = width = size;
					break;
				case Shape.Rectangle10:
					ConfigureRectangle(size, out height, out width, 10);
					break;
				case Shape.Rectangle20:
					ConfigureRectangle(size, out height, out width, 20);
					break;
				case Shape.Rectangle30:
					ConfigureRectangle(size, out height, out width, 30);
					break;
				case Shape.Rectangle40:
					ConfigureRectangle(size, out height, out width, 40);
					break;
				case Shape.RoundedRectangle10:
					ConfigureRectangle(size, out height, out width, 10);
					break;
				case Shape.RoundedRectangle20:
					ConfigureRectangle(size, out height, out width, 20);
					break;
				case Shape.RoundedRectangle30:
					ConfigureRectangle(size, out height, out width, 30);
					break;
				case Shape.RoundedRectangle40:
					ConfigureRectangle(size, out height, out width, 40);
					break;
				case Shape.Circle:
					height = width = size;
					break;
				default:
					height = width = size;
					break;
			}
		}

		private static void ConfigureRectangle(double size, out double height, out double width, int factor)
		{
			var proportionalSize = ((int)Math.Round(100 * factor / size));
			height = size - proportionalSize;
			width = size + proportionalSize;
		}
   

		public static Preserver.Image GetPNGImage(bool isRandom = true)
		{
			if (isRandom)
			{
				if (PNGImageList.Count == 0)
				{
					PNGImageList.AddRange(LoadImagesResourcesList(PNGImageType));
				}
				var randomIndex = new Random().Next(PNGImageList.Count);
				var img = PNGImageList[randomIndex];
				PNGImageList.RemoveAt(randomIndex);
				return new Preserver.Image { Source = img };
			}
			else
			{
				try
				{
					if (ComponentBuilder.RestoredUIAttributes.Image.Any())
					{
						var img = ComponentBuilder.RestoredUIAttributes.Image[0];
						ComponentBuilder.RestoredUIAttributes.Image.RemoveAt(0);
						return img;
					}
				}
				finally
				{

				}
			}

			return GetPNGImage(true);
		}

		static Preserver.Image GeneratePseudoSVGImageAttributes()
		{
			var seed = new Random().Next(5, 50);
            var increment = new Random().Next(5, 50);

			var reset = seed;
   			
			var img = new Preserver.Image
			{
				OuterElementBackGroundColor = new Preserver.Color(LayoutBuilder.GetColor()),
				FillElementBackGroundColor = new Preserver.Color(LayoutBuilder.GetColor()),
				SeedX =seed,
 				IncrementX =increment,
				Source = nameof(SVGImage),
 
			};

            if (new Random().NextDouble() >= 0.5)
            {
                seed = reset;

            }
            else
            {
                seed = new Random().Next(5, 50);
                increment = new Random().Next(5, 50);
            }

			img.SeedY = seed;
			img.IncrementY = increment;

			return img;
		}

		public static Preserver.Image GetSVGImage(bool isRandom = true)
		{
			if (isRandom)
			{
				return GeneratePseudoSVGImageAttributes();
			}
			else
			{
				try
				{
					if (ComponentBuilder.RestoredUIAttributes.Image.Any())
					{
						var img = ComponentBuilder.RestoredUIAttributes.Image[0];
						ComponentBuilder.RestoredUIAttributes.Image.RemoveAt(0);
						return img;
					}
				}
				finally
				{

				}
			}

			return GetSVGImage(true);
		}

	}
}
