namespace BackendTask1.Services.Region
{
    using System.Collections.Generic;
    using System.Linq;
    using BackendTask1.Dtos;
    using BackendTask1.EntityModels;
    using BackendTask1.Interfaces.Country;
    using Castle.MicroKernel;
    using Castle.Windsor;

    public class RegionService : IService
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

        public void Save<T>(List<T> entities) where T : Entity
        {
            var saveEntityList = entities as List<RegionEntity>;
            if (saveEntityList == null)
            {
                throw new System.Exception("Can't save empty entity");
            }

            using (var context = new CountryContext())
            {
                foreach (var saveEntity in saveEntityList)
                {
                    context.Regions.Add(saveEntity);
                }

                context.SaveChanges();
            }
        }

        public void Save<T>(T entity) where T : Entity
        {
            var saveEntity = entity as RegionEntity;
            if (saveEntity == null)
            {
                throw new System.Exception("Can't save empty entity");
            }

            using (var context = new CountryContext())
            {
                context.Regions.Add(saveEntity);
                context.SaveChanges();
            }
        }
    }
}
