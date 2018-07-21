﻿using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views.Templates
{
    public partial class UwpNotificationItemTemplate : ContentView
    {
        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create("TapCommand", typeof(ICommand), typeof(NotificationItemTemplate));

        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        public UwpNotificationItemTemplate()
        {
            InitializeComponent();
        }
    }
}