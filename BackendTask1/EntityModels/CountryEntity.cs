namespace BackendTask1.EntityModels
{
    public class CountryEntity : Entity
    {
        public string Iso2Code { get; set; }

        public string Name { get; set; }

        public RegionEntity Region { get; set; }

        public RegionEntity Adminregion { get; set; }

        public RegionEntity IncomeLevel { get; set; }

        public RegionEntity LendingType { get; set; }

        public string CapitalCity { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }
    }
}