using fn_sensitive_data_obfuscation_library_netstandard.Attributes;
using fn_sensitive_data_obfuscation_library_netstandard.Enums;

namespace fn_sensitive_data_obfuscation_library_demo.Models
{
    internal class CustomerModel
    {
        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Name)]
        public string Name { get; private set; }

        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Email)]
        public string Email { get; private set; }

        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Address)]
        public AddressModel Address { get; private set; }

        public CustomerModel(string name, string email, AddressModel address)
        {
            Name = name;
            Email = email;
            Address = address;
        }
    }
}