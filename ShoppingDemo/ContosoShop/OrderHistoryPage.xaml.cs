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
            await GetShippingStatus(details.TrackingNumber);

            TransactionDetails.Text = String.Format("Order ID: {0}\n{1}\nOrder Placed On: {2}\nStatus: {3}\nTracking No: {4}", 
                details.OrderID, details.ItemName, details.OrderPlacedTime, status, details.TrackingNumber);
        }

        private async Task GetShippingStatus(string TrackingNumber)
        {
            //openc

            //sendm
        }
       

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
