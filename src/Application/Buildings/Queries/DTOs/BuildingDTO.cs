using apollo.Application.Common.Mappings;
using apollo.Application.Floors.Queries.DTOs;
using apollo.Application.PropertyImages.Queries.GetImageLinkBySlug;
using apollo.Application.Units.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apollo.Application.Buildings.Queries.DTOs
{
    public class BuildingDTO : IMapFrom<Building>
    {
        #region Public Properties

        public string Name { get; set; }

        public int PropertyID { get; set; }

        public string PropertyName { get; set; }

        public int PropertyTypeID { get; set; }

        public string PropertyTypeName { get; set; }
        public int? SubTypeID { get; set; }
        public string SubTypeName { get; set; }
        public int? GradeID { get; set; }
        public string GradeName { get; set; }
        public int? AddressID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int? SubMarketID { get; set; }
        public string SubMarketName { get; set; }
        public int? MicroDistrictID { get; set; }
        public string MicroDistrictName { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<PolygonDTO> PolygonPoints { get; set; }

        public string MainImage { get; set; }
        public int ImageCount { get; set; }
        public string MainFloorPlanImage { get; set; }
        public int FloorPlanCount { get; set; }
        public bool IsExclusive { get; set; }
        public string Note { get; set; }
        public int SortOrder { get; set; }
        public ICollection<PropertyImageDTO> PropertyImages { get; private set; }
        public int? SEOID { get; set; }
        public string Url { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string MetaKeyword { get; set; }
        public DateTimeOffset? PublishedDate { get; set; }
        public bool IsPublished { get; set; }
        public PublishType PublishType { get; set; }
        public bool IsFeatured { get; set; }
        public int FeaturedWeight { get; set; }

        public DateTime? DateBuilt { get; set; }

        public int YearBuilt { get; set; }

        public string PEZA { get; set; }

        public bool OperatingHours { get; set; }

        public string LEED { get; set; }

        #endregion

        #region Private Properties

        public int LeasingContactID { get; set; }

        public string LeasingContactName { get; set; }

        public int? DeveloperID { get; set; }

        public string DeveloperName { get; set; }

        public int PropertyManagementID { get; set; }

        public string PropertyManagementName { get; set; }

        public int? OwnershipTypeID { get; set; }
        public string OwnershipTypeName { get; set; }

        public int ProjectStatusID { get; set; }

        public string ProjectStatusName { get; set; }

        public DateTime? TurnOverDate { get; set; }

        public string TenantMix { get; set; }

        public double GrossBuildingSize { get; set; }

        public string GrossLeasableSize { get; set; }

        public string TypicalFloorPlateSize { get; set; }

        public int TotalFloors { get; set; }

        public int TotalUnits { get; set; }

        public decimal EfficiencyRatio { get; set; }

        public string CeilingHeight { get; set; }

        public string MinimumLeaseTerm { get; set; }

        public string Elevators { get; set; }

        public string Power { get; set; }

        public string ACSystem { get; set; }

        public string Telcos { get; set; }

        public string Amenities { get; set; }

        public string WebPage { get; set; }

        public double DensityRatio { get; set; }

        public string ParkingElevator { get; set; }

        public string PassengerElevator { get; set; }

        public string ServiceElevator { get; set; }

        #endregion

        public ICollection<FloorDTO> Floors { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Building, BuildingDTO>()
                .ForMember(i => i.Floors, o => o.MapFrom(m => m.Floors.Where(a => !a.IsDeleted).Select(a => new FloorDTO
                {
                    ID = a.ID,
                    BuildingID = a.BuildingID,
                    Name = a.Name,
                    Sort = a.Sort,
                    FloorPlateSize = a.FloorPlateSize,
                    Units = a.Units.Where(x => !x.IsDeleted).Select(x => new UnitDTO
                    {
                        ID = x.ID,
                        Name = x.Name,
                        ACCharges = x.ACCharges,
                        ACExtensionCharges = x.ACExtensionCharges,
                        BasePrice = x.BasePrice,
                        CUSA = x.CUSA,
                        EscalationRate = x.EscalationRate,
                        FloorID = x.FloorID,
                        HandOverConditionID = x.HandOverConditionID,
                        HandOverConditionName = x.HandOverCondition.Name,
                        HandOverDate = x.HandOverDate,
                        LeaseFloorArea = x.LeaseFloorArea,
                        ListingTypeID = x.ListingTypeID,
                        ListingTypeName = x.ListingType.Name,
                        MinimumLeaseTerm = x.MinimumLeaseTerm,
                        ParkingRent = x.ParkingRent,
                        UnitNumber = x.UnitNumber,
                        UnitStatusID = x.UnitStatusID,
                        UnitStatusName = x.UnitStatus.Name,
                        Contracts = x.Property.Contracts.Where(y => !y.IsDeleted).Select(y => new ContractDTO
                        {
                            ID = y.ID,
                            Name = y.Name,
                            BrokerCompanyID = y.BrokerCompanyID,
                            BrokerCompanyName = y.BrokerCompany.Name,
                            BrokerID = y.BrokerID,
                            BrokerName = y.Broker.FirstName + " " + y.Broker.LastName,
                            CompanyID = y.CompanyID,
                            CompanyName = y.Company.Name,
                            ContactID = y.ContactID,
                            ContactName = y.Contact.FirstName + " " + y.Contact.LastName,
                            StartDate = y.StartDate,
                            EndDate = y.EndDate,
                            EstimatedArea = y.EstimatedArea,
                            IsHistorical = y.IsHistorical,
                            LeaseTerm = y.LeaseTerm,
                            PropertyID = y.PropertyID,
                            PropertyName = y.Property.Name,
                            TenantClassificationID = y.TenantClassificationID,
                            TenantClassificationName = y.TenantClassification.Name
                        })
                    })
                })))
                .ForMember(i => i.Created, o => o.MapFrom(m => m.Created))
                .ForMember(i => i.CreatedBy, o => o.MapFrom(m => m.CreatedBy))
                .ForMember(i => i.LastModified, o => o.MapFrom(m => m.LastModified))
                .ForMember(i => i.LastModifiedBy, o => o.MapFrom(m => m.LastModifiedBy))
                .ForMember(i => i.IsActive, o => o.MapFrom(m => m.IsActive))
                .ForMember(i => i.IsDeleted, o => o.MapFrom(m => m.IsDeleted))
                .ForMember(i => i.Name, o => o.MapFrom(m => m.Name))
                .ForMember(i => i.PropertyID, o => o.MapFrom(m => m.PropertyID))
                .ForMember(i => i.PropertyName, o => o.MapFrom(m => m.Property.Name))
                .ForMember(i => i.PropertyTypeID, o => o.MapFrom(m => m.Property.PropertyTypeID))
                .ForMember(i => i.PropertyTypeName, o => o.MapFrom(m => m.Property.PropertyType.Name))
                .ForMember(i => i.SubTypeID, o => o.MapFrom(m => m.Property.SubTypeID))
                .ForMember(i => i.SubTypeName, o => o.MapFrom(m => m.Property.SubType.Name))
                .ForMember(i => i.GradeID, o => o.MapFrom(m => m.Property.GradeID))
                .ForMember(i => i.GradeName, o => o.MapFrom(m => m.Property.Grade.Name))
                .ForMember(i => i.Line1, o => o.MapFrom(m => m.Property.Address.Line1))
                .ForMember(i => i.Line2, o => o.MapFrom(m => m.Property.Address.Line2))
                .ForMember(i => i.CityID, o => o.MapFrom(m => m.Property.Address.CityID))
                .ForMember(i => i.CityName, o => o.MapFrom(m => m.Property.Address.City.Name))
                .ForMember(i => i.SubMarketID, o => o.MapFrom(m => m.Property.Address.SubMarketID))
                .ForMember(i => i.SubMarketName, o => o.MapFrom(m => m.Property.Address.SubMarket.Name))
                .ForMember(i => i.MicroDistrictID, o => o.MapFrom(m => m.Property.Address.MicroDistrictID))
                .ForMember(i => i.MicroDistrictName, o => o.MapFrom(m => m.Property.Address.MicroDistrict.Name))
                .ForMember(i => i.ZipCode, o => o.MapFrom(m => m.Property.Address.ZipCode))
                .ForMember(i => i.Latitude, o => o.MapFrom(m => m.Property.Address.Latitude))
                .ForMember(i => i.Longitude, o => o.MapFrom(m => m.Property.Address.Longitude))
                .ForMember(i => i.PolygonPoints, o => o.MapFrom(m =>
                !string.IsNullOrEmpty(m.Property.Address.PolygonPoints) ?
                JsonConvert.DeserializeObject<IEnumerable<PolygonDTO>>(m.Property.Address.PolygonPoints) : null))
                .ForMember(i => i.IsExclusive, o => o.MapFrom(m => m.Property.IsExclusive))
                .ForMember(i => i.Note, o => o.MapFrom(m => m.Property.Note))
                .ForMember(i => i.Url, o => o.MapFrom(m => $"{m.Property.SEO.Url}-{m.ID}"))
                .ForMember(i => i.PageTitle, o => o.MapFrom(m => m.Property.SEO.PageTitle))
                .ForMember(i => i.PageDescription, o => o.MapFrom(m => m.Property.SEO.PageDescription))
                .ForMember(i => i.MetaKeyword, o => o.MapFrom(m => m.Property.SEO.MetaKeyword))
                .ForMember(i => i.PublishedDate, o => o.MapFrom(m => m.Property.SEO.PublishedDate))
                .ForMember(i => i.IsFeatured, o => o.MapFrom(m => m.Property.SEO.IsFeatured))
                .ForMember(i => i.FeaturedWeight, o => o.MapFrom(m => m.Property.SEO.FeaturedWeight))
                .ForMember(i => i.LeasingContactID, o => o.MapFrom(m => m.LeasingContactID))
                .ForMember(i => i.DeveloperID, o => o.MapFrom(m => m.DeveloperID))
                .ForMember(i => i.PropertyManagementID, o => o.MapFrom(m => m.PropertyManagementID))
                .ForMember(i => i.OwnershipTypeID, o => o.MapFrom(m => m.OwnershipTypeID))
                .ForMember(i => i.ProjectStatusID, o => o.MapFrom(m => m.ProjectStatusID))
                .ForMember(i => i.LeasingContactName, o => o.MapFrom(m => m.LeasingContact.Name))
                .ForMember(i => i.DeveloperName, o => o.MapFrom(m => m.Developer.Name))
                .ForMember(i => i.PropertyManagementName, o => o.MapFrom(m => m.PropertyManagement.Name))
                .ForMember(i => i.OwnershipTypeName, o => o.MapFrom(m => m.OwnershipType.Name))
                .ForMember(i => i.ProjectStatusName, o => o.MapFrom(m => m.ProjectStatus.Name))
                .ForMember(i => i.TurnOverDate, o => o.MapFrom(m => m.TurnOverDate))
                .ForMember(i => i.TenantMix, o => o.MapFrom(m => m.TenantMix))
                .ForMember(i => i.GrossBuildingSize, o => o.MapFrom(m => m.GrossBuildingSize))
                .ForMember(i => i.GrossLeasableSize, o => o.MapFrom(m => m.GrossLeasableSize))
                .ForMember(i => i.TypicalFloorPlateSize, o => o.MapFrom(m => m.TypicalFloorPlateSize))
                .ForMember(i => i.TotalFloors, o => o.MapFrom(m => m.TotalFloors))
                .ForMember(i => i.TotalUnits, o => o.MapFrom(m => m.TotalUnits))
                .ForMember(i => i.EfficiencyRatio, o => o.MapFrom(m => m.EfficiencyRatio))
                .ForMember(i => i.CeilingHeight, o => o.MapFrom(m => m.CeilingHeight))
                .ForMember(i => i.MinimumLeaseTerm, o => o.MapFrom(m => m.MinimumLeaseTerm))
                .ForMember(i => i.Elevators, o => o.MapFrom(m => m.Elevators))
                .ForMember(i => i.Power, o => o.MapFrom(m => m.Power))
                .ForMember(i => i.ACSystem, o => o.MapFrom(m => m.ACSystem))
                .ForMember(i => i.Telcos, o => o.MapFrom(m => m.Telcos))
                .ForMember(i => i.Amenities, o => o.MapFrom(m => m.Amenities))
                .ForMember(i => i.WebPage, o => o.MapFrom(m => m.WebPage))
                .ForMember(i => i.DensityRatio, o => o.MapFrom(m => m.DensityRatio))
                .ForMember(i => i.ParkingElevator, o => o.MapFrom(m => m.ParkingElevator))
                .ForMember(i => i.PassengerElevator, o => o.MapFrom(m => m.PassengerElevator))
                .ForMember(i => i.ServiceElevator, o => o.MapFrom(m => m.ServiceElevator));
        }

    }

    public class BuildingPublicProperties
    {

    }
    public class BuildingPrivateProperties
    {

    }

}
