using apollo.Application.Agents.Commands.SavePropertyAgent;
using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Floors.Queries.DTOs;
using apollo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace apollo.Application.Buildings.Commands.CreateBuilding
{
    public class CreateBuildingCommand : IRequest<int>
    {
        #region Public Properties

        public string Name { get; set; }
        public string PropertyName { get; set; }
        public int? MasterProjectID { get; set; }
        public string MasterProjectName { get; set; }
        public int PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public int? SubTypeID { get; set; }
        public string SubTypeName { get; set; }
        public int? GradeID { get; set; }
        public string GradeName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int? SubMarketID { get; set; }
        public string SubMarketName { get; set; }
        public int? MicroDistrictID { get; set; }
        public string MicroDistrictName { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<PolygonDTO> PolygonPoints { get; set; }

        public int PropertyContactID { get; set; }
        public string PropertyContactName { get; set; }
        public int PropertyContactCompanyID { get; set; }
        public string PropertyContactCompany { get; set; }
        public int PropertyOwnerID { get; set; }
        public string PropertyOwnerName { get; set; }
        public int PropertyOwnerCompanyID { get; set; }
        public string PropertyOwnerCompany { get; set; }

        public string MainImage { get; set; }
        public string MainFloorPlanImage { get; set; }
        public bool IsExclusive { get; set; }
        public string Note { get; set; }
        public int SortOrder { get; set; }
        public List<PropertyImageCreateDTO> PropertyImages { get; private set; }

        public List<PropertyAgentRequest> PropertyAgents { get; private set; }

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

        public List<FloorDTO> Floors { get; set; }

    }
}
