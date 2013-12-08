using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WhatsOn
{
    public partial class SearchResults : PhoneApplicationPage
    {
        public static Listing listingData;

        public SearchResults()
        {
            InitializeComponent();
            listingData = new Listing();
            // if (listingData.isNetwork == true)
            //{
            //listingData.LoadListingData();
            DataContext = Listing.itemJsonData;

            //}
            //else
            //{
            //errorMsg.Text = "Error, Network not available, Try Later!";
            //}
        }




        private void ListDetail_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Listing.ListViewListing item = ((FrameworkElement)e.OriginalSource).DataContext as Listing.ListViewListing;
            listingData.selectedItem = item;
            if (item != null) // if fast-clicking, it is possible to get here with nothing selected.  Ignore
                NavigationService.Navigate(new Uri("/ListingDetail.xaml", UriKind.Relative));
        }



        private void ApplicationBarAboutItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainSettings.xaml", UriKind.Relative));
        }


    }
}