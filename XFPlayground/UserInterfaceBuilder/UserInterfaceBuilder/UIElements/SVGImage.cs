using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using XamarinFormsStarterKit.UserInterfaceBuilder.Helpers;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;


namespace XamarinFormsStarterKit.UserInterfaceBuilder.UIElements
{
	public enum Shape
	{
		Square,
		Rectangle10,
		Rectangle20,
		Rectangle30,
		Rectangle40,
		RoundedRectangle10,
		RoundedRectangle20,
		RoundedRectangle30,
		RoundedRectangle40,
		Circle
	}

	public class SVGImage : SKCanvasView
	{
		private const string Rect = "rect";
		private const string Circle = "circle";
		public static readonly BindableProperty SourceProperty = BindableProperty.Create(
			nameof(Source), typeof(string), typeof(SVGImage), "m", propertyChanged: RedrawCanvas);

		public string Source
		{
			get { return (string)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }

		}

		public static readonly BindableProperty GrowPercentProperty = BindableProperty.Create(
			nameof(GrowPercent), typeof(double), typeof(SVGImage), 100d, propertyChanged: RedrawCanvas);

		public double GrowPercent
		{
			get { return (double)GetValue(GrowPercentProperty); }
			set { SetValue(GrowPercentProperty, value); }

		}

		public Preserver.Image Image
		{
			get;
			set;
		}

		public static readonly BindableProperty ShapeProperty = BindableProperty.Create(
			nameof(Shape), typeof(Shape), typeof(SVGImage), Shape.Square, propertyChanged: RedrawCanvas);

		public Shape Shape
		{
			get { return (Shape)GetValue(ShapeProperty); }
			set { SetValue(ShapeProperty, value); }

		}
		public SVGImage()
		{
			PaintSurface += CanvasViewOnPaintSurface;
		}

		private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
		{
			var svgImage = bindable as SVGImage;
			svgImage?.InvalidateSurface();
		}

		private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
		{
			if (string.IsNullOrEmpty(Source))
				return;
   
			using (var reader = GenerateSVG(args.Info.Width, args.Info.Height).CreateReader())
			{
				var svg = new SKSvg();
				svg.Load(reader);

				var surface = args.Surface;
				var canvas = surface.Canvas;
				canvas.Clear(SKColors.White);

				canvas.DrawPicture(svg.Picture);
				canvas.Dispose();

			}
		}

		// todo change to Skia sharp Generated image rather than manually formed XML
		public XDocument GenerateSVG(double width, double height)
		{
			XDocument xDocument = CreateSVGRoot();

			CreateOuterElement(width, height, xDocument, this.Shape);

			return CreateFillElement(width, height, xDocument, this.Shape);
		}

		private XDocument CreateFillElement(double width, double height, XDocument xDocument, Shape shape)
		{
			string color = SetFillElementColor();

			var gElement = new XElement("g", new XAttribute("fill", color));

			var seed = new Random().Next(5, 50);
			var increment = new Random().Next(5, 50);

			if (ComponentBuilder.Options.PreserveSession)
			{
				seed = Image.SeedX;
				increment = Image.IncrementX;
			}

			var svgShape = shape == Shape.Circle ? Circle : Rect;

			var reset = seed;

			while (seed <= width)
			{
				gElement.Add(new XElement(svgShape,
										  new XAttribute("width", "1"),
										  new XAttribute("height", height),
										  new XAttribute("x", seed.ToString())));
				seed = seed + increment;
			}

			if (new Random().NextDouble() >= 0.5)
			{
				seed = reset;

			}
			else
			{
				seed = new Random().Next(5, 50);
				increment = new Random().Next(5, 50);
			}

			if (ComponentBuilder.Options.PreserveSession)
			{
				seed = Image.SeedY;
				increment = Image.IncrementY;
			}

			while (seed <= height)
			{

				gElement.Add(new XElement(svgShape,
										  new XAttribute("width", width),
												  new XAttribute("height", "1"),
									  new XAttribute("y", seed.ToString())));

				seed = seed + increment;

			}

			xDocument.Root.Add(gElement);

			return xDocument;
		}

		private string SetFillElementColor()
		{
			var color = Color.Default.ToSKColor().ToString();

			if (ComponentBuilder.Options.PreserveSession)
			{
				color = Image.FillElementBackGroundColor.ToXamarinColor().ToSKColor().ToString();
			}
			else
			{
				color = RandomColor();
			}

			return color;
		}

		private string SetOuterElementColor()
		{
			var color = Color.Default.ToSKColor().ToString();

			if (ComponentBuilder.Options.PreserveSession)
			{
				color = Image.OuterElementBackGroundColor.ToXamarinColor().ToSKColor().ToString();
			}
			else
			{
				color = RandomColor();
			}

			return color;
		}

		private void CreateOuterElement(double width, double height, XDocument xDocument, Shape shape)
		{
			string color = SetOuterElementColor();
            
			switch (shape)
			{
				case Shape.Square:
				case Shape.Rectangle10:
				case Shape.Rectangle20:
				case Shape.Rectangle30:
				case Shape.Rectangle40:
					{
						CreaterRect(width, height, xDocument, color);
						break;
					}
				case Shape.RoundedRectangle10:
					{
						CreaterRoundRect(width, height, "10", "10", xDocument, color);
						break;
					}
				case Shape.RoundedRectangle20:
					{
						CreaterRoundRect(width, height, "20", "20", xDocument, color);
						break;
					}
				case Shape.RoundedRectangle30:
					{
						CreaterRoundRect(width, height, "30", "30", xDocument, color);
						break;
					}
				case Shape.RoundedRectangle40:
					{
						CreaterRoundRect(width, height, "40", "40", xDocument, color);
						break;
					}
				case Shape.Circle:
					{
						CreateCircle(width, height, xDocument, color);
						break;
					}
				default:
					{
						CreaterRect(width, height, xDocument, color);
						break;
					}
			}

		}


		private void CreaterRoundRect(double width, double height, string rx, string ry, XDocument xDocument, string color)
		{
			xDocument.Root.Add(new XElement(Rect,
								new XAttribute("width", width),
								new XAttribute("height", height),
								new XAttribute("rx", rx),
								new XAttribute("ry", ry),
								new XAttribute("style", "stroke:black;stroke-width:1;opacity:0.5"),
											new XAttribute("fill", color)));
		}

		private void CreateCircle(double width, double height, XDocument xDocument, string color)
		{
			xDocument.Root.Add(new XElement(Circle,
								new XAttribute("r", width / 2),
											new XAttribute("cx", width / 2),
											new XAttribute("cy", height / 2),
								new XAttribute("style", "stroke:black;stroke-width:1;opacity:0.5"),
											new XAttribute("fill", color)));
		}

		private void CreaterRect(double width, double height, XDocument xDocument, string color)
		{
			xDocument.Root.Add(new XElement(Rect,
								new XAttribute("width", width),
								new XAttribute("height", height),
								new XAttribute("style", "stroke:black;stroke-width:1;opacity:0.5"),
								new XAttribute("fill", color)));
		}



		private static string RandomColor()
		{
			var color = LayoutBuilder.GetColor().ToSKColor().ToString();

			while ((color == Color.White.ToSKColor().ToString() ||
					color == Color.WhiteSmoke.ToSKColor().ToString() ||
					color == Color.FloralWhite.ToSKColor().ToString() ||
					color == Color.NavajoWhite.ToSKColor().ToString() ||
					color == Color.GhostWhite.ToSKColor().ToString()
				   ))
			{

				return Color.FromRgb(new Random().Next(255) / 255.0f,
									 new Random().Next(255) / 255.0f,
									 new Random().Next(255) / 255.0f).ToSKColor().ToString();

			}

			return color;
		}

		private XDocument CreateSVGRoot()
		{
			var svgXml = @"<?xml version='1.0' encoding='UTF-8' ?>
                                        <svg xmlns='http://www.w3.org/2000/svg' 
                                        width ='100%' height ='100%' 
                                        ></svg>";
			var xDocument = XDocument.Parse(svgXml);
			return xDocument;
		}
	}
}
