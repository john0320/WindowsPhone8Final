using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using Newtonsoft.Json;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;
using System.Text.RegularExpressions;

namespace WhatsOn
{
    public partial class SettingsLocationPage : PhoneApplicationPage
    {
        public static AppSettings appSettings;
        private GeoCoordinate MyCoordinate = null;
        private ReverseGeocodeQuery MyReverseGeocodeQuery = null;
        private double _accuracy = 0.0;
        public bool isNetwork { get; set; }

        public SettingsLocationPage()
        {
            InitializeComponent();
            appSettings = new AppSettings();

            LocationProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private async void GetCurrentCoordinate()
        {
            LocationProgressBar.Visibility = System.Windows.Visibility.Visible;
            textBoxPostalCode.Text = "";
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;

            try
            {
                Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                _accuracy = currentPosition.Coordinate.Accuracy;

                MyCoordinate = new GeoCoordinate(currentPosition.Coordinate.Latitude, currentPosition.Coordinate.Longitude);

                if (MyReverseGeocodeQuery == null || !MyReverseGeocodeQuery.IsBusy)
                {
                    MyReverseGeocodeQuery = new ReverseGeocodeQuery();
                    MyReverseGeocodeQuery.GeoCoordinate = new GeoCoordinate(MyCoordinate.Latitude, MyCoordinate.Longitude);
                    MyReverseGeocodeQuery.QueryCompleted += ReverseGeocodeQuery_QueryCompleted;
                    MyReverseGeocodeQuery.QueryAsync();
                }
            }
            catch (Exception)
            {
                invalidMessage.Text = "Unable to find Valid Postal Code";
            }
        }

        private void ReverseGeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                if (e.Result.Count > 0)
                {
                    MapAddress address = e.Result[0].Information.Address;
                    textBoxPostalCode.Text = address.PostalCode;
                    LocationProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        private void HandlePCSetting(int handleButton)
        {
            if (handleButton == 1)
            {
                invalidMessage.Visibility = System.Windows.Visibility.Collapsed;
                GetCurrentCoordinate();
                textBoxPostalCode.IsReadOnly = true;
            }
            else
            {
                textBoxPostalCode.Text = "";
                textBoxPostalCode.IsReadOnly = false;
                textBoxPostalCode.Focus();
            }
        }


        private void ApplicationBarAboutItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }



        private void HandleUsePostalCode(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            int handleButton;
            if (rb == radioButton1)
            {
                handleButton = 1;
            }
            else
            {
                handleButton = 2;
            }
            HandlePCSetting(handleButton);
        }

        private void textBoxPostalCode_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool pcValid = IsPostalCode(tb.Text);
            if (!pcValid)
            {
                invalidMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else { invalidMessage.Visibility = System.Windows.Visibility.Collapsed; }
        }


        public static bool IsPostalCode(string tbPC)
        {
            //Canadian Postal Code in the format of "M3A 1A5 - space is optional"
            string pattern = "^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (!(reg.IsMatch(tbPC)))
                return false;
            return true;
        }




    }
}