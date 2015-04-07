using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ContosoShop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void BuyMonitor_Click(object sender, RoutedEventArgs e)
        {
            Buy("4K monitor", "574.95", "4KMonitor.jpg");
        }        

        private void BuyLaptop_Click(object sender, RoutedEventArgs e)
        {
            Buy("Laptop", "999.00", "laptop.jpg");
        }

        private void BuyPhone_Click(object sender, RoutedEventArgs e)
        {
            Buy("Windows Phone", "199.00", "phone.jpg");
        }

        private void BuyTablet_Click(object sender, RoutedEventArgs e)
        {
            Buy("Tablet", "499.95", "tablet.jpg");
        }

        private async void Buy(string productName, string price, string filename)
        {
            var woodgroveUri = new Uri("woodgrove-pay:");

			var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "111055f1-4e28-4634-b304-e76934e91e53_876gvmnfevegr";

			var inputData = new ValueSet();
			inputData["ProductName"] = productName;
			inputData["Amount"] = price;
			inputData["Currency"] = "USD";

			StorageFolder assetsFolder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");

			StorageFile imgFile = await assetsFolder.GetFileAsync("irobot.png");

			inputData["ImageFileToken"] = SharedStorageAccessManager.AddFile(imgFile);

			//Get rid of the temporary file
			StorageFile transactionDetailsFile = ((await ApplicationData.Current.TemporaryFolder.TryGetItemAsync("transactiondetails.txt")) as StorageFile);
			if (transactionDetailsFile != null)
			{
				await transactionDetailsFile.DeleteAsync();
			}

			//We're sending the payment app an empty file that it can fill with transaction details
			transactionDetailsFile =
                await ApplicationData.Current.TemporaryFolder.CreateFileAsync("transactiondetails.txt", CreationCollisionOption.FailIfExists);
            inputData["TransactionDetailsToken"] = SharedStorageAccessManager.AddFile(transactionDetailsFile);

			LaunchUriResult result = await Launcher.LaunchUriForResultsAsync(woodgroveUri, options, inputData);	// .LaunchUriForResultsAndContinueAsync(woodgroveUri, options, continuationData, inputData);
			if (result.Status == LaunchUriStatus.Success &&
				result.Result["Status"] != null &&
				(result.Result["Status"] as string) == "Success")
			{
				this.Frame.Navigate(typeof(OrderPlacedPage), result.Result);
			}
        }

        private void OrderHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OrderHistoryPage));
        }        
    }
}
