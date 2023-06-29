using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.Logs;
using apollo.Domain.Entities.References;
using apollo.Domain.Entities.Shared;
using apollo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        #region Core Entities

        public DbSet<RefreshToken> AspNetRefreshTokens { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<Industrial> IndustrialListings { get; set; }

        public DbSet<Investment> Investments { get; set; }

        public DbSet<Mandate> Mandates { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyDocument> PropertyDocuments { get; set; }

        public DbSet<PropertyImageVersion> PropertyImageVersions { get; set; }

        public DbSet<ResidentialListing> ResidentialListings { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Appraisal> Appraisals { get; set; }

        public DbSet<Lead> Leads { get; set; }

        public DbSet<Document> Documents { get; set; }

        #endregion

        #region Logs Entities

        public DbSet<AuditTrail> AuditLogs { get; set; }

        #endregion

        #region References Entities

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<HandOverCondition> HandOverConditions { get; set; }

        public DbSet<ListingType> ListingTypes { get; set; }

        public DbSet<MicroDistrict> MicroDistricts { get; set; }

        public DbSet<OwnershipType> OwnershipTypes { get; set; }

        public DbSet<ProjectStatus> ProjectStatuses { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<SubMarket> SubMarkets { get; set; }

        public DbSet<SubType> SubTypes { get; set; }

        public DbSet<TenantClassification> TenantClassifications { get; set; }

        public DbSet<UnitStatus> UnitStatuses { get; set; }

        #endregion

        #region Shared Entities

        public DbSet<Address> Addresses { get; set; }
        public DbSet<SEO> SEO { get; set; }
        public DbSet<OfferOption> OfferOptions { get; set; }

        #endregion

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Email;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.IsActive = true;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.Email;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            OnBeforeSaveChanges(_currentUserService.Email);

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }

        //Audit Trail
        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditTrail || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditTrailType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditTrailType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                //check if values has been replaced
                                if (propertyName != "Created" && propertyName != "LastModified" &&
                                    (property.CurrentValue != null ? property.CurrentValue.ToString() : string.Empty) != (property.OriginalValue != null ? property.OriginalValue.ToString() : string.Empty))
                                {
                                    auditEntry.ChangedColumns.Add(propertyName);
                                    auditEntry.AuditType = AuditTrailType.Update;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }

                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
    }
}
