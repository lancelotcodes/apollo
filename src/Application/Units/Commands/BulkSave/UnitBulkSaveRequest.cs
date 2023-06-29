using apollo.Application.Common.Mappings;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace apollo.Application.Units.Commands.CreateUnit
{
    public class UnitBulkSaveRequest : IRequest<bool>
    {
        public List<UpdateUnitRequest> Units { get; set; }

    }

    public class UpdateUnitRequest : IMapFrom<Domain.Entities.Core.Unit>
    {
        public int ID { get; set; }

        public int FloorID { get; set; }

        public string UnitNumber { get; set; }

        public int UnitStatusID { get; set; }

        public int AvailabilityID { get; set; }

        public int ListingTypeID { get; set; }

        public int HandOverConditionID { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CUSA { get; set; }

        public string ACCharges { get; set; }

        public string ACExtensionCharges { get; set; }

        public decimal EscalationRate { get; set; }

        public int MinimumLeaseTerm { get; set; }

        public decimal LeaseFloorArea { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUnitRequest, Domain.Entities.Core.Unit>(MemberList.Source);
        }
    }
}
