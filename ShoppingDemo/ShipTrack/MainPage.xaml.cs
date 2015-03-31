using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShipTrack
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        AppServiceConnection serviceConnection = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Track_Click(object sender, RoutedEventArgs e)
        {
            if (serviceConnection == null)
            {
                serviceConnection = new AppServiceConnection();
                serviceConnection.AppServiceName = "TrackingServiceEndpoint";
                serviceConnection.PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
                
                var status = await serviceConnection.OpenAsync();

                if (status != AppServiceConnectionStatus.Success)
                {
                    this.StatusLog.Items.Add("Failed to open connection: " + status.ToString());
                    return;
                }

                this.StatusLog.Items.Add("Connection open!");
            }

            var message = new ValueSet();
            message.Add("PackageID", "12345");

            this.StatusLog.Items.Add("Sending a message");
            AppServiceResponse response = await serviceConnection.SendMessageAsync(message);
            this.StatusLog.Items.Add("Message sent. Response received (" + response.Status + ")");

            if (response.Status == AppServiceResponseStatus.Success)
            {
                if (response.Message.Keys.Count() > 0)
                {
                    string key = response.Message.Keys.First();
                    this.StatusLog.Items.Add("Status: " + response.Message[key] as string);
                }
            }
            else
            {
                this.StatusLog.Items.Add("Message send failed!");
            }
        }
    }
}
