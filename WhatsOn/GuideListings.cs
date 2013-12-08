using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WhatsOn
{
    public class GuideListings 
    {
        public static GuideListListings guideJsonData = null;
       
        public GuideListings()
        {
            LoadListingData();
        }

        [DataContract]

        public class GuideListListings
        {
           
            [DataMember]
            public string episodeTitle { get; set; }

            [DataMember]
            public string showType { get; set; }

            [DataMember]
            public string listDateTime { get; set; }

            [DataMember]
            public string duration { get; set; }

            [DataMember]
            public string showName { get; set; }

            [DataMember]
            public string callsign { get; set; }

        }
        private void LoadListingData()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                isNetwork = true;
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
                webClient.DownloadStringAsync(new Uri("http://api.tvmedia.ca/tv/v2/lineups/5109/listings/chrono?api_key=7bd7c3b4c3ceb3c4572de29de52bc5c5&detail=detailed"));
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
            var container = frame.Content as Guide;

            if (e.Error == null)
            {
                string json = e.Result;
                if (!string.IsNullOrEmpty(json))
                {
                    List<GuideListListings> guideJsonData = JsonConvert.DeserializeObject<List<GuideListListings>>(json);

                    app = App.Current as App;
                    frame = App.Current.RootVisual as PhoneApplicationFrame;
                    container = frame.Content as Guide;
                    container.LLS.ItemsSource = guideJsonData;
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
