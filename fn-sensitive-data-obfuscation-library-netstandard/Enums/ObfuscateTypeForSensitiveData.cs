namespace fn_sensitive_data_obfuscation_library_netstandard.Enums
{
    public enum ObfuscateTypeForSensitiveData
    {
        None = 0,
        Half = 1,
        Complete = 2,
        Name = 3,
        Address = 4,
        PersonalIdentity = 5,
        Email = 6,
        CreditCard = 7,
        Intercaled = 8
    }
}
