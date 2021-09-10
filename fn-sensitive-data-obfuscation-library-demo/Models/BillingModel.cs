using fn_sensitive_data_obfuscation_library_netstandard.Attributes;
using fn_sensitive_data_obfuscation_library_netstandard.Enums;

namespace fn_sensitive_data_obfuscation_library_demo.Models
{
    internal class BillingModel
    {
        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.CreditCard)]
        public string CreditCardNumber { get; private set; }

        public BillingModel(string creditCardNumber)
        {
            CreditCardNumber = creditCardNumber;
        }
    }
}
