﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DnnSummit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScheduleDetailsPage : ContentPage
	{
		public ScheduleDetailsPage ()
		{
			InitializeComponent ();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView != null)
            {
                listView.SelectedItem = null;
            }
        }
    }
}