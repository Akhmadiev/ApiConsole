namespace BackendTask1.Dtos
{
    public class CountryDto
    {
        public string Iso2Code { get; set; }

        public string Name { get; set; }

        public RegionDto Region { get; set; }

        public RegionDto Adminregion { get; set; }

        public RegionDto IncomeLevel { get; set; }

        public RegionDto LendingType { get; set; }

        public string CapitalCity { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }
    }
}
