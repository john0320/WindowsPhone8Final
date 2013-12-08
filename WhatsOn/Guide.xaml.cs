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
    public partial class Guide : PhoneApplicationPage
    {

        public static GuideListings guideData;
        

        public Guide()
        {
            InitializeComponent();

            guideData = new GuideListings();
             if (guideData.isNetwork == true)
            {
                DataContext = GuideListings.guideJsonData;
            }
            else
            {
                //errorMsg.Text = "Error, Network not available, Try Later!";
            }
        }


        private void ApplicationBarAboutItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }
    }
}