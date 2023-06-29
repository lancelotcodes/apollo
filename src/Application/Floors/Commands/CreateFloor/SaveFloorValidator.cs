using apollo.Application.Floors.Commands.CreateFloor;
using FluentValidation;

namespace apollo.Application.Properties.Commands.SaveProperty
{
    public class SaveFloorValidator : AbstractValidator<SaveFloorRequest>
    {
        public SaveFloorValidator()
        {
            RuleFor(e => e.Name).NotNull().NotEmpty();
            RuleFor(e => e.FloorPlateSize).NotNull().NotEmpty();
            RuleFor(e => e.Sort).NotNull().NotEmpty();
            RuleFor(e => e.BuildingID).NotNull();
        }
    }
}
