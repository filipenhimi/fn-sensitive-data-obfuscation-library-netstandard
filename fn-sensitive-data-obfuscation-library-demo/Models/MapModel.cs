namespace fn_sensitive_data_obfuscation_library_demo.Models
{
    public class MapModel
    {
        public decimal Latitude { get; private set; }

        public decimal Longitude { get; private set; }

        public MapModel(decimal latitude, decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}