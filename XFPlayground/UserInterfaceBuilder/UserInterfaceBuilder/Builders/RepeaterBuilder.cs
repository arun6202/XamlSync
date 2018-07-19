using Xamarin.Forms;
using XamarinFormsStarterKit.UserInterfaceBuilder.UIElements;
using XamarinFormsStarterKit.UserInterfaceBuilder.ViewModels;

namespace XamarinFormsStarterKit.UserInterfaceBuilder.Builders
{
	public static class RepeaterBuilder
	{
		public static void Repeat(Layout layout, bool apply = true)
		{
			if (!apply)
			{
				return;
			}
			if (layout is RepeaterView repeaterView)
			{
				var repeater = new RepeaterViewModel { RepeatCount = repeaterView.RepeatCount };
				repeaterView.ItemsSource = repeater.RepeatItems;

			}

			foreach (var child in layout.Children)
			{

				if (child is Layout currentLayout)
				{
					Repeat(currentLayout, apply);
				}

			}
		}
	}
}
