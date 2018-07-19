using System;
using Xamarin.Forms;

namespace XamarinFormsStarterKit.UserInterfaceBuilder
{
	public class ComponentBuilderOptions
	{

		public View Content
		{
			get;
			set;
		}

		public bool Apply
		{
			get;
			set;
		} = true;


		public bool EnableRepeater
		{
			get;
			set;
		} = false;

		public bool EnableRestorationOfUIAttributes
		{
			get;
			set;
		} = false;


		public bool EnableTapGestureRecognizers
		{
			get;
			set;
		} = false;


		public bool EnableUIAttributesGeneration
		{
			get;
			set;
		} = false;

		public bool CompressLayout
        {
            get;
            set;
        } = true;


		public bool SuppressAllBackGroundColor
		{
			get;
			set;
		} = false;

		public bool SuppressLayoutBackGroundColor
		{
			get;
			set;
		}= false;

		public bool SuppressImageBackGroundColor
		{
			get;
			set;
		}= false;

		public bool SuppressLoremTextBackGroundColor
		{
			get;
			set;
		}= false;

		public bool PreserveSession
		{
			get;
			set;
		} = false;

		public string PreserveXml
		{
			get;
			set;
		}

	}
}
