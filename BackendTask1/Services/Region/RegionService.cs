namespace BackendTask1.Services.Region
{
    using System.Collections.Generic;
    using System.Linq;
    using BackendTask1.Dtos;
    using BackendTask1.EntityModels;
    using BackendTask1.Interfaces.Country;
    using Castle.MicroKernel;
    using Castle.Windsor;

    public class RegionService : ICountryService
    {
        public string GetData(List<CountryDto> countries)
        {
            var regions = countries.Select(x => x.IncomeLevel.Value)
                .Concat(countries.Select(x => x.Region.Value))
                .Concat(countries.Select(x => x.Adminregion.Value))
                .Concat(countries.Select(x => x.LendingType.Value))
                .Distinct()
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            return string.Join("\n", regions);
        }

        public int GetMaxValue()
        {
            using (var context = new CountryContext())
            {
                return context.Regions.Max(x => x.Value.Length);
            }
        }

        public void Save(List<CountryDto> countries)
        {
            var regions = countries.Select(x => x.IncomeLevel.Value)
                .Concat(countries.Select(x => x.Region.Value))
                .Concat(countries.Select(x => x.Adminregion.Value))
                .Concat(countries.Select(x => x.LendingType.Value))
                .Distinct()
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            using (var context = new CountryContext())
            {
                foreach (var region in regions)
                {
                    var newRegion = new RegionEntity
                    {
                        Value = region
                    };

                    context.Regions.Add(newRegion);
                }

                context.SaveChanges();
            }
        }
    }
}
