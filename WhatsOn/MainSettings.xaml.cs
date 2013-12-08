using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace WhatsOn
{
    public partial class MainSettings : PhoneApplicationPage
    {
        public static AppSettings appSettings;
        public static Provider provider;
        public bool isNetwork { get; set; }

        public MainSettings()
        {
            InitializeComponent();
            appSettings = new AppSettings();
            CheckPostalCode();
            CheckProvider();

            if (appSettings.ChooseProvider == "")
            {
                ProviderSet.Text = "*Required";
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Red;
                ProviderSet.Foreground = Brush1;
            }
            else
            {
                ProviderSet.Text = appSettings.ChooseProvider;
            }
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                CheckPostalCode();
                CheckProvider();
            }
        }

        private void CheckPostalCode()
        {
            if (appSettings.PostalCodeSetting == "")
            {
                PostalCodeSet.Text = "*Required";
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Red;
                PostalCodeSet.Foreground = Brush1;
                ValidPC.Visibility = System.Windows.Visibility.Collapsed;
                IsValidPC.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (!IsPostalCode(appSettings.PostalCodeSetting))
            {
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Red;
                PostalCodeSet.Foreground = Brush1;
                PostalCodeSet.Text = appSettings.PostalCodeSetting;
                ValidPC.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                PostalCodeSet.Text = appSettings.PostalCodeSetting;
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Green;
                PostalCodeSet.Foreground = Brush1;
                ValidPC.Visibility = System.Windows.Visibility.Collapsed;
                IsValidPC.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void CheckProvider()
        {
            if (appSettings.ChooseProvider == "")
            {
                ProviderSet.Text = "*Required";
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Red;
                ProviderSet.Foreground = Brush1;
            }
            else
            {
                ProviderSet.Text = appSettings.ChooseProvider;
                SolidColorBrush Brush1 = new SolidColorBrush();
                Brush1.Color = Colors.Green;
                ProviderSet.Foreground = Brush1;
            }


        }
        public static bool IsPostalCode(string tbPC)
        {
            //Canadian Postal Code in the format of "M3A 1A5"
            string pattern = "^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (!(reg.IsMatch(tbPC)))
                return false;
            return true;
        }



        private void ApplicationBarAboutItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void ChangeProvider_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (IsPostalCode(appSettings.PostalCodeSetting))
            {
                NavigationService.Navigate(new Uri("/SettingsProviderPage.xaml", UriKind.Relative));
            }
            else
            {
                IsValidPC.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ChangePostalCode_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsLocationPage.xaml", UriKind.Relative));
        }


    }
}