//Author: Pat Kozlowski


using Foundation;
using System;
using UIKit;
using CoreLocation;
using MapKit;
using Plugin.ExternalMaps;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace AppleGoogleMapsDemo
{
    public partial class AppleMapsViewController : UIViewController
    {
		CLLocationManager locationManager = new CLLocationManager();

        public AppleMapsViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//Request Location
			locationManager.RequestAlwaysAuthorization();
			//Set Navigation Bar Color
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			//Show UserLocation
			mapView.ShowsUserLocation = true;

			//Sets zoom level
			MKCoordinateRegion region;
			region.Center.Latitude = 41.8795845;
			region.Center.Longitude = -87.625902;
			region.Span.LatitudeDelta = .3;
			region.Span.LongitudeDelta = .3;
			mapView.Region = region;
			//Sets MapviewDelegate to this ViewController
			mapView.Delegate = new MapViewDelegate(this);

			//Add the annotations
			//TO DO:  Populate from backend i.e. from Parse Server 
			mapView.AddAnnotation(new PinAnnotation(new CLLocationCoordinate2D(41.8795845, -87.625902),"The Art Institute of Chicago","111 S Michigan Ave, Chicago, IL 60603"));
 			mapView.AddAnnotation(new PinAnnotation(new CLLocationCoordinate2D(41.866261, -87.6191692),"The Field Museum","1400 S Lake Shore Dr, Chicago, IL 60605"));
			mapView.AddAnnotation(new PinAnnotation(new CLLocationCoordinate2D(41.866333, -87.6089716),"Adler Planetarium","Museum Campus, 1300 S Lake Shore Dr, Chicago, IL 60605"));

			//Populate Museum Markers using JSON.NET and Museum Class.  To run, comment out markers above.

			//var jsonFile = JsonConvert.DeserializeObject<RootObject> (System.IO.File.ReadAllText ("Museum.json"));
			//foreach (var museum in jsonFile.Museum)
			//{
			//	mapView.AddAnnotation(new PinAnnotation(new CLLocationCoordinate2D(museum.Latitude, museum.Longitude), museum.Name, museum.Address));
			//}

			centerBarButton.Clicked += (sender, e) =>
			{
				//Animates to center based on current location
				CLLocationCoordinate2D coords = mapView.UserLocation.Coordinate;
				MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(3), MilesToLongitudeDegrees(3, coords.Latitude));
				var regionCenter = new MKCoordinateRegion(coords, span);
				mapView.SetRegion(regionCenter,true);
			};;
		}

		//Helper Methods for center button
		public double MilesToLatitudeDegrees(double miles)
		{
			double earthRadius = 3960.0;
			double radiansToDegrees = 180.0/Math.PI;
			return (miles/earthRadius) * radiansToDegrees;
		}

		public double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			double earthRadius = 3960.0;
			double degreesToRadians = Math.PI / 180.0;
			double radiansToDegrees = 180.0 / Math.PI;
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
			return (miles / radiusAtLatitude) * radiansToDegrees;
		}
				
}
		class MapViewDelegate : MKMapViewDelegate
		{
			const string Identifier = "MuseumAnnotation";
			public AppleMapsViewController mapviewController;
			public MapViewDelegate(AppleMapsViewController mapviewController )
			{
				//Set Mapviewdelegate to this viewcontroller
				this.mapviewController = mapviewController;
			}
			public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
			{

				//get the view for the annotation
				CLLocationCoordinate2D currentLocation = mapView.UserLocation.Coordinate;
				CLLocationCoordinate2D annotationLocation = annotation.Coordinate;
				if (annotation is MKUserLocation)
					return null;

				if (currentLocation.Latitude == annotationLocation.Latitude && currentLocation.Longitude == annotationLocation.Longitude)
					return null;

				var annotationView = mapView.DequeueReusableAnnotation(Identifier) as MKPinAnnotationView;
				if (annotationView == null)
				{
					annotationView = new MKPinAnnotationView(annotation, Identifier);
				}

				annotationView.PinColor = MKPinAnnotationColor.Red;
				annotationView.CanShowCallout = true;
				annotationView.Draggable = false;
				annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);

				return annotationView;
			}


			public override void CalloutAccessoryControlTapped (MKMapView mapView, MKAnnotationView view, UIControl control)
			{
					//Get Title, Lat, and Long from PinAnnotation and send to CrossExternal Maps Plugin
					var getCoords = view.Annotation as PinAnnotation;
					//Navigate to Tapped Location
					CrossExternalMaps.Current.NavigateTo(getCoords.Title, getCoords.Coordinate.Latitude, getCoords.Coordinate.Longitude, Plugin.ExternalMaps.Abstractions.NavigationType.Driving);

			}

	}
		

	//create custom class to hold values, can pass in other information about the values as well such as phone, website, etc.
	 class PinAnnotation : MKAnnotation
	{
		string title, subtitle;
		public override CLLocationCoordinate2D Coordinate { get { return this.Coords; } }
		public CLLocationCoordinate2D Coords;
		public override string Title { get { return title; } }
		public override string Subtitle { get { return subtitle; } }
		public PinAnnotation(CLLocationCoordinate2D coordinate, string title, string subtitle)
		{
			this.Coords = coordinate;
			this.title = title;
			this.subtitle = subtitle;
		}
	}



}