using System;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms;
using XamarinFormsStarterKit.UserInterfaceBuilder.Builders;
using XamarinFormsStarterKit.UserInterfaceBuilder.Helpers;
using XamarinFormsStarterKit.UserInterfaceBuilder.Preserver;

namespace XamarinFormsStarterKit.UserInterfaceBuilder
{
	public static class ComponentBuilder
	{
		private const string XamlPlaygroundAssembly = "XamlPlayground";
		public static Preserve PreserveUIAttributes = new Preserve();
		public static Preserve RestoredUIAttributes = new Preserve();

		public static ComponentBuilderOptions Options
		{
			get;
			set;
		}

		public static void Init(ComponentBuilderOptions options)
		{
			if (!options.Apply)
			{
				return;
			}

			Options = options;
   
			if (options.EnableRepeater)
			{
				ConfigureRepeater(options);

			}
			if (options.EnableRestorationOfUIAttributes)
			{
				RestorePreserveUIAttributes(options);

			}
			if (options.EnableTapGestureRecognizers)
			{
				HookTapGestureRecognizer(options);

			}
			if (options.EnableUIAttributesGeneration)
			{
				GenerateUIAttributes(options);
			}
			if (options.CompressLayout)
			{
				CompressLayout(options);
			}


			LoadAllComponents(options);
		}

		private static void CompressLayout(ComponentBuilderOptions options)
		{
			ConfigureOptions(options, out Layout layout, out bool suppressLayout, out bool suppressImage, out bool suppressLoremText);

			LayoutBuilder.CompressLayoutAsHeadless(layout, options.CompressLayout);
		}

		private static void HookTapGestureRecognizer(ComponentBuilderOptions options)
		{
			ConfigureOptions(options, out Layout layout, out bool suppressLayout, out bool suppressImage, out bool suppressLoremText);

			LayoutBuilder.HookTapGestureRecognizer(layout, options.Apply);
		}

		private static void RestorePreserveUIAttributes(ComponentBuilderOptions options)
		{
			if (!options.PreserveSession)
			{
				return;
			}

			XmlSerializer deserializer = new XmlSerializer(typeof(Preserve));


			if (string.IsNullOrEmpty(options.PreserveXml))
			{
                TextReader reader = new StreamReader(ResourceLoader.GetEmbeddedResourceStream("Preserve.xml", XamlPlaygroundAssembly));
                try
                {
                    
                 RestoredUIAttributes = (Preserve)deserializer.Deserialize(reader);
                }
                finally
                {
                    reader.Close();
                }
			}
			else
			{
				using (var stream = options.PreserveXml.GenerateStreamFromString())
                {
					RestoredUIAttributes = (Preserve)deserializer.Deserialize(stream);
                }
    
			}
            
		}

		private static void ConfigureRepeater(ComponentBuilderOptions options)
		{
			RepeaterBuilder.Repeat((Layout)options.Content, options.Apply);
		}

		public static void GenerateUIAttributes(ComponentBuilderOptions options)
		{
			ConfigureOptions(options, out Layout layout, out bool suppressLayout, out bool suppressImage, out bool suppressLoremText);

			LayoutBuilder.GenerateLayoutColors(layout, suppressLayout);
			TextBuilder.GenerateLoremText(layout, suppressLoremText);
			ImageBuilder.GenerateImage(layout, suppressImage);
		}

		public static void LoadAllComponents(ComponentBuilderOptions options)
		{
			ConfigureOptions(options, out Layout layout, out bool suppressLayout, out bool suppressImage, out bool suppressLoremText);

			LayoutBuilder.ColorizeLayout(layout, options.Apply, suppressLayout, options.PreserveSession);
 			ImageBuilder.LoadImage(layout, options.Apply, suppressImage, options.PreserveSession);
			TextBuilder.LoadLoremText(layout, options.Apply, suppressLoremText, options.PreserveSession);
		}

		private static void ConfigureOptions(ComponentBuilderOptions options, out Layout layout, out bool suppressLayout, out bool suppressImage, out bool suppressLoremText)
		{
			layout = (Layout)options.Content;
			suppressLayout = false;
			suppressImage = false;
			suppressLoremText = false;
			var suppressAll = options.SuppressAllBackGroundColor;
			if (suppressAll)
			{
				suppressLayout = true;
				suppressImage = true;
				suppressLoremText = true;

			}
			else
			{
				suppressLayout = options.SuppressLayoutBackGroundColor;
				suppressImage = options.SuppressImageBackGroundColor;
				suppressLoremText = options.SuppressLoremTextBackGroundColor;
			}
		}

		public static void LoadLayoutComponent(View view, bool apply = true)
		{
			var layout = (Layout)view;


			LayoutBuilder.ColorizeLayout(layout, apply);
			LayoutBuilder.CompressLayoutAsHeadless(layout, apply);


		}

		public static void LoadImageComponent(View view, bool apply = true, bool suppressBackGroundColor = true)
		{
			var layout = (Layout)view;

			ImageBuilder.LoadImage(layout, apply, suppressBackGroundColor);
		}

		public static void LoadLoremTextComponent(View view, bool apply = true, bool suppressBackGroundColor = true)
		{
			var layout = (Layout)view;

			TextBuilder.LoadLoremText(layout, apply, suppressBackGroundColor);
		}
	}
}
