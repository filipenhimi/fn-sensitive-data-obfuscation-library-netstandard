using fn_obfuscate_sensitive_data_library_test.Models;
using fn_sensitive_data_obfuscation_library_netstandard.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace fn_sensitive_data_obfuscation_library_test
{
    public class ObfuscateSensitiveDataTest
    {
        private readonly ObfuscateSensitiveData _obfuscateSensitiveData = new();

        private static CustomerModel GetCustomerModel()
        {
            var name = "Filipe Tório Lopes Ruas Nhimi";
            var email = "filipenhimi@gmail.com";
            var address = "Av Amazonas, 999999 - Centro - Belo Horizonte - Minas Gerais";

            var contacts = new List<ContactList>()
            {
                new ContactList("Bill Gates", "5531999999999"),
                new ContactList("Steve Jobs", "5511999998888"),
                new ContactList("Linus Torvalds", "5532999997777")
            };

            return new CustomerModel(name, email, address, contacts);
        }

        [Fact]
        public void BlurSensitiveDataAndConvertClassToJson()
        {
            //Act
            var json = _obfuscateSensitiveData.Blur(GetCustomerModel()).ToString();

            //Assert
            Assert.Contains("Filipe ***** ***** **** *****", json);
            Assert.Contains("***********@gmail.com", json);
            Assert.Contains("Av*********999999*Centro*Belo*********-*****Gerais", json);
            Assert.Contains("Bill *****", json);
            Assert.Contains("******9999999", json);
            Assert.Contains("Steve ****", json);
            Assert.Contains("******9998888", json);
            Assert.Contains("Linus ********", json);
            Assert.Contains("******9997777", json);
        }

        [Fact]
        public void BlurSensitiveData()
        {
            //Act
            var customer = (CustomerModel) _obfuscateSensitiveData.Blur(GetCustomerModel(), false);

            //Assert
            Assert.Equal("Filipe ***** ***** **** *****", customer.Name);
            Assert.Contains("***********@gmail.com", customer.Email);
            Assert.Contains("Av*********999999*Centro*Belo*********-*****Gerais", customer.Address);
            Assert.True(customer.Contacts.Where(x=>x.Name == "Bill *****").Any());
            Assert.True(customer.Contacts.Where(x => x.Phone == "******9999999").Any());
            Assert.True(customer.Contacts.Where(x => x.Name == "Steve ****").Any());
            Assert.True(customer.Contacts.Where(x => x.Phone == "******9998888").Any());
            Assert.True(customer.Contacts.Where(x => x.Name == "Linus ********").Any());
            Assert.True(customer.Contacts.Where(x => x.Phone == "******9997777").Any());
        }
    }
}