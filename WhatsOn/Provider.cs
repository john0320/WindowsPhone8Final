using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WhatsOn
{
    public class Provider
    {

        public static ListProviders providerJsonData = null;
        public ListProviders selectedItem { get; set; }
        public string urlProviderString { get; set; }

        public Provider()
        {
            //LoadProviderData();
        }


        [DataContract]

        public class ListProviders
        {
            [DataMember]
            public string lineupName { get; set; }

            [DataMember]
            public string lineupID { get; set; }
        }

        public void LoadProviderData()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                isNetwork = true;
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
                webClient.DownloadStringAsync(new Uri(urlProviderString));
            }
            else
            {
                isNetwork = false;
            }

        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var app = App.Current as App;
            var frame = App.Current.RootVisual as PhoneApplicationFrame;
            var container = frame.Content as SettingsProviderPage;

            if (e.Error == null)
            {
                try
                {

                    string json = e.Result;
                    if (!string.IsNullOrEmpty(json))
                    {
                        frame = App.Current.RootVisual as PhoneApplicationFrame;
                        container = frame.Content as SettingsProviderPage;

                        List<ListProviders> providerJsonData = JsonConvert.DeserializeObject<List<ListProviders>>(json);
                        container.ProvList.ItemsSource = providerJsonData;
                        container.ProviderProgressBar.Visibility = System.Windows.Visibility.Collapsed;

                    }
                }
                catch (Exception)
                {
                    container.ChooseProviderError.Visibility = System.Windows.Visibility.Visible;
                    container.ChooseProvider.Text = "";
                    container.ProviderProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            else
            {
                //container.errorMsg.Text = "Error, Web Service not available, Try Later!";
            }
        }

        public bool isNetwork { get; set; }
    }
}


