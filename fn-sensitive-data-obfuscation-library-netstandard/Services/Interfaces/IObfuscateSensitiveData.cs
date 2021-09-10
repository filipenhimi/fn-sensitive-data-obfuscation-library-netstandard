namespace fn_sensitive_data_obfuscation_library_netstandard.Services.Interfaces
{
    /// <summary>
    /// Interface for use FN Obfuscate Sensitive Data Library
    /// </summary>
    public interface IObfuscateSensitiveData
    {
        /// <summary>
        /// Blur sensitive datas and return a json object or an object
        /// </summary>
        /// <param name="data">class object with attributes configured for obfuscate sensitive data type</param>
        /// <param name="returnJsonObject">indicate if must generate object json</param>
        /// <returns></returns>
        object Blur(object data, bool returnJsonObject = true);
    }
}