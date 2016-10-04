// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AppleGoogleMapsDemo
{
    [Register ("AppleMapsViewController")]
    partial class AppleMapsViewController
    {
        [Outlet]
        UIKit.UIBarButtonItem centerBarButton { get; set; }


        [Outlet]
        MapKit.MKMapView mapView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (centerBarButton != null) {
                centerBarButton.Dispose ();
                centerBarButton = null;
            }

            if (mapView != null) {
                mapView.Dispose ();
                mapView = null;
            }
        }
    }
}