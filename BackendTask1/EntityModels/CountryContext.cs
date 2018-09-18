namespace BackendTask1.EntityModels
{
    using System.Data.Entity;

    public class CountryContext : DbContext
    {
        public virtual DbSet<CountryEntity> Countries { get; set; }

        public virtual DbSet<RegionEntity> Regions { get; set; }
    }
}
