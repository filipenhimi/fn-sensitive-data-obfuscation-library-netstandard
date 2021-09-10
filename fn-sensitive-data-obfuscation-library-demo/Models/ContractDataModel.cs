using fn_sensitive_data_obfuscation_library_netstandard.Attributes;
using fn_sensitive_data_obfuscation_library_netstandard.Enums;
using System;

namespace fn_sensitive_data_obfuscation_library_demo.Models
{
    internal class ContractDataModel
    {
        [ObfuscateSensitiveData(ObfuscateTypeForSensitiveData.Name)]
        public string Salesman { get; private set; }
        public CustomerModel Customer { get; private set; }
        public BillingModel Billing { get; private set; }
        public DateTime Date { get; private set; }

        public ContractDataModel(string salesman, CustomerModel customer, BillingModel billing)
        {
            Salesman = salesman;
            Customer = customer;
            Billing = billing;
            Date = DateTime.Now;
        }
    }
}