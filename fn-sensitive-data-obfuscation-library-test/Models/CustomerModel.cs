using fn_sensitive_data_obfuscation_library_netstandard.Attributes;
using fn_sensitive_data_obfuscation_library_netstandard.Enums;
using System.Collections.Generic;

namespace fn_obfuscate_sensitive_data_library_test.Models
{
    public class CustomerModel
    {
        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Name)]
        public string Name { get; private set; }

        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Email)]
        public string Email { get; private set; }

        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Address)]
        public string Address { get; private set; }

        public List<ContactList> Contacts { get; private set; }

        public CustomerModel(string name, string email, string address, List<ContactList> contacts)
        {
            Name = name;
            Email = email;
            Address = address;
            Contacts = contacts;
        }
    }

    public class ContactList
    {
        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Name)]
        public string Name { get; private set; }

        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Half)]
        public string Phone { get; private set; }

        public ContactList(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
}