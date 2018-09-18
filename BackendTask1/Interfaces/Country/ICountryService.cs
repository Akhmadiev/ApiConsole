namespace BackendTask1.Interfaces.Country
{
    using BackendTask1.Dtos;
    using Castle.MicroKernel.Registration;
    using System.Collections.Generic;

    /// <summary>
    /// Country's interface
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Save entites
        /// </summary>
        void Save(List<CountryDto> countries);

        /// <summary>
        /// Get max value
        /// </summary>
        int GetMaxValue();

        /// <summary>
        /// Get data
        /// </summary>
        string GetData(List<CountryDto> countries);
    }
}
