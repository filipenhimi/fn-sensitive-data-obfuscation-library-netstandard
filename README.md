# FN - Sensitive Data Obfuscation Library
This is a library for obfuscate sensitive datas in .Net Framework and .Net Core applications.

> In order to guarantee the conformity in protect personal data, you can use this library to obfuscate sensitive datas in your applications. More precisilly you can improve you solution about this subject. Choice the type of data and blur it in JSON, Classes and Variables objects. 

<img src="docs\diagram.png" alt="diagram">

<hr />


## 💻 Requirements

Before starting, if you meet the following requirements:

  You have installed the latest version of `<Visual Studio or VS Code>` with  `<NETStandard.Library>`
  

## ☕ Using

Configure your classes with ObfuscateSensitiveData Attribute for each property that you want to apply the obfuscation. Choice the type of sensitive data.

```c#
    public class CustomerModel
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
```

Create an instance of <b><i>ObfuscateSensitiveData</i></b> class and invoke the Blur method passing the object of class.
```c#
        private static void JsonWithSensitiveDataTest()
        {
            var _obfuscateSensitiveData = new ObfuscateSensitiveData();

            var map = new MapModel(27.0156552m, -86.4304592m);
            var address = new AddressModel("3860 NE 39th Street, Redmond 98052, WA", map);
            var customer = new CustomerModel("William Henry Gates", "billgates@microsoft.com", address);
            var billing = new BillingModel("1434546734568765");
            var contract = new ContractDataModel("Filipe Tório Lopes Ruas Nhimi", customer, billing);

            var json = _obfuscateSensitiveData.Blur(contract);
            
            Console.WriteLine(json);
        }
```

This is the result in json object with sensitive datas obufuscated.
```
{
   "Salesman":"Filipe ***** ***** **** *****",
   "Customer":{
      "Name":"William ***** *****",
      "Email":"*********@microsoft.com",
      "Address":{
         "CompleteAddress":"3860**39th*******Redmond******WA",
         "GeoLocation":{
            "Latitude":27.0156552,
            "Longitude":-86.4304592
         }
      }
   },
   "Billing":{
      "CreditCardNumber":"************8765"
   },
   "Date":"2021-09-09T19:46:59.9246481-03:00"
}
```
> This library is avaliable like as a Nuget Package and you can installed it using the following .<b>NET CLI</b> command:
```
dotnet add package fn-sensitive-data-obfuscation-library-netstandard --version 1.0.0
```

## 📫 Contributing to Sensitive Data Obfuscation Library

1. Fork this repository.
2. Create a branch: `git checkout -b <branch_name>`.
3. Make your changes and confirm them: `git commit -m '<commit_message>'`
4. Send to original branch: `git push origin <project_name> / <local>`
5. Create pull request.

## 😄 Be one of the contributors<br>

Do you want to be part of this project? [HERE](CONTRIBUTING.md) and read how to contribute.

## 📝 License

This project is under license. See the  [LICENSE](LICENSE.md) file for more detais.

[⬆ Back to top](#nome-do-projeto)<br>