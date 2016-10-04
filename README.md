# Xamarin.iOSAppleGoogleMaps
Xamarin.iOS 10 project to see side-to-side the differences between Apple Maps &amp; Google Maps.

Ingested data three ways:  Code, JSON File, and using Parse Server (You need to configure your own Parse Server).

Be sure to re-add the Xamarin.Google.iOS.Maps, Xam.Plugin.ExternalMaps Packages, Parse, and Newtonsoft.JSON if the solution doesn't restore the packages successfully.

When selecting an annotation, this will navigate to that location using the External Maps plugin.

Use XCode 8 Interface builder if you need to tweak the GUI.

Using Parse Server, create your own Parse Server instance using https://github.com/ParsePlatform/parse-server-example
(I used Heroku, and it's free in Dev environments).

Create a "Museum" class and create "Name", "Address", "Latitude", and "Longitude" columns, and input row information for each one.


![alt tag](https://cloud.githubusercontent.com/assets/16422288/19059102/be25b570-89a2-11e6-99f5-5f3be3c97296.png)

![alt tag](https://cloud.githubusercontent.com/assets/16422288/19059125/031e0740-89a3-11e6-9991-b5c14bbda539.png)

![alt tag](https://cloud.githubusercontent.com/assets/16422288/19061640/0b4f224a-89b8-11e6-9854-b50788acbddf.png)




