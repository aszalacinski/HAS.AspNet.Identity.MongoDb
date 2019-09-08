using System;

namespace Microsoft.AspNetCore.Identity.MongoDb
{
    public class UserEmail : UserContactRecord
    {
        public UserEmail(string email) : base(email)
        {
        }

        public string NormalizedValue { get; private set; }

        public virtual void SetNormalizedEmail(string normalizedEmail)
        {
            NormalizedValue = normalizedEmail ?? throw new ArgumentNullException(nameof(normalizedEmail));
        }
    }
}
