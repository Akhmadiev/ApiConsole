namespace BackendTask1.Services.Region
{
    using System.Collections.Generic;
    using System.Linq;
    using BackendTask1.Dtos;
    using BackendTask1.EntityModels;
    using BackendTask1.Interfaces.Country;

    public class RegionService : IService
    {
        public List<T> ParsingResult<T, T2>(List<T2> data) where T : Entity where T2 : class
        {
            var parsedData = data as List<CountryDto>;

            if (parsedData == null)
            {
                throw new System.Exception("Can't parse data");
            }

            var regions = parsedData.Select(x => x.IncomeLevel.Value)
                .Concat(parsedData.Select(x => x.Region.Value))
                .Concat(parsedData.Select(x => x.Adminregion.Value))
                .Concat(parsedData.Select(x => x.LendingType.Value))
                .Distinct()
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => new RegionEntity
                {
                    Value = x
                })
                .ToList();

            return regions as List<T>;
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
