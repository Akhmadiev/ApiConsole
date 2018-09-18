namespace BackendTask1.Services.Country
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BackendTask1.Dtos;
    using BackendTask1.EntityModels;
    using BackendTask1.Interfaces.Country;
    using Castle.MicroKernel;

    public class CountryService : ICountryService
    {
        public string GetData(List<CountryDto> countries)
        {
            return string.Join("\n", countries.Select(x => x.Name).ToList());
        }

        public int GetMaxValue()
        {
            using (var context = new CountryContext())
            {
                return context.Countries.Max(x => x.Name.Length);
            }
        }

        public void Save(List<CountryDto> countries)
        {
            using (var context = new CountryContext())
            {
                var regions = countries.Select(x => x.Region.Value);
                var incomeLevels = countries.Select(x => x.IncomeLevel.Value);
                var adminregions = countries.Select(x => x.Adminregion.Value);
                var lendingTypes = countries.Select(x => x.LendingType.Value);

                var values = regions.Concat(incomeLevels.Concat(adminregions.Concat(lendingTypes))).Distinct().ToList();

                var allRegions = context.Regions
                    .Where(x => values.Contains(x.Value))
                    .ToList();

                foreach (var country in countries)
                {
                    var adminregion = allRegions
                        .FirstOrDefault(x => x.Value == country.Adminregion.Value);

                    var incomeLevel = allRegions
                        .FirstOrDefault(x => x.Value == country.IncomeLevel.Value);

                    var region = allRegions
                        .FirstOrDefault(x => x.Value == country.Region.Value);

                    var lendingType = allRegions
                        .FirstOrDefault(x => x.Value == country.LendingType.Value);

                    var newCountry = new CountryEntity
                    {
                        Adminregion = adminregion,
                        IncomeLevel = incomeLevel,
                        Region = region,
                        LendingType = lendingType,
                        CapitalCity = country.CapitalCity,
                        Iso2Code = country.Iso2Code,
                        Latitude = country.Latitude,
                        Longitude = country.Longitude,
                        Name = country.Name
                    };

                    context.Countries.Add(newCountry);
                }

                context.SaveChanges();
            }
        }
    }
}
