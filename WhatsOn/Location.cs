using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;

namespace WhatsOn
{
    class Location
    {
        double longC;
        double latC;

        public async void GetCurrentLocation()
        {
            Geolocator locationFinder = new Geolocator
            {
                DesiredAccuracyInMeters = 50,
                DesiredAccuracy = PositionAccuracy.Default
            };
            try
            {
                Geoposition currentLocation = await locationFinder.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromSeconds(120),
                    timeout: TimeSpan.FromSeconds(10));

                longC = currentLocation.Coordinate.Longitude;
                latC = currentLocation.Coordinate.Latitude;
                String longitude = currentLocation.Coordinate.Longitude.ToString("0.00");
                String latitude = currentLocation.Coordinate.Latitude.ToString("0.00");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("And Exception Occured");
            }

        }
    }
}
