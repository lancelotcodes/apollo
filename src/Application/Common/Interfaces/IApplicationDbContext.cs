using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.Logs;
using apollo.Domain.Entities.References;
using apollo.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        #region Core Entities
        DbSet<Appraisal> Appraisals { get; set; }

        DbSet<Building> Buildings { get; set; }

        DbSet<Company> Companies { get; set; }

        DbSet<Contact> Contacts { get; set; }

        DbSet<Contract> Contracts { get; set; }

        DbSet<Floor> Floors { get; set; }

        DbSet<Industrial> IndustrialListings { get; set; }

        DbSet<Investment> Investments { get; set; }

        DbSet<Lead> Leads { get; set; }

        DbSet<Mandate> Mandates { get; set; }

        DbSet<Property> Properties { get; set; }

        DbSet<PropertyDocument> PropertyDocuments { get; set; }

        DbSet<PropertyImageVersion> PropertyImageVersions { get; set; }

        DbSet<ResidentialListing> ResidentialListings { get; set; }

        DbSet<Unit> Units { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }

        #endregion

        #region Logs Entities

        DbSet<AuditTrail> AuditLogs { get; set; }

        #endregion

        #region References Entities

        DbSet<City> Cities { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Grade> Grades { get; set; }

        DbSet<HandOverCondition> HandOverConditions { get; set; }

        DbSet<ListingType> ListingTypes { get; set; }

        DbSet<MicroDistrict> MicroDistricts { get; set; }

        DbSet<OwnershipType> OwnershipTypes { get; set; }

        DbSet<ProjectStatus> ProjectStatuses { get; set; }

        DbSet<PropertyType> PropertyTypes { get; set; }

        DbSet<Province> Provinces { get; set; }

        DbSet<Region> Regions { get; set; }

        DbSet<State> States { get; set; }

        DbSet<SubMarket> SubMarkets { get; set; }

        DbSet<SubType> SubTypes { get; set; }

        DbSet<TenantClassification> TenantClassifications { get; set; }

        DbSet<UnitStatus> UnitStatuses { get; set; }

        #endregion

        #region Shared Entities

        DbSet<Address> Addresses { get; set; }

        DbSet<SEO> SEO { get; set; }

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
