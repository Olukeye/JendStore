namespace JendStore.Client.Utilities
{
    public class HttpVerbs
    {
        public static string CouponAPIBase{get; set;}
        public static string AuthAPIBase{ get; set; }
        public const string RoleAdmin = "Admin";
        public const string RoleUser = "User";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
