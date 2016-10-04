//Author: Pat Kozlowski


using Foundation;
using System;
using UIKit;
using Google.Maps;
using CoreGraphics;
using CoreLocation;
using Plugin.ExternalMaps;
using Newtonsoft.Json;
using Parse;
using System.Collections.Generic;

namespace AppleGoogleMapsDemo
{
	public partial class GoogleMapsViewController : UIViewController
	{
		bool firstLocationUpdate;
	
		public GoogleMapsViewController(IntPtr handle) : base(handle)
		{

		}

		//Use async when running parse
		public /*async*/ override void ViewWillAppear(bool animated)
		{

			base.ViewWillAppear(animated);

			//UI Navigation Controller set color
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;

			//Create Camera for coordinates when view loads to Chicago
			var camera = CameraPosition.FromCamera(41.8795845, -87.625902, 12);
			mapView = MapView.FromCamera(this.View.Bounds, camera);
			View.AddSubview(mapView);

			//Set Location Button 
			mapView.Settings.MyLocationButton = true;
			mapView.MyLocationEnabled = true;

			//Move Location Button up to not interfere with tab bar
			mapView.Padding = new UIEdgeInsets(0, 0, 50, 0);

			mapView.AddObserver(this, new NSString("myLocation"), NSKeyValueObservingOptions.New, IntPtr.Zero);

			//Add markers from method below
			AddMuseumMarkers();


			//Navigate to tapped location
			mapView.InfoTapped += (aMapView, aMarker) =>
			{
				CrossExternalMaps.Current.NavigateTo(aMarker.Marker.Title, aMarker.Marker.Position.Latitude, aMarker.Marker.Position.Longitude, Plugin.ExternalMaps.Abstractions.NavigationType.Driving);
			};
		}

		//Use async when running parse
		/*async*/ void AddMuseumMarkers()
		{

			//Populate Museum Markers from Code
			var artInstituteMarker = new Marker()
			{
				Title = "The Art Institute of Chicago",
				Snippet = "111 S Michigan Ave, Chicago, IL 60603",
				Position = new CLLocationCoordinate2D(41.8795845, -87.625902),
				Map = this.mapView
			};
			var fieldMuseumMarker = new Marker()
			{
				Title = "The Field Museum",
				Snippet = "1400 S Lake Shore Dr, Chicago, IL 60605",
				Position = new CLLocationCoordinate2D(41.866261, -87.6191692),
				Map = this.mapView
			};
			var adlerPlanetarium = new Marker()
			{
				Title = "Adler Planetarium",
				Snippet = "Museum Campus, 1300 S Lake Shore Dr, Chicago, IL 60605",
				Position = new CLLocationCoordinate2D(41.866333, -87.6089716),
				Map = this.mapView
			};

			//Populate Museum Markers using JSON.NET and Museum Class.  To run, comment out markers above.

			//var jsonFile = JsonConvert.DeserializeObject<RootObject>(System.IO.File.ReadAllText("Museum.json"));
			//foreach (var museum in jsonFile.Museum)
			//{
			//	var getAll = new Marker()
			//	{
			//		Title = museum.Name,
			//		Snippet = museum.Address,
			//		Position = new CLLocationCoordinate2D(museum.Latitude, museum.Longitude),
			//		Map = this.mapView
			//	};
			//}

			//Populate Museum Markers using Parse Server.  To run, comment out markers above.  You need to create your own instance of Parse Server.  See app delegate.

			//Query class Museum created in Parse Server
			//ParseQuery<ParseObject> queryMuseums = ParseObject.GetQuery("Museum");
			//IEnumerable<ParseObject> Museum = await queryMuseums.FindAsync();
			//foreach (var museum in Museum)
			//{
			//	var getAll = new Marker()
			//	{
			//		Title = museum.Get<string>("Name"),
			//		Snippet = museum.Get<string>("Address"),
			//		Position = new CLLocationCoordinate2D(museum.Get<double>("Latitude"), museum.Get<double>("Longitude")),
			//		Map = this.mapView
			//	};
			//}

		}


		public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (!firstLocationUpdate)
			{
				// If the first location update has not yet been recieved, then jump to that location
				firstLocationUpdate = true;
				//var location = change.ObjectForKey(NSValue.ChangeNewKey) as CLLocation;
				mapView.Camera = CameraPosition.FromCamera(41.8795845, -87.625902, 12);
				//Can jump to current location, but just a static location for demo purposes.
				//mapView.Camera = CameraPosition.FromCamera(location.Coordinate, 8);
			}
		}
	}
}