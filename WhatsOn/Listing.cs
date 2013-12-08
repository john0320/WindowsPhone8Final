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
using System.Windows;

namespace WhatsOn
{
    public class Listing
    {
        public static ListViewListing itemJsonData = null;
        public ListViewListing selectedItem { get; set; }
        public int searchType { get; set; }
        public string searchString { get; set; }
        public string urlQueryString { get; set; }

        public Listing()
        {
            //LoadListingData();
        }


        [DataContract]

        public class ListViewListing
        {
            [DataMember]
            public string callsign { get; set; }

            [DataMember]
            public string episodeTitle { get; set; }

            [DataMember]
            public string episodeNumber { get; set; }

            [DataMember]
            public string rating { get; set; }

            [DataMember]
            public string hd { get; set; }

            [DataMember]
            public string showType { get; set; }

            [DataMember]
            public string stationID { get; set; }

            [DataMember]
            public string channel
            {
                get
                {
                    { return stationID + " - " + callsign; }
                }
                set { channel = value; }
            }


            [DataMember]
            public string starRating { get; set; }

            [DataMember]
            public string description { get; set; }

            [DataMember]
            public DateTime listDateTime { get; set; }

            [DataMember]
            public string listDate
            {
                get
                {
                    { return listDateTime.ToShortDateString(); }
                }
                set { listDate = value; }
            }

            [DataMember]
            public string listTime
            {
                get
                {
                    { return listDateTime.ToShortTimeString(); }
                }
                set { listTime = value; }
            }


            [DataMember]
            public string duration { get; set; }

            [DataMember]
            public string showName { get; set; }

            [DataMember]
            public string showPicture { get; set; }

            [DataMember]
            public string network { get; set; }

            [DataMember]
            public string PicLink
            {
                get
                {
                    if (showPicture == null)
                    { return string.Format("/Assets/Images/noImage_150x100.png"); }
                    else { return string.Format("http://api.tvmedia.ca/images/shows/" + showPicture); }
                }
                set { PicLink = value; }
            }

            [DataMember]
            public string shortDesc
            {
                get
                {
                    if (description == null || description.Length <= 122)
                    {
                        return description;
                    }
                    else
                    {
                        string truncatedString = description.Substring(0, 125);
                        truncatedString = truncatedString.TrimEnd();
                        truncatedString += "...";
                        return truncatedString;
                    }
                }
                set { shortDesc = value; }
            }




        }

        public void LoadListingData()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                isNetwork = true;
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
                webClient.DownloadStringAsync(new Uri(urlQueryString));
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
            var container = frame.Content as SearchResults;

            if (e.Error == null)
            {

                string json = e.Result;
                if (!string.IsNullOrEmpty(json))
                {
                    frame = App.Current.RootVisual as PhoneApplicationFrame;
                    container = frame.Content as SearchResults;
                    if (searchType == 0)
                    {
                        List<ListViewListing> itemJsonData = JsonConvert.DeserializeObject<List<ListViewListing>>(json);
                        container.SearchProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                        container.LLS.ItemsSource = itemJsonData;
                    }
                    if (searchType == 1)
                    {
                        List<ListViewListing> itemJsonData = JsonConvert.DeserializeObject<IEnumerable<ListViewListing>>(json).Where(x => x.showName.ToUpper().Contains(searchString.ToUpper())).ToList();
                        container.SearchProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                        container.LLS.ItemsSource = itemJsonData;
                    }
                    if (searchType == 2)
                    {
                        List<ListViewListing> itemJsonData = JsonConvert.DeserializeObject<IEnumerable<ListViewListing>>(json).Where(x => x.showType == (searchString)).ToList();
                        container.SearchProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                        container.LLS.ItemsSource = itemJsonData;
                    }
                    if (searchType == 3)
                    {
                        List<ListViewListing> itemJsonData = JsonConvert.DeserializeObject<IEnumerable<ListViewListing>>(json).Where(x => x.callsign.ToUpper().Contains(searchString.ToUpper())).ToList();
                        container.SearchProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                        container.LLS.ItemsSource = itemJsonData;
                    }

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





