using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class UserPreferences : BaseEntityId
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int PropertyListViewMode { get; set; }
        public int StackingPlanViewMode { get; set; }
        public int PropertyDetailsBrokerViewMode { get; set; }
        public string PropertyInfoSequence { get; set; }
        public int CardCollapse { get; set; }
        public int MapCollapse { get; set; }
        public int OptionListTemplate { get; set; }
        public int SubscriptionMode { get; set; }
        public string SubscriptionPropertyTypes { get; set; }
        public string SubscriptionCities { get; set; }
        public string SubscriptionSubmarket { get; set; }
    }
}
