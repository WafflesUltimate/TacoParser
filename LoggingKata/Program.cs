using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {

        

        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0) { logger.LogError("no lines", new ArgumentNullException()); }
            else if (lines.Length == 1) { logger.LogWarning("Only one argument passed."); }

            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();
            logger.LogInfo("Locations Generated from data...");

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable start = null;
            ITrackable destination = null;
            double testDistance = 0;
            double finalDistance = 0;
            var locA = new GeoCoordinate();
            var locB = new GeoCoordinate();

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`]
            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            logger.LogInfo($"Searching Locations for longest distance between two locations.");
            for (int i = 0; i < locations.Length; i++)
            {
                locA.Latitude = locations[i].Location.Latitude;
                locA.Longitude = locations[i].Location.Longitude;

                // Now, do another loop on the locations within the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int h = 1; h < locations.Length; h++)
                {
                    locB.Latitude = locations[h].Location.Latitude;
                    locB.Longitude = locations[h].Location.Longitude;

                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    testDistance = locA.GetDistanceTo(locB);

                        
                    // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

                    if (finalDistance < testDistance)
                    {
                        finalDistance = testDistance;
                        logger.LogInfo($"New Longest distance is between location {i + 1} and location {h + 1}.");
                        logger.LogInfo($"New Longest distance is {finalDistance}");
                        // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
                        start = locations[i];
                        destination = locations[h];
                    }
                }
            }
            logger.LogInfo($"The Longest distance between two Two Taco Bell's is: {finalDistance}");
        }
    }
}
