namespace Microsoft.AspNetCore.Identity.MongoDb
{
    public class UserPhoneNumber : UserContactRecord
    {
        public UserPhoneNumber(string phoneNumber) : base(phoneNumber) { }
    }
}
