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

namespace WhatsOn
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        private void WebSite_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask wbt = new Microsoft.Phone.Tasks.WebBrowserTask();
            wbt.Uri = new Uri("http://algonquincollege.com");
            wbt.Show();
        }

        private void Email_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.Subject = "Email Developer / Support or Suggestions";
            emailComposeTask.To = "john0320@algonquinlive.com";
            emailComposeTask.Show();
        }
    }
}