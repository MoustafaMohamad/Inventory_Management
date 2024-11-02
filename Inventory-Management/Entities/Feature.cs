using Inventory_Management.Common.Enums;

namespace Inventory_Management.Entities
{
    public class Feature : BaseModel
    {
        public string Name { get; set; }

        public EnumFeature featureValue { get; set; }
    }
}
