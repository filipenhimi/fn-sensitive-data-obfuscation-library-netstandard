using fn_sensitive_data_obfuscation_library_netstandard.Enums;
using System;

namespace fn_sensitive_data_obfuscation_library_netstandard.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ObfuscateSensitiveDataAttribute : Attribute
    {
        private readonly ObfuscateTypeForSensitiveData obfuscateTypeForSensitiveData;

        public ObfuscateSensitiveDataAttribute(ObfuscateTypeForSensitiveData obfuscateTypeForSensitiveData)
        {
            this.obfuscateTypeForSensitiveData = obfuscateTypeForSensitiveData;
        }

        public virtual ObfuscateTypeForSensitiveData ObfuscateType
        {
            get { return obfuscateTypeForSensitiveData; }
        }
    }
}
