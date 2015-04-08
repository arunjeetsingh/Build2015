using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Activation;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WoodgroveBank
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        private ProtocolForResultsOperation operation = null;
		private string productName = null;

        public PaymentPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var protocolForResultsArgs = e.Parameter as ProtocolForResultsActivatedEventArgs;
            this.operation = protocolForResultsArgs.ProtocolForResultsOperation;
			productName = protocolForResultsArgs.Data["ProductName"] as string;
			this.Item.Text = productName;
            this.Amount.Text = "$" + protocolForResultsArgs.Data["Amount"] as string;

            if (protocolForResultsArgs.Data.ContainsKey("ImageFileToken"))
            {
                string imageFileToken = protocolForResultsArgs.Data["ImageFileToken"] as string;

                StorageFile imageFile = await SharedStorageAccessManager.RedeemTokenForFileAsync(imageFileToken);
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(await imageFile.OpenReadAsync());
                ProductImage.Source = bitmap;
                ProductImage.Height = 200;
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            ValueSet result = new ValueSet();
            result["Status"] = "Success";
			result["ProductName"] = productName;
            operation.ReportCompleted(result);
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            ValueSet result = new ValueSet();
            result["Status"] = "Cancelled";
			result["ProductName"] = productName;
			operation.ReportCompleted(result);
        }
    }
}
