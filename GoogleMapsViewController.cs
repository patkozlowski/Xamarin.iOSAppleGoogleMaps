//Author: Pat Kozlowski


using Foundation;
using System;
using UIKit;
using Google.Maps;
using CoreGraphics;
using CoreLocation;
using Plugin.ExternalMaps;

namespace AppleGoogleMapsDemo
{
	public partial class GoogleMapsViewController : UIViewController
	{
		bool firstLocationUpdate;
	
		public GoogleMapsViewController(IntPtr handle) : base(handle)
		{

		}
		public override void ViewWillAppear(bool animated)
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


		void AddMuseumMarkers()
		{
			//TO DO:  Populate from JSON file OR backend i.e. from Parse Server 

			//Populate Museum Markers  
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