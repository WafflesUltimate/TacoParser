﻿using System;
using System.Runtime.InteropServices;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // If your array.Length is less than 3, something went wrong
            if (cells.Length != 3)
            {
                // Log that and return null
                

                logger.LogWarning("Length of the argument's elements did not meet the required amount. Returning null value");
                // Do not fail if one record parsing fails, return null
                return null; // TODO Implement
            }

            // grab the latitude from your array at index 0
            logger.LogInfo("Parsing latitude");
            var latitude = double.Parse(cells[0]);

            // grab the longitude from your array at index 1
            logger.LogInfo("Parsing longitude");
            var longitude = double.Parse(cells[1]);

            // grab the name from your array at index 2
            logger.LogInfo("Logging name");
            var name = cells[2];

            // Your going to need to parse your string as a `double`
            // which is similar to parsing a string as an `int`

            // You'll need to create a TacoBell class
            // that conforms to ITrackable

            // Then, you'll need an instance of the TacoBell class
            // With the name and point set correctly
            var tacoBellLocation = new TacoBell(name, latitude, longitude);

            //tacoBellLocation.Location.Latitude = latitude;

            // Then, return the instance of your TacoBell class
            // Since it conforms to ITrackable

            return tacoBellLocation;
        }
    }
}