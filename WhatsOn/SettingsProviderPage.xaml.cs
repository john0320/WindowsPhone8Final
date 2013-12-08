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
    public partial class SettingsProviderPage : PhoneApplicationPage
    {
        public static AppSettings appSettings;
        public static Provider provider;
        public string postalcode;

        public bool isNetwork { get; set; }


        public SettingsProviderPage()
        {
            InitializeComponent();
            appSettings = new AppSettings();
            provider = new Provider();
            postalcode = appSettings.PostalCodeSetting;
            LoadProviders();
        }

       

        private void LoadProviders()
        {
            //can't get here until have valid postalcode, but check it again
           // if (postalcode != null) or is valid
            //{
                string urlProvider = "http://api.tvmedia.ca/tv/v2/lineups?postalCode=" + postalcode + "&" + appSettings.apiKey;
                // http://api.tvmedia.ca/tv/v2/lineups?postalCode=k2c0s1&api_key=7bd7c3b4c3ceb3c4572de29de52bc5c5
                // http://api.tvmedia.ca/tv/v2/lineups/261/listings/chrono?api_key=7bd7c3b4c3ceb3c4572de29de52bc5c5&detail=detailed
                provider.urlProviderString = urlProvider;
                provider.LoadProviderData();
            //}
            //else MessageBox.Show("Postal Code Needs to be sourced");
        }

        private void ProviderChoosen_Selection(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Provider.ListProviders item = ((FrameworkElement)e.OriginalSource).DataContext as Provider.ListProviders;
            provider.selectedItem = item;
            ChooseProvider.Text = provider.selectedItem.lineupName;
            ChooseProviderID.Text = provider.selectedItem.lineupID;
        }

        private void ApplicationBarAboutItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }


    }
}