using System;
using System.Collections.Generic;


namespace AppleGoogleMapsDemo
{

	public class Museum
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}

	public class RootObject
	{
		public List<Museum> Museum { get; set; }
	}

}
