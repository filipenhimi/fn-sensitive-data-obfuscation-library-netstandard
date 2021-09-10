using System;

namespace fn_sensitive_data_obfuscation_library_netstandard.Exceptions
{
    [Serializable]
    public class ObfuscateSensitiveDataException : Exception
    {
        public ObfuscateSensitiveDataException(string message) : base(message) { }
    }
}
