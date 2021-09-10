using fn_sensitive_data_obfuscation_library_netstandard.Attributes;
using fn_sensitive_data_obfuscation_library_netstandard.Configurations;
using fn_sensitive_data_obfuscation_library_netstandard.Enums;
using fn_sensitive_data_obfuscation_library_netstandard.Exceptions;
using fn_sensitive_data_obfuscation_library_netstandard.Services.Interfaces;
using fn_sensitive_data_obfuscation_library_netstandard.Utility;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace fn_sensitive_data_obfuscation_library_netstandard.Services
{
    public class ObfuscateSensitiveData : IObfuscateSensitiveData
    {
        public object Blur(object data, bool returnJsonObject = true)
        {
            try
            {
                if (ReflectionUtility.IsList(data.GetType()))
                {
                    var list = (IList)data;
                    var itemType = ReflectionUtility.GetCollectionElementType(data.GetType());

                    foreach (var itemList in list)
                    {
                        foreach (var item in itemType.GetProperties())
                        {
                            var obfuscateTypeForSensitiveData = GetCustomAttribute(item);

                            if (IsObjectClass(item)) //class
                            {
                                BlurObjectClass(item, ref data, item.GetValue(itemList));
                            }
                            else //property
                            {
                                var propertyValue = item.GetValue(itemList);
                                var obfuscatedValue = BlurSensitiveData(propertyValue, obfuscateTypeForSensitiveData);
                                item.SetValue(itemList, obfuscatedValue);
                            }
                        }
                    }
                }
                else
                {
                    var fieldsValues = (FieldInfo[])((TypeInfo)data.GetType()).DeclaredFields;

                    int i = 0;
                    foreach (var item in data.GetType().GetProperties())
                    {
                        var obfuscateTypeForSensitiveData = GetCustomAttribute(item);

                        if (IsObjectClass(item)) //class
                        {
                            BlurObjectClass(item, ref data);
                        }
                        else //property
                        {
                            var propertyValue = fieldsValues[i].GetValue(data);
                            var obfuscatedValue = BlurSensitiveData(propertyValue, obfuscateTypeForSensitiveData);
                            item.SetValue(data, obfuscatedValue);
                        }

                        i++;
                    }
                }
            }
            catch(ObfuscateSensitiveDataException exception)
            {
                throw exception;
            }

            return (returnJsonObject) ? JsonConvert.SerializeObject(data) : data;
        }

        private object BlurSensitiveData(object data, ObfuscateTypeForSensitiveData obfuscateTypeForSensitiveData)
        {
            object bluredData = data;
            string dataString = data.ToString();

            try
            {
                switch (obfuscateTypeForSensitiveData)
                {
                    case ObfuscateTypeForSensitiveData.Half:
                        bluredData = HalfBlur(dataString); break;

                    case ObfuscateTypeForSensitiveData.Complete:
                        bluredData = CompleteBlur(dataString); break;

                    case ObfuscateTypeForSensitiveData.CreditCard:
                        bluredData = BlurAtCreditCard(dataString); break;

                    case ObfuscateTypeForSensitiveData.Email:
                        bluredData = BlurAtEmail(dataString); break;

                    case ObfuscateTypeForSensitiveData.PersonalIdentity:
                        bluredData = dataString; break;

                    case ObfuscateTypeForSensitiveData.Address:
                        bluredData = IntercaledBlur(dataString);
                        break;

                    case ObfuscateTypeForSensitiveData.Name:
                        bluredData = BlurAtName(dataString); break;

                    case ObfuscateTypeForSensitiveData.Intercaled:
                        bluredData = IntercaledBlur(dataString);
                        break;
                }
            }
            catch
            {
                bluredData = CompleteBlur(dataString);
            }

            return bluredData;
        }

        private string BlurAtCreditCard(string value)
        {
            return new string(
                Constants.OBFUSCATE_CHAR_FOR_STRING_FIELDS, value.Length - 4) +
                value.Substring(value.Length - 4);
        }

        private string BlurAtEmail(string value)
        {
            int positionChar = value.IndexOf("@");
            return new string(Constants.OBFUSCATE_CHAR_FOR_STRING_FIELDS, positionChar) +
                value.Substring(positionChar);
        }

        private string BlurAtName(string value)
        {
            string[] part = value.Split(" ");
            string bluredData = part[0];

            for (int i = 1; i < part.Length; i++)
                bluredData += " " + new string(Constants.OBFUSCATE_CHAR_FOR_STRING_FIELDS, part[i].Length);

            return bluredData;
        }

        private string HalfBlur(string value)
        {
            return new string(
                Constants.OBFUSCATE_CHAR_FOR_STRING_FIELDS, value.Length / 2) +
                value.Substring(value.Length / 2);
        }

        private string CompleteBlur(string value)
        {
            return new string(Constants.OBFUSCATE_CHAR_FOR_STRING_FIELDS, value.Length);
        }

        private string IntercaledBlur(string value)
        {
            string bluredData = string.Empty;
            string[] part = value.Split(" ");
            bool aux = false;
            for (int i = 0; i < part.Length; i++)
            {
                if (!aux)
                {
                    bluredData += part[i];
                    aux = true;
                }
                else
                {
                    bluredData += new string(Constants.OBFUSCATE_CHAR_FOR_STRING_FIELDS, part[i].Length);
                    aux = false;
                }
            }
            return bluredData;
        }

        private void BlurObjectClass(PropertyInfo propertyInfo, ref object data, object propertyInfoWithValues = null)
        {
            var fieldsValues = (propertyInfoWithValues == null) ? propertyInfo.GetValue(data) : propertyInfoWithValues;

            if (ReflectionUtility.IsList(fieldsValues.GetType()))
            {
                var list = (IList)propertyInfo.GetValue(data);
                var itemType = ReflectionUtility.GetCollectionElementType(fieldsValues.GetType());

                foreach (var itemList in list)
                {
                    foreach (var item in itemType.GetProperties())
                    {
                        var obfuscateTypeForSensitiveData = GetCustomAttribute(item);

                        if (IsObjectClass(item)) //class
                        {
                            BlurObjectClass(item, ref data, item.GetValue(itemList));
                        }
                        else //property
                        {
                            var propertyValue = item.GetValue(itemList);
                            var obfuscatedValue = BlurSensitiveData(propertyValue, obfuscateTypeForSensitiveData);
                            item.SetValue(itemList, obfuscatedValue);
                        }
                    }
                }
            }     
            else
            {
                foreach (var item in fieldsValues.GetType().GetProperties())
                {
                    if (IsObjectClass(item))
                    {
                        BlurObjectClass(item, ref fieldsValues);
                    }
                    else
                    {
                        var obfuscateTypeForSensitiveData = GetCustomAttribute(item);
                        var propertyValue = item.GetValue(fieldsValues);
                        var obfuscatedValue = BlurSensitiveData(propertyValue, obfuscateTypeForSensitiveData);
                        item.SetValue(fieldsValues, obfuscatedValue);
                    }
                }
            }
        }

        private bool IsObjectClass(PropertyInfo propertyInfo)
        {
            return (propertyInfo.PropertyType.IsNested || !propertyInfo.PropertyType.IsSealed);
        }

        private ObfuscateTypeForSensitiveData GetCustomAttribute(PropertyInfo propertyInfo)
        {
            var obfuscateTypeForSensitiveData = ObfuscateTypeForSensitiveData.None;

            var customAttribute =
                ((ReadOnlyCollection<CustomAttributeData>)
                propertyInfo.CustomAttributes).ToList();

            if (customAttribute.Any())
            {
                var attr = customAttribute
                    .Where(x => x.AttributeType.Name == typeof(ObfuscateSensitiveDataAttribute).Name);

                if (attr.Any())
                {
                    obfuscateTypeForSensitiveData = (ObfuscateTypeForSensitiveData)attr.FirstOrDefault().ConstructorArguments.FirstOrDefault().Value;
                }
            }

            return obfuscateTypeForSensitiveData;
        }
    }
}