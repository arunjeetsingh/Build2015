using NativeCode;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace WpfShareSource
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DataTransferManagerHelper dtmHelper = null;
		ShareMode currentShareMode = ShareMode.Blank;
		List<IStorageItem> filesToShare = null;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void EnsureDataTransferManager()
		{
			if (this.dtmHelper == null)
			{
				IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                this.dtmHelper = new DataTransferManagerHelper(windowHandle);
				this.dtmHelper.DataTransferManager.DataRequested += this.OnDataRequested;
			}
		}

		private void ShareTextButton_Click(object sender, RoutedEventArgs e)
		{
            this.EnsureDataTransferManager();
            this.currentShareMode = ShareMode.Text;
            this.dtmHelper.ShowShareUI();
		}

		private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
		{
			DataPackage dp = args.Request.Data;
			dp.Properties.Title = DataPackageTitle.Text;

			switch (currentShareMode)
			{
				case ShareMode.Text:
					dp.SetText(DataPackageText.Text);
					break;

				case ShareMode.Weblink:
					dp.SetWebLink(new System.Uri(DataPackageWeblink.Text));
					break;

				case ShareMode.StorageItem:					
					dp.SetStorageItems(filesToShare);
					break;
			}
		}

		private void ShareWeblinkButton_Click_1(object sender, RoutedEventArgs e)
		{
			this.EnsureDataTransferManager();
			this.currentShareMode = ShareMode.Weblink;
			this.dtmHelper.ShowShareUI();
		}

		private async void ShareUnicornButton_Click(object sender, RoutedEventArgs e)
		{
			this.EnsureDataTransferManager();

			if (filesToShare == null)
			{
				filesToShare = new List<IStorageItem>();
				StorageFile unicornFile = await StorageFile.GetFileFromPathAsync(AppDomain.CurrentDomain.BaseDirectory + "Images\\WindowsUnicorn.jpg");
				filesToShare.Add(unicornFile);
			}

			this.currentShareMode = ShareMode.StorageItem;			
			this.dtmHelper.ShowShareUI();			
		}
	}

	public enum ShareMode
	{
		Blank,
		Text,
		Weblink,
		StorageItem
	}
}
