using fn_sensitive_data_obfuscation_library_demo.Models;
using fn_sensitive_data_obfuscation_library_netstandard.Services;
using System;
using System.Collections.Generic;

namespace fn_sensitive_data_obfuscation_library_demo
{
    class Program
    {
        private static ObfuscateSensitiveData _obfuscateSensitiveData;
        private const string separator = "----------------------------------------------------------------------------------------------";

        static void Main()
        {
            Console.WriteLine(separator);
            Console.WriteLine("Welcome to FN - Sensitive Data Obfuscation Library");
            Console.WriteLine(separator);

            _obfuscateSensitiveData = new ObfuscateSensitiveData();

            JsonWithSensitiveDataTest();
            JsonWithListOfSensitiveDataTest();
            ObjectClassWithSensitiveDataTest();
        }

        private static void JsonWithSensitiveDataTest()
        {
            var map = new MapModel(27.0156552m, -86.4304592m);
            var address = new AddressModel("3860 NE 39th Street, Redmond 98052, WA", map);
            var customer = new CustomerModel("William Henry Gates", "billgates@microsoft.com", address);
            var billing = new BillingModel("1434546734568765");
            var contract = new ContractDataModel("Filipe Tório Lopes Ruas Nhimi", customer, billing);

            var json = _obfuscateSensitiveData.Blur(contract);
            
            Console.WriteLine(json);
            Console.WriteLine(separator);
        }

        private static void JsonWithListOfSensitiveDataTest()
        {
            var map = new MapModel(27.0156552m, -86.4304592m);
            var address = new AddressModel("3860 NE 39th Street, Redmond 98052, WA", map);
            var customer = new CustomerModel("William Henry Gates", "billgates@microsoft.com", address);

            var map2 = new MapModel(23.0156552m, -83.4304592m);
            var address2 = new AddressModel("Av Amazonas, 999999 - Centro - Belo Horizonte - Minas Gerais", map2);
            var customer2 = new CustomerModel("Filipe Tório Lopes Ruas Nhimi", "filipenhimi@gmail.com", address2);

            var map3 = new MapModel(23.1116552m, -83.4307772m);
            var address3 = new AddressModel("Rua José da Silva, 999999 - Centro - Contagem - Minas Gerais", map3);
            var customer3 = new CustomerModel("Nikola Tesla", "tesla@gmail.com", address3);

            var customerList = new List<CustomerModel>
            {
                customer,
                customer2,
                customer3
            };

            var customerListModel = new CustomerListModel
            {
                CustomerList = customerList
            };

            var json = _obfuscateSensitiveData.Blur(customerListModel);

            Console.WriteLine(json);
            Console.WriteLine(separator);
        }

        private static void ObjectClassWithSensitiveDataTest()
        {
            var map = new MapModel(27.0156552m, -86.4304592m);
            var address = new AddressModel("Av Comunicações, 4 - Santa Fé - (6278030) Osasco, São Paulo", map);
            var customer = new CustomerModel("Senor Abravanel", "silvio.santos@sbt.com", address);
            var billing = new BillingModel("1634121334568765");
            var contract = new ContractDataModel("Ricardo da Silva", customer, billing);

            var objectClass = (ContractDataModel) _obfuscateSensitiveData.Blur(contract, false);

            Console.WriteLine(
                objectClass.Customer.Name + " | " + 
                objectClass.Customer.Email + " | " + 
                objectClass.Customer.Address.CompleteAddress + " | " + 
                objectClass.Billing.CreditCardNumber);
        }
    }
}