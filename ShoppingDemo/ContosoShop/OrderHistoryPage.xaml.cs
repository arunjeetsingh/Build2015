using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ContosoShop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderHistoryPage : Page
    {
        AppServiceConnection serviceConnection = null;
        string status = "Unknown";

        public OrderHistoryPage()
        {
            this.InitializeComponent();            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.RetrieveOrder();
        }

        private async Task RetrieveOrder()
        {
            // Read the recent order from disk
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("OrderDetails");
            if (file == null)
                return;
            IRandomAccessStream inStream = await file.OpenReadAsync();
            // Deserialize the Session State.
            DataContractSerializer serializer = new DataContractSerializer(typeof(OrderDetails));
            var details = (OrderDetails)serializer.ReadObject(inStream.AsStreamForRead());
            inStream.Dispose();

            //Invoke service to fetch shipping details
            await GetShippingStatus(details.OrderId);

            TransactionDetails.Text = String.Format("Order ID: {0}\n{1}\nOrder Placed On {2}\nStatus {3}", 
                details.OrderId, details.ItemName, details.OrderPlacedTime, status);
        }

        private async Task GetShippingStatus(string OrderId)
        {
            if (serviceConnection == null)
            {
                serviceConnection = new AppServiceConnection();
                serviceConnection.AppServiceName = "TrackingServiceEndpoint";
                // Package family name of the shipping app
                serviceConnection.PackageFamilyName = "21492134-53b0-490a-9e74-c0316e413a61_22pq88dgcvw56";

                var status = await serviceConnection.OpenAsync();

                if (status != AppServiceConnectionStatus.Success)
                {
                    Debug.WriteLine("Failed to open connection: " + status.ToString());
                    return;
                }

                Debug.WriteLine("Connection open!");
            }

            var message = new ValueSet();
            message.Add("PackageID", OrderId);

            Debug.WriteLine("Sending a message");
            AppServiceResponse response = await serviceConnection.SendMessageAsync(message);
            Debug.WriteLine("Message sent. Response received (" + response.Status + ")");

            if (response.Status == AppServiceResponseStatus.Success)
            {
                if (response.Message.Keys.Count() > 0)
                {
                    string key = response.Message.Keys.First();                    
                    this.status = response.Message[key] as string;
                }
            }
            else
            {
                Debug.WriteLine("Message send failed!");
            }
        }
       

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
