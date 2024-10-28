namespace Inventory_Management.Common.Helpers
{
    public static class MethodHelper
    {
        public static bool ValueEqualsTo(this string value1, string value2)
        {
            return string.Equals(value1, value2 , StringComparison.OrdinalIgnoreCase);
        }
    }
}
