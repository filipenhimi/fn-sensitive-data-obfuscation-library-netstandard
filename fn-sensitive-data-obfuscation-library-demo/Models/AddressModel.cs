using fn_sensitive_data_obfuscation_library_netstandard.Attributes;
using fn_sensitive_data_obfuscation_library_netstandard.Enums;

namespace fn_sensitive_data_obfuscation_library_demo.Models
{
    public class AddressModel
    {
        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Address)]
        public string CompleteAddress { get; private set; }
        public MapModel GeoLocation { get; private set; }

        public AddressModel(string address, MapModel geoLocation)
        {
            CompleteAddress = address;
            GeoLocation = geoLocation;
        }
    }
}
