using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using XamarinFormsStarterKit.UserInterfaceBuilder.ViewModels;

namespace XamarinFormsStarterKit.UserInterfaceBuilder.UIElements
{
	[ContentProperty("ItemTemplate")]
	public class RepeaterView : StackLayout
	{
		private const int DefaultValue = 1;

		public static readonly BindableProperty RepeatCountProperty = BindableProperty.Create(
		nameof(RepeatCount),
		typeof(int),
		typeof(RepeaterView),
		DefaultValue,
		BindingMode.OneWay
		);

		public static readonly BindableProperty ContentViewProperty = BindableProperty.Create(
			nameof(ContentView),
			typeof(ContentView),
		typeof(RepeaterView),
		null,
		BindingMode.OneWay
		);

		public int RepeatCount
		{
			get
			{
				return (int)GetValue(RepeatCountProperty);
			}

			set
			{
				SetValue(RepeatCountProperty, value);
				var repeaterVM = new RepeaterViewModel { RepeatCount = value };
				ItemsSource = repeaterVM.RepeatItems;
			}
		}

		public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
			nameof(ItemTemplate),
			typeof(DataTemplate),
			typeof(RepeaterView),
			default(DataTemplate));

		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
			nameof(ItemsSource),
			typeof(ICollection),
			typeof(RepeaterView),
			null,
			BindingMode.OneWay,
			propertyChanged: ItemsChanged);

		public RepeaterView()
		{
			CompressedLayout.SetIsHeadless(this, true);
			Spacing = 0;
		}

		public ICollection ItemsSource
		{
			get => (ICollection)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		public DataTemplate ItemTemplate
		{
			get => (DataTemplate)GetValue(ItemTemplateProperty);
			set => SetValue(ItemTemplateProperty, value);
		}

		protected virtual View ViewFor(object item)
		{
			View view = null;

			if (ItemTemplate != null)
			{
				var content = ItemTemplate.CreateContent();

				view = content is View ? content as View : ((ViewCell)content).View;

				view.BindingContext = item;
			}

			return view;
		}

		private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!(bindable is RepeaterView control)) return;

			control.Children.Clear();

			var items = (ICollection)newValue;

			if (items == null) return;

			foreach (var item in items)
			{
				control.Children.Add(control.ViewFor(item));
			}
		}
	}

}