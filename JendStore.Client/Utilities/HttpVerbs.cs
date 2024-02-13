namespace JendStore.Client.Utilities
{
    public class HttpVerbs
    {
        public static string CouponAPIBase{get; set;}
        public static string ProductAPIBase { get; set; }
        public static string AuthAPIBase{ get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleUser = "USER";
        public const string TokenCookie = "Token";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
