namespace apollo.Application.Floors.Commands.MigrateFloor
{
    public class MigrateFloorDTO
    {
        public int BuildingID { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public decimal FloorPlateSize { get; set; }
    }
}