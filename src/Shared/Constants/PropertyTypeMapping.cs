using System.Collections.Generic;

namespace Shared.Constants
{
    public static class ApplicationMapping
    {
        public static IDictionary<string, string> PropertyMappings = new Dictionary<string, string>()
        {
            {"Office Building","Office Space" },
            {"Retail Building","Retail Space" },
            {"Warehouse Building","Warehouse Space" },
            {"Condominium Building","Condominium Unit" },
            {"Dormitory Building","House & Lot" }
        };
    }
}
