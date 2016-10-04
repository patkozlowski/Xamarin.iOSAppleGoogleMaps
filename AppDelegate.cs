//Author: Pat Kozlowski

using Foundation;
using UIKit;
using Google.Maps;
using Parse;

namespace AppleGoogleMapsDemo
{
	
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		const string MapsApiKey = "AIzaSyDzESNp7zn6ccLB_64rFXCbptV-ZoRSgxI";


		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			MapServices.ProvideAPIKey(MapsApiKey);

			//Uncomment and place in your own parse information here.
			//ParseClient.Initialize(new ParseClient.Configuration
			//{
			//	ApplicationId = "PLACEAPPIDHERE",
			//	Server = "https://PLACESERVERURLHERE/parse/",
			//});
			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			
		}

		public override void DidEnterBackground(UIApplication application)
		{
			
		}

		public override void WillEnterForeground(UIApplication application)
		{
			
		}

		public override void OnActivated(UIApplication application)
		{

		}

		public override void WillTerminate(UIApplication application)
		{
			
		}
	}
}


