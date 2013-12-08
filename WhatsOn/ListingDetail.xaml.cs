using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Globalization;


namespace WhatsOn
{
    public partial class ListingDetail : PhoneApplicationPage
    {
        public ListingDetail()
        {
            InitializeComponent();

            if (SearchResults.listingData.selectedItem.showPicture != null)
            {
                string url = SearchResults.listingData.selectedItem.PicLink;
                showImage.Source = new BitmapImage(new Uri(url, UriKind.Absolute)); ;
            }
            episodeTitle.Text = SearchResults.listingData.selectedItem.episodeTitle;
            episodeNumber.Text = SearchResults.listingData.selectedItem.episodeNumber;
            rating.Text = SearchResults.listingData.selectedItem.rating;
            if (SearchResults.listingData.selectedItem.hd == "1")
            {
                hd.Text = "SD";
            }
            else { hd.Text = "HD"; }

            showType.Text = SearchResults.listingData.selectedItem.showType;
            description.Text = SearchResults.listingData.selectedItem.description;
            showName.Text = SearchResults.listingData.selectedItem.showName;
            listTime.Text = SearchResults.listingData.selectedItem.listTime;
            listDate.Text = SearchResults.listingData.selectedItem.listDate;
            channel.Text = SearchResults.listingData.selectedItem.channel;
            network.Text = SearchResults.listingData.selectedItem.network;
            duration.Text = SearchResults.listingData.selectedItem.duration;
        }

        private void Reminder_Click(object sender, EventArgs e)
        {
            SaveAppointmentTask SATask = new SaveAppointmentTask();
            SATask.Subject = "TV Show" + SearchResults.listingData.selectedItem.showName;
            SATask.Location = "ON TV";
            SATask.Details = SearchResults.listingData.selectedItem.episodeTitle;
            //prompt user for reminder?
            SATask.Reminder = Reminder.OneHour;

            SATask.AppointmentStatus = Microsoft.Phone.UserData.AppointmentStatus.Busy;
            //get the date/time of the show and set as starttime
            DateTime dt1 = DateTime.Parse(SearchResults.listingData.selectedItem.listDate, CultureInfo.InvariantCulture);
            var date = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            string[] split = SearchResults.listingData.selectedItem.listTime.Split(new Char[] { ':', ' ' });
            var time = 0;
            if (split[2] == "PM")
            {
                time = int.Parse(split[0]) + 12;
            }
            else time = int.Parse(split[0]);

            var showTime = date.AddHours(time);
            showTime = showTime.AddMinutes(int.Parse(split[1]));
            SATask.StartTime = showTime;
            //get the duration of the show to add to start time
            int t1 = int.Parse(SearchResults.listingData.selectedItem.duration);
            SATask.EndTime = showTime.AddMinutes(t1);
            SATask.Show();

        }

        private void Share_Click(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.Subject = SearchResults.listingData.selectedItem.showName + " - Check it out!";
            emailComposeTask.Show();
        }

        private void Favs_Click(object sender, EventArgs e)
        {
            //Need to have a fav list saved to local json file
            //this click would add the show title to the list
        }

        private void ApplicationBarAboutItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }
    }
}