using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using System.Runtime.InteropServices;
using NativeCode;

namespace share
{
	enum ShareType
	{
		Text,
		WebLink,
		Data,
		DelayedRendering,
		File
	};

	public partial class Form1 : Form
	{
		ShareType _ShareType;
		DataTransferManagerHelper dtmHelper;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			SHareDataBox.Text = @"{
    ""type"" : ""http://schema.org/Book"",
    ""properties"" :
    {
    ""image"" : ""http://sourceurl.com/catcher-in-the-rye-book-cover.jpg"",
    ""name"" : ""The Catcher in the Rye"",
    ""bookFormat"" : ""http://schema.org/Paperback"",
    ""author"" : ""http://sourceurl.com/author/jd_salinger.html"",
    ""numberOfPages"" : 224,
    ""publisher"" : ""Little, Brown, and Company"",
    ""datePublished"" : ""1991-05-01"",
    ""inLanguage"" : ""English"",
    ""isbn"" : ""0316769487""
    }
    }";
		}

		private void DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
		{
			DataPackage dataPackage = e.Request.Data;
			switch (_ShareType)
			{
				case ShareType.Text:
					{
						dataPackage.Properties.Title = "Share Text Example";
						dataPackage.Properties.Description = "An example of how to share text.";
						dataPackage.SetText(ShareTextBox.Text);
						break;
					}
				case ShareType.WebLink:
					{
						dataPackage.Properties.Title = "Share WebLink Example";
						dataPackage.Properties.Description = "An example of how to share WebLink.";	// The description is optional.
						dataPackage.SetWebLink(new Uri(ShareWeblinkBox.Text));
						break;
					}

				case ShareType.Data:
					{
						string dataPackageFormat = "http://schema.org/Book";
						string data = SHareDataBox.Text;
						dataPackage.Properties.Title = "Share Data Example";
						dataPackage.Properties.Description = "An example of how to share Data."; // The description is optional.
						dataPackage.SetData(dataPackageFormat, data);
						break;
					}

				case ShareType.DelayedRendering:
					dataPackage.Properties.Title = "Share Delayed Example";
					dataPackage.Properties.Description = "An example of how to share Delayed rendering."; // The description is optional.
					dataPackage.SetDataProvider(StandardDataFormats.Text, providerRequest => this.OnDeferredTextRequestedHandler(providerRequest));
					break;
				default:
					break;
			}

		}

		private void OnDeferredTextRequestedHandler(DataProviderRequest providerRequest)
		{
			string text = "Hello World delayed";
			providerRequest.SetData(text);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_ShareType = ShareType.Text;

			dtmHelper.ShowShareUI();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_ShareType = ShareType.WebLink;

			dtmHelper.ShowShareUI();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			_ShareType = ShareType.Data;

			dtmHelper.ShowShareUI();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			_ShareType = ShareType.DelayedRendering;

			dtmHelper.ShowShareUI();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			dtmHelper = new DataTransferManagerHelper(this.Handle);
			dtmHelper.DataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
		}
	}
}
