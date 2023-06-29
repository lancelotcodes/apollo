using apollo.Application.Units.Commands.CreateUnit;
using FluentValidation;

namespace apollo.Application.Properties.Commands.SaveProperty
{
    public class SaveUnitValidator : AbstractValidator<SaveUnitRequest>
    {
        public SaveUnitValidator()
        {
            RuleFor(e => e.Name).NotNull().NotEmpty();
            RuleFor(e => e.UnitNumber).NotNull().NotEmpty();
            RuleFor(e => e.LeaseFloorArea).NotNull().NotEmpty();
            RuleFor(e => e.BasePrice).NotNull().NotEmpty();
            RuleFor(e => e.CUSA).NotNull().NotEmpty();
            RuleFor(e => e.EscalationRate).NotNull().NotEmpty();
            RuleFor(e => e.MinimumLeaseTerm).NotNull().NotEmpty();
            RuleFor(e => e.ParkingRent).NotNull().NotEmpty();
            RuleFor(e => e.UnitStatusID).NotNull();
            RuleFor(e => e.AvailabilityID).NotNull();
            RuleFor(e => e.ListingTypeID).NotNull();
            RuleFor(e => e.HandOverConditionID).NotNull();
            RuleFor(e => e.FloorID).NotNull();
        }
    }
}
