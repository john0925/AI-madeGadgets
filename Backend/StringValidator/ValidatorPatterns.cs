
namespace john.aiGadgets.StringValidator
{
    public static class ValidatorPatterns
    {
        public static readonly string Alpha = "^[a-zA-Z]+$";
        public static readonly string Numeric = "^[0-9]+$";
        public static readonly string AlphaNumeric = "^[a-zA-Z0-9]+$";
        public static readonly string Chinese = "^[\u4e00-\u9fa5]+$";
        public static readonly string Email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public static readonly string Url = @"^(http|https)://[^\s/$.?#].[^\s]*$";
        public static readonly string IP = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        public static readonly string Currency = @"^\d+(\.\d{1,2})?$";
        public static readonly string Date = @"^\d{4}-\d{2}-\d{2}$";
        public static readonly string Time = @"^\d{2}:\d{2}:\d{2}$";
        public static readonly string DateTime = @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$";
        public static readonly string ZipCode = @"^\d{3,10}$";
    }
}
