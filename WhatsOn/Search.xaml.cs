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
    public partial class Search : PhoneApplicationPage
    {
        public static Listing listingData;
        public static AppSettings appSettings;

        public Search()
        {
            InitializeComponent();
            listingData = new Listing();
            appSettings = new AppSettings();
        }

        private void ApplicationBarAboutItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void Search_Click(object sender, EventArgs e)
        {
            string urlBuildString = "http://api.tvmedia.ca/tv/v2/";

            if (listingData.searchType == 0)
            {
                listingData.searchString = "";

                urlBuildString += "lineups/" + appSettings.ChooseProviderID + "/listings/chrono?" + appSettings.apiKey + "&detail=detailed";
                listingData.urlQueryString = urlBuildString;
            }

            if (listingData.searchType == 1)
            {
                listingData.searchString = searchShowName.Text;

                urlBuildString += "lineups/" + appSettings.ChooseProviderID + "/listings/chrono?" + appSettings.apiKey + "&detail=detailed";
                listingData.urlQueryString = urlBuildString;
            }
            else if (listingData.searchType == 2)
            {
                listingData.searchString = listBoxShowType.SelectedValue.ToString();
            }

            else if (listingData.searchType == 3)
            {
                listingData.searchString = searchNetworkName.Text;
            }

            listingData.LoadListingData();
            NavigationService.Navigate(new Uri("/SearchResults.xaml", UriKind.Relative));
        }


        private void HandleSearch(object sender, RoutedEventArgs e)
        {

            RadioButton rb = sender as RadioButton;
            string selectedRB = rb.Content.ToString();

            switch (selectedRB)
            {

                case "list all shows":
                    searchShowName.IsEnabled = false;
                    searchShowName.Visibility = Visibility.Collapsed;
                    searchNetworkName.IsEnabled = false;
                    searchNetworkName.Visibility = Visibility.Collapsed;
                    listBoxShowType.Visibility = Visibility.Collapsed;
                    searchNetworkName.Text = "";
                    searchShowName.Text = "";
                    listingData.searchType = 0;
                    break;

                case "by show name":
                    searchShowName.IsEnabled = true;
                    searchShowName.Visibility = Visibility.Visible;
                    searchNetworkName.IsEnabled = false;
                    searchNetworkName.Visibility = Visibility.Collapsed;
                    listBoxShowType.Visibility = Visibility.Collapsed;
                    searchNetworkName.Text = "";
                    searchShowName.Text = "";
                    listingData.searchType = 1;

                    break;

                case "by show type":
                    searchShowName.IsEnabled = false;
                    searchShowName.Visibility = Visibility.Collapsed;
                    searchNetworkName.IsEnabled = false;
                    searchNetworkName.Visibility = Visibility.Collapsed;
                    listBoxShowType.Visibility = Visibility.Visible;
                    listBoxShowType.SelectedItem = null;
                    searchNetworkName.Text = "";
                    searchShowName.Text = "";
                    listingData.searchType = 1;


                    break;

                case "by network name":
                    searchNetworkName.IsEnabled = true;
                    searchNetworkName.Visibility = Visibility.Visible;
                    searchShowName.IsEnabled = false;
                    searchShowName.Visibility = Visibility.Collapsed;
                    listBoxShowType.Visibility = Visibility.Collapsed;
                    searchShowName.Text = "";
                    searchNetworkName.Text = "";
                    listingData.searchType = 3;

                    break;
            }

        }



        private void Settings_Click(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }


    }
}