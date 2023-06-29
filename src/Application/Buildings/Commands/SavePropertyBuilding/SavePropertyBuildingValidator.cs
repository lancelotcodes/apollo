using FluentValidation;

namespace apollo.Application.Buildings.Commands.SavePropertyBuilding
{
    public class SavePropertyBuildingValidator : AbstractValidator<SavePropertyBuildingRequest>
    {
        public SavePropertyBuildingValidator()
        {
            RuleFor(e => e.PropertyID).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.YearBuilt).NotNull();
            RuleFor(e => e.PEZA).NotNull();
            RuleFor(e => e.OperatingHours).NotNull();
            RuleFor(e => e.GrossBuildingSize).NotNull();
            RuleFor(e => e.TotalFloors).NotNull();
            RuleFor(e => e.TotalUnits).NotNull();
            RuleFor(e => e.EfficiencyRatio).NotNull();
            RuleFor(e => e.DensityRatio).NotNull();
        }
    }
}
