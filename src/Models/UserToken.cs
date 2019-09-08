namespace Microsoft.AspNetCore.Identity.MongoDb
{
    public class UserToken
    {
        public string LoginProvider { get; set; }
        public string TokenName { get; set; }
        public string TokenValue { get; set; }
    }
}
