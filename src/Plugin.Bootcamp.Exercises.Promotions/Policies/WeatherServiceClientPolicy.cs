using Sitecore.Commerce.Core;


namespace Plugin.Bootcamp.Exercises.Promotions
{
    class WeatherServiceClientPolicy : Policy
    {
        /* Student: Create a property to store the API key.
         * Create a constructor to initialize it to an empty
         * string. */
        public string ApplicationId { get; set; }

        public WeatherServiceClientPolicy()
        {
            //this.ApplicationId = "73e682b26deb2811f4a6e9b4dc7a3521";
            this.ApplicationId = string.Empty;
        }
    }
}
